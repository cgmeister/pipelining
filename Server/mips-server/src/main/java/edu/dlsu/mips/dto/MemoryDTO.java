package edu.dlsu.mips.dto;

import java.util.LinkedHashMap;
import java.util.Map;

import edu.dlsu.mips.exception.BitLengthException;
import edu.dlsu.mips.exception.MemoryAddressOverFlowException;
import edu.dlsu.mips.exception.MemoryAddressRangeException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.util.BitStringUtils;
import edu.dlsu.mips.util.Storage;

public class MemoryDTO {
	
	private static MemoryDTO memoryDTO;
	
	static{
		memoryDTO = new MemoryDTO();
	}
	
	public static MemoryDTO getInstance(){
		return memoryDTO;
	}
	
	private LinkedHashMap<String, String> memory;
	
	public void getMemory(String start, String end){
		
		try {
			Map<String, String> memoryMap;
			
				memoryMap = Storage.getMemoryDump(start, end);
			
			
			memory = new LinkedHashMap<String, String>();
			Long s = Long.decode("0x" + start);
			while(s <= Long.decode("0x" + end)){
				
				String address = BitStringUtils.doUnsignedHexSignExtension(Long.toHexString(s) , 16);
				System.out.println(address);
				memory.put(address.toUpperCase(), memoryMap.get(address.toUpperCase()));
				s++;
				
			}
	
		} catch (StorageInitializationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			
		} catch (MemoryAddressOverFlowException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (MemoryAddressRangeException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (BitLengthException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
	public void setMemory(String address, String value) throws MemoryAddressOverFlowException, StorageInitializationException{
		Storage.setMemoryContents(address, value);
		
	}
	
	
	public LinkedHashMap<String, String> getMemory(){
		return memory;
	}
}
