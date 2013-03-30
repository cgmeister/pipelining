package edu.dlsu.mips.domain;

public class PipelineProcess {

	private PipelineStage pipelineStage;
	private InstructionSet opcode;

	private PipelineProcess(InstructionSet opcode) {
		this.pipelineStage = PipelineStage.IF;
		this.opcode = opcode;
	}

	public static PipelineProcess newInstance(InstructionSet opcode) {
		return new PipelineProcess(opcode);
	}
	
	public void incrementStage() {
		if (pipelineStage.equals(PipelineStage.IF)) {
			pipelineStage = PipelineStage.ID;
		} else if (pipelineStage.equals(PipelineStage.ID)) {
			pipelineStage = PipelineStage.EXE;
		} else if (pipelineStage.equals(PipelineStage.EXE)) {
			pipelineStage = PipelineStage.MEM;
		} else if (pipelineStage.equals(PipelineStage.MEM)) {
			pipelineStage = PipelineStage.WB;
		}
	}

	public InstructionSet getOpcode() {
		return opcode;
	}

	public void setOpcode(InstructionSet opcode) {
		this.opcode = opcode;
	}

	public PipelineStage getPipelineStage() {
		return pipelineStage;
	}

}
