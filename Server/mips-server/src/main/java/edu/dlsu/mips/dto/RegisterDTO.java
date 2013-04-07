package edu.dlsu.mips.dto;

import java.util.LinkedHashMap;
import java.util.Map;

import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.util.Storage;

public class RegisterDTO {
	private static RegisterDTO registerDTO;
	
	static{
		registerDTO = new RegisterDTO();
	}
	
	public static RegisterDTO getInstance(){
		return registerDTO;
	}
	
	private LinkedHashMap<String, String> register;
	
	public void updateRegister(){
		
		try {
			Map<String, String> registerMap = Storage.getRegisterDump();
			//map = new TreeMap<String, String>();
			register = new LinkedHashMap<String, String>();
			for(int i = 0; i < registerMap.size(); i++){
				register.put("R"+i, registerMap.get("R"+i));
			}
	
		} catch (StorageInitializationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			
		}
		
	}
	
	public LinkedHashMap<String, String> getRegister(){
		return register;
	}
}
