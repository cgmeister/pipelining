package edu.dlsu.mips.dto;

import edu.dlsu.mips.domain.PipelineStage;

public class PipelineDTO {

	private PipelineStage stage;
	
	private PipelineDTO(PipelineStage stage) {
		this.stage = stage;
	}
	
	public static PipelineDTO newInstance(PipelineStage stage) {
		return new PipelineDTO(stage);
	}

	public PipelineStage getStage() {
		return stage;
	}
	
}
