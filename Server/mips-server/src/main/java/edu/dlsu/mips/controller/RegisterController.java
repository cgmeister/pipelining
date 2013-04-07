package edu.dlsu.mips.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import edu.dlsu.mips.dto.RegisterDTO;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.util.Storage;

@Controller
@RequestMapping("/Register")
public class RegisterController {
	
	@RequestMapping(value = "/dump", method = RequestMethod.GET)
	@ResponseBody
	public RegisterDTO getRegister(){
		
		RegisterDTO registerDTO = RegisterDTO.getInstance();
		registerDTO.updateRegister();
		
		return registerDTO;
		
	}
	
	@RequestMapping(value = "/set", method = RequestMethod.GET)
	@ResponseBody
	public RegisterDTO setRegister(@RequestParam("register") String register, @RequestParam("value")String value){
		
		try {
			Storage.setRegisterContents(register, value);
		} catch (StorageInitializationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (RegisterAddressOverFlowException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		RegisterDTO registerDTO = RegisterDTO.getInstance();
		registerDTO.updateRegister();
		
		return registerDTO;
	}
	
	
}
