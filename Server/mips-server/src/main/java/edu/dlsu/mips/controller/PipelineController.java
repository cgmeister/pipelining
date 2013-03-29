package edu.dlsu.mips.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import edu.dlsu.mips.domain.PipelineStage;
import edu.dlsu.mips.dto.PipelineDTO;

@Controller
public class PipelineController {

	@RequestMapping(value = "/pipeline", method = RequestMethod.GET)
	@ResponseBody
	public PipelineDTO viewPipelineMap() {
		return PipelineDTO.newInstance(PipelineStage.IF);
	}
	
}
