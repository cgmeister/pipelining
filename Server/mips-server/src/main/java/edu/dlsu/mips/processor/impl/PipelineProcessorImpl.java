package edu.dlsu.mips.processor.impl;

import edu.dlsu.mips.domain.Instruction;
import edu.dlsu.mips.domain.InstructionSet;
import edu.dlsu.mips.domain.MIPSRegisters;
import edu.dlsu.mips.domain.PipelineProcess;
import edu.dlsu.mips.domain.PipelineStage;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.MemoryAddressOverFlowException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.exception.TrapException;
import edu.dlsu.mips.processor.PipelineProcessor;
import edu.dlsu.mips.util.BitStringUtils;
import edu.dlsu.mips.util.InstructionRunner;
import edu.dlsu.mips.util.InstructionSetBuilder;
import edu.dlsu.mips.util.Storage;
import edu.dlsu.mips.util.SystemUtils;

public class PipelineProcessorImpl implements PipelineProcessor {

	private static final String LD_OPCODE_OFFSET = "8";

	@Override
	public void processInstruction(Instruction instruction)
			throws JumpAddressException, OperandException, StorageInitializationException, RegisterAddressOverFlowException,
			TrapException, OpcodeNotSupportedException, MemoryAddressOverFlowException {
		incrementSystemClock();
		processWB();
		processMem();
		processExe();
		processID();
		processIF(instruction);
	}

	private void incrementSystemClock() {
		SystemUtils.clockCycle++;
	}

	private void processWB() throws StorageInitializationException, RegisterAddressOverFlowException {
		PipelineProcess wbProcess = SystemUtils.retrieveActiveProcess(PipelineStage.WB);
		if (null != wbProcess) {
			InstructionSet instructionSet = wbProcess.getInstructionSet();
			String opcode = instructionSet.getOpcode();
			if (opcode.equals("LD")) {
				Storage.setRegisterContents(instructionSet.getRt(), MIPSRegisters.MEMWBLMD);
			} else if (isALUInstruction(opcode)) {
				Storage.setRegisterContents(instructionSet.getRt(), MIPSRegisters.MEMWBALUOUTPUT);
			}
			wbProcess.logProcessClocking();
			SystemUtils.removeFromActiveProcess(PipelineStage.WB);
		}
	}
	
	private boolean isALUInstruction(String opcode) {
		return opcode.equals("DADD") || opcode.equals("DSUB")
				|| opcode.equals("DADDI") || opcode.equals("AND")
				|| opcode.equals("XOR") || opcode.equals("SLT");
	}
	
	private void processMem() throws StorageInitializationException, MemoryAddressOverFlowException {
		PipelineProcess memProcess = SystemUtils.retrieveActiveProcess(PipelineStage.MEM);
		if (null != memProcess) {
			InstructionSet instructionSet = memProcess.getInstructionSet();
			String opcode = instructionSet.getOpcode();
			if (opcode.equals("LD")) {
				MIPSRegisters.MEMWBIR = MIPSRegisters.EXMEMIR;
				MIPSRegisters.MEMWBLMD = Storage.getMemoryContents(MIPSRegisters.EXMEMALUOUTPUT, LD_OPCODE_OFFSET);
			} else if (opcode.equals("SD")) {
				MIPSRegisters.MEMWBIR = MIPSRegisters.EXMEMIR;
				Storage.setMemoryContents(MIPSRegisters.EXMEMALUOUTPUT, MIPSRegisters.EXMEMB);
			} else if (isALUInstruction(opcode)) {
				MIPSRegisters.MEMWBIR = MIPSRegisters.EXMEMIR;
				MIPSRegisters.MEMWBALUOUTPUT = MIPSRegisters.EXMEMALUOUTPUT;
			}
			memProcess.logProcessClocking();
			memProcess.incrementStage();
		}
	}

