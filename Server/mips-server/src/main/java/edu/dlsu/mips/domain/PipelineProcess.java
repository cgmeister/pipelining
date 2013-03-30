package edu.dlsu.mips.domain;

public class PipelineProcess {

	private PipelineStage pipelineStage;
	private Opcode opcode;

	private PipelineProcess(Opcode opcode) {
		this.pipelineStage = PipelineStage.IF;
		this.opcode = opcode;
	}

	public static PipelineProcess newInstance(Opcode opcode) {
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

	public Opcode getOpcode() {
		return opcode;
	}

	public void setOpcode(Opcode opcode) {
		this.opcode = opcode;
	}

	public PipelineStage getPipelineStage() {
		return pipelineStage;
	}

}
