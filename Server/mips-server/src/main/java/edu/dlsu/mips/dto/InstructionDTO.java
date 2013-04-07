package edu.dlsu.mips.dto;

import edu.dlsu.mips.domain.InstructionSet;
import edu.dlsu.mips.domain.MIPSRegisters;
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
	
	public String getPC(){
		return MIPSRegisters.PC;
	}
	
	public String getIFIDNPC(){
		return MIPSRegisters.IFIDNPC;
	}
	
	public String getIFIDIR(){
		return MIPSRegisters.IFIDIR;
	}
	
	public String getIDEXIR(){
		return MIPSRegisters.IDEXIR;
	}
	public String getIDEXA(){
		return MIPSRegisters.IDEXA;
	}
	public String getIDEXB(){
		return MIPSRegisters.IDEXB;
	}
	public String getIDEXIMM(){
		return MIPSRegisters.IDEXIMM;
	}
	public String getIDEXNPC(){
		return MIPSRegisters.IDEXNPC;
	}
	
	public String getEXMEMIR(){

		return MIPSRegisters.EXMEMIR;
	}
	public String getEXMEMALUOUTPUT(){
		return MIPSRegisters.EXMEMALUOUTPUT;
	}
	public String getEXMEMB(){
		return MIPSRegisters.EXMEMB;
	}
	public String getEXMEMCOND(){
		return MIPSRegisters.EXMEMCOND;
	}
	
	public String getMEMWBIR(){
		return MIPSRegisters.MEMWBIR;
	}
	
	public String getMEMWBALUOUTPUT(){
		return MIPSRegisters.MEMWBALUOUTPUT;
	}
	public String getMEMWBLMD(){
		return MIPSRegisters.MEMWBLMD;
	}
}