	private void processExe() throws StorageInitializationException,
			RegisterAddressOverFlowException, TrapException {
		PipelineProcess exeProcess = SystemUtils.retrieveActiveProcess(PipelineStage.EXE);
		if (null != exeProcess) {
			InstructionSet instructionSet = exeProcess.getInstructionSet();
			String opcode = instructionSet.getOpcode();
			if (opcode.equals("DADD")) {
				MIPSRegisters.EXMEMIR = MIPSRegisters.IDEXIR;
				MIPSRegisters.EXMEMALUOUTPUT = InstructionRunner.DADD(instructionSet.getRs(), instructionSet.getRt());
				MIPSRegisters.EXMEMCOND = "0";
			} else if (opcode.equals("DSUB")) {
				MIPSRegisters.EXMEMIR = MIPSRegisters.IDEXIR;
				MIPSRegisters.EXMEMALUOUTPUT = InstructionRunner.DSUB(instructionSet.getRs(), instructionSet.getRt());
				MIPSRegisters.EXMEMCOND = "0";
			} else if (opcode.equals("XOR")) {
				MIPSRegisters.EXMEMIR = MIPSRegisters.IDEXIR;
				MIPSRegisters.EXMEMALUOUTPUT = InstructionRunner.XOR(instructionSet.getRs(), instructionSet.getRt());
				MIPSRegisters.EXMEMCOND = "0";
			} else if (opcode.equals("SLT")) {
				MIPSRegisters.EXMEMIR = MIPSRegisters.IDEXIR;
				MIPSRegisters.EXMEMALUOUTPUT = InstructionRunner.SLT(instructionSet.getRs(), instructionSet.getRt());
				MIPSRegisters.EXMEMCOND = "0";
			} else if (opcode.equals("AND")) {
				MIPSRegisters.EXMEMIR = MIPSRegisters.IDEXIR;
				MIPSRegisters.EXMEMALUOUTPUT = InstructionRunner.AND(instructionSet.getRs(), instructionSet.getRt());
				MIPSRegisters.EXMEMCOND = "0";
			} else if (opcode.equals("BNEZ")) {
				Integer aluOutput = Integer.parseInt(MIPSRegisters.IDEXNPC, 2) + Integer.parseInt(MIPSRegisters.IDEXIMM, 2);
				MIPSRegisters.EXMEMALUOUTPUT = Integer.toBinaryString(aluOutput);
				MIPSRegisters.EXMEMCOND = Integer.parseInt(MIPSRegisters.IDEXA, 2) != 0? "1" : "0";
			} else if (opcode.equals("SD")) {
				MIPSRegisters.EXMEMIR = MIPSRegisters.IDEXIR;
				MIPSRegisters.EXMEMB = MIPSRegisters.IDEXB;
				MIPSRegisters.EXMEMCOND = "0";
			} else if (opcode.equals("LD")) {
				MIPSRegisters.EXMEMIR = MIPSRegisters.IDEXIR;
				Integer aluOutput = Integer.parseInt(MIPSRegisters.IDEXA, 2) + Integer.parseInt(MIPSRegisters.IDEXIMM, 2);
				MIPSRegisters.EXMEMALUOUTPUT = Integer.toBinaryString(aluOutput);
			} else if (opcode.equals("DADDI")) {
				MIPSRegisters.EXMEMIR = MIPSRegisters.IDEXIR;
				MIPSRegisters.EXMEMALUOUTPUT = InstructionRunner.DADDI(instructionSet.getRs(), instructionSet.getImm());
				MIPSRegisters.EXMEMCOND = "0";
			} else if (opcode.equals("J")) {
				
			}
		}
	}

	private void processID() {
		PipelineProcess idProcess = SystemUtils.retrieveActiveProcess(PipelineStage.ID);
		if (null != idProcess) {
			MIPSRegisters.IDEXIR = MIPSRegisters.IFIDIR;
			MIPSRegisters.IDEXA = idProcess.getInstructionSet().getBinaryInstruction().substring(6, 10);
			MIPSRegisters.IDEXB = idProcess.getInstructionSet().getBinaryInstruction().substring(11, 15);
			MIPSRegisters.IDEXIMM = idProcess.getInstructionSet().getBinaryInstruction().substring(16, 31);
			idProcess.logProcessClocking();
			idProcess.incrementStage();
		}
	}

	private void processIF(Instruction instruction) throws JumpAddressException, OperandException, OpcodeNotSupportedException {
		PipelineProcess ifProcess = buildPipelineProcess(instruction);
		SystemUtils.addActiveProcess(ifProcess);
		SystemUtils.addToAllProcess(ifProcess);
		MIPSRegisters.IFIDIR = ifProcess.getInstructionSet().getHexInstruction();
		String nextPC = incrementProgramCounter();
		MIPSRegisters.IFIDNPC = nextPC;
		InstructionSet instructionSet = ifProcess.getInstructionSet();
		if (instructionSet.getOpcode().equals("J")) {
			MIPSRegisters.PC = retrieveJumpInstruction(instructionSet);
		} else if (instructionSet.getOpcode().equals("BNEZ")) {
			MIPSRegisters.PC = retrieveBranchInstruction(instructionSet);
		} else {
			MIPSRegisters.PC = nextPC;
		}
		ifProcess.logProcessClocking();
		ifProcess.incrementStage();
	}

	private String incrementProgramCounter() {
		return SystemUtils.incrementPc(MIPSRegisters.PC);
	}

	private String retrieveJumpInstruction(InstructionSet instructionSet) {
		String binaryInstruction = instructionSet.getBinaryInstruction().substring(6,31);
		return BitStringUtils.shiftLeft(binaryInstruction, 2);
	}

	private String retrieveBranchInstruction(InstructionSet instructionSet) {
		return null;
	}

	private PipelineProcess buildPipelineProcess(Instruction instruction)
			throws JumpAddressException, OperandException, OpcodeNotSupportedException {
		InstructionSet opcode = InstructionSetBuilder.buildInstructionSet(instruction.getInstruction());
		return PipelineProcess.newInstance(opcode);
	}

}
