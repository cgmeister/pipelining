package edu.dlsu.mips.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import edu.dlsu.mips.dto.InstructionDTO;

@Controller
@RequestMapping("/Instruction")
public class InstructionController {
	
	@RequestMapping(value = "/send", method = RequestMethod.GET)
	@ResponseBody
	public String sendInstruction(@RequestParam("instruction") String instruction){
		
		InstructionDTO instructionDTO = InstructionDTO.getInstance();
		instructionDTO.setInstruction(instruction);
		return "success";
		
	}
	
}
