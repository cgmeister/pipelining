package edu.dlsu.mips.domain;

public class Instruction {

	private String instruction;
	
	public Instruction(String instruction) {
		this.instruction = instruction;
	}

	public static Instruction newInstance(String instruction) {
		return new Instruction(instruction);
	}

	public String getInstruction() {
		return instruction;
	}

	public void setInstruction(String instruction) {
		this.instruction = instruction;
	}
	
}
