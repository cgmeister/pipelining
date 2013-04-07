package edu.dlsu.mips.controller;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import edu.dlsu.mips.domain.MIPSRegisters;
import edu.dlsu.mips.dto.PipelineDTO;
import edu.dlsu.mips.util.Storage;
import edu.dlsu.mips.util.SystemUtils;

@Controller
@RequestMapping("/")
public class IndexController {

	@RequestMapping(value = "/welcome", method = RequestMethod.GET)
	public String welcome(Model model) {
		model.addAttribute("message", "Welcome to MIPS Server!");
		return "index";
	}
	
	@RequestMapping(value = "/initialize", method = RequestMethod.GET)
	@ResponseBody
	public String initialize(){
		Storage.initializeStorage();
		MIPSRegisters.getInstance().initialize();
		SystemUtils.initialize();
		PipelineDTO.getInstance().Initialize();
		return "success";
		
	}
}
