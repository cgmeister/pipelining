package edu.dlsu.mips.dto;

import edu.dlsu.mips.domain.InstructionSet;
import edu.dlsu.mips.domain.MIPSRegisters;
import edu.dlsu.mips.domain.ProcessStatus;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.util.InstructionSetBuilder;

public class InstructionDTO {
	private static InstructionDTO instructionDTO;
	
	static{
		instructionDTO = new InstructionDTO();
	}
	
	public static InstructionDTO getInstance(){
		return instructionDTO;
	}
	
	private InstructionSet instructionSet;
	
	public void setInstruction(String instruction){
		try {
			instructionSet = InstructionSetBuilder.buildInstructionSet(instruction);
		} catch (JumpAddressException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (OperandException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (OpcodeNotSupportedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public InstructionSet getInstructionSet(){
		return instructionSet;
	}
}
