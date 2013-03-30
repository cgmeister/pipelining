package edu.dlsu.mips.processor.impl;

import edu.dlsu.mips.domain.Instruction;
import edu.dlsu.mips.domain.MIPSRegisters;
import edu.dlsu.mips.domain.InstructionSet;
import edu.dlsu.mips.domain.PipelineProcess;
import edu.dlsu.mips.domain.PipelineStage;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.exception.TrapException;
import edu.dlsu.mips.processor.PipelineProcessor;
import edu.dlsu.mips.util.InstructionRunner;
import edu.dlsu.mips.util.InstructionSetBuilder;
import edu.dlsu.mips.util.SystemUtils;

public class PipelineProcessorImpl implements PipelineProcessor {

	@Override
	public void processInstruction(Instruction instruction)
			throws JumpAddressException, OperandException, 
			StorageInitializationException, RegisterAddressOverFlowException,
			TrapException, OpcodeNotSupportedException {
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
	
	private void processWB() {
		PipelineProcess wbProcess = SystemUtils.retrieveActiveProcess(PipelineStage.WB);
		SystemUtils.removeFromActiveProcess(PipelineStage.WB);
		InstructionSet opcode = wbProcess.getOpcode();
	}
	
	private void processMem() {
		PipelineProcess memProcess = SystemUtils.retrieveActiveProcess(PipelineStage.MEM);
		InstructionSet opcode = memProcess.getOpcode();
		if (opcode.getName().equals("LD")) {
			
		} else if (opcode.getName().equals("SD")) {
			
		} else if (opcode.getName().equals("BNEZ")) {
			
		}
		memProcess.incrementStage();
	}
	
	private void processExe() throws StorageInitializationException, RegisterAddressOverFlowException, TrapException {
		PipelineProcess exeProcess = SystemUtils.retrieveActiveProcess(PipelineStage.EXE);
		InstructionSet opcode = exeProcess.getOpcode();
		String opcodeString = opcode.getName();
		if (opcodeString.equals("DADD")) {
			InstructionRunner.DADD(opcode.getRs(), opcode.getRt());
		} else if (opcodeString.equals("DSUB")) {
			InstructionRunner.DSUB(opcode.getRs(), opcode.getRt());
		} else if (opcodeString.equals("XOR")) {
			InstructionRunner.XOR(opcode.getRs(), opcode.getRt());
		} else if (opcodeString.equals("SLT")) {
			InstructionRunner.SLT(opcode.getRs(), opcode.getRt());
		} else if (opcodeString.equals("AND")) {
			InstructionRunner.AND(opcode.getRs(), opcode.getRt());
		} else if (opcodeString.equals("BNEZ")) {
			
		} else if (opcodeString.equals("SD")) {
			
		} else if (opcodeString.equals("LD")) {
			
		} else if (opcodeString.equals("DADDI")) {
			InstructionRunner.DADDI(opcode.getRs(), opcode.getImm());
		} else if (opcodeString.equals("J")) {
			
		}
		
	}
	
	private void processID() {
		PipelineProcess idProcess = SystemUtils.retrieveActiveProcess(PipelineStage.ID);
		MIPSRegisters.IDEXA = idProcess.getOpcode().getBinaryInstruction().substring(6, 10);
		MIPSRegisters.IDEXB = idProcess.getOpcode().getBinaryInstruction().substring(11, 15);
		MIPSRegisters.IDEXIMM = idProcess.getOpcode().getBinaryInstruction().substring(16, 31);
		idProcess.incrementStage();
	}
	
	private void processIF(Instruction instruction) throws JumpAddressException, OperandException, OpcodeNotSupportedException {
		PipelineProcess ifProcess = buildPipelineProcess(instruction);
		SystemUtils.addActiveProcess(ifProcess);
		SystemUtils.addToAllProcess(ifProcess);
		MIPSRegisters.IFIDIR = ifProcess.getOpcode().getBinaryInstruction();
		String nextPC = incrementProgramCounter();
		MIPSRegisters.IFIDNPC = nextPC;
		InstructionSet opcode = ifProcess.getOpcode();
		if (opcode.getName().equals("J")) {
			MIPSRegisters.PC = retrieveJumpInstruction(opcode);
		} else if (opcode.getName().equals("BNEZ")) {
			
		} else {
			MIPSRegisters.PC = nextPC;
		}
		
		ifProcess.incrementStage();
	}
	
	private String incrementProgramCounter() {
		String currentPC = MIPSRegisters.PC;
		
		return null;
	}
	
	private String retrieveJumpInstruction(InstructionSet opcode) {
		
		return null;
	}
	
	private PipelineProcess buildPipelineProcess(Instruction instruction) throws JumpAddressException, OperandException, OpcodeNotSupportedException {
		InstructionSet opcode = InstructionSetBuilder.buildOpcode(instruction.getInstruction());
		return PipelineProcess.newInstance(opcode);
	}

}
