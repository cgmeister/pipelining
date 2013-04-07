package edu.dlsu.mips.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import edu.dlsu.mips.dto.MemoryDTO;
import edu.dlsu.mips.exception.MemoryAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.util.Storage;

@Controller
@RequestMapping("/Memory")
public class MemoryController {
	
		@RequestMapping(value = "/dump", method = RequestMethod.GET)
		@ResponseBody
		public MemoryDTO getMemory(@RequestParam("start") String start, @RequestParam("end") String end){
			
			MemoryDTO memoryDTO = MemoryDTO.getInstance();
			memoryDTO.getMemory(start, end);
			
			return memoryDTO;
			
		}
		
		@RequestMapping(value = "/set", method = RequestMethod.GET)
		@ResponseBody
		public String setMemory(@RequestParam("address") String address, @RequestParam("value") String value){
			Storage.initializeStorage();
			MemoryDTO memoryDTO = MemoryDTO.getInstance();
			try {
				memoryDTO.setMemory(address, value);
			} catch (MemoryAddressOverFlowException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				return "fail";
			} catch (StorageInitializationException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				return "fail";
			}
			
			return "success";
			
		}
}
