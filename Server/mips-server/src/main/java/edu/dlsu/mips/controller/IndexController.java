package edu.dlsu.mips.controller;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import edu.dlsu.mips.util.Storage;

@Controller
@RequestMapping("/")
public class IndexController {

	@RequestMapping(value = "/welcome", method = RequestMethod.GET)
	public String welcome(Model model) {
		//for testing
		Storage.initializeStorage();
		//end for testing
		model.addAttribute("message", "Welcome to MIPS Server! //initializing storage //for testing uncomment it after if ever i failed to");
		return "index";
	}

}
