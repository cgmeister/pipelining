package edu.dlsu.mips.util;

import java.util.Map;

import edu.dlsu.mips.exception.MemoryAddressOverFlowException;
import edu.dlsu.mips.exception.MemoryAddressRangeException;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.util.Storage;

import junit.framework.TestCase;

public class StorageTest extends TestCase {
	
	@Override
	protected void setUp() throws Exception {
		// TODO Auto-generated method stub
		Storage.initializeStorage();
	}
	
	public void testMemoryFunctions(){
		
		try {
			Storage.setMemoryContents("FFF9", "FF33DD");
			assertEquals("FF33DD", Storage.getMemoryContents("FFF9", "0003"));
			Map<String, String> memoryDump = Storage.getMemoryDump("FFF9", "FFFF");
			for(String memoryAddress: memoryDump.keySet()){
				System.out.println(memoryAddress + " => " + memoryDump.get(memoryAddress));
			}
		} catch (MemoryAddressOverFlowException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (StorageInitializationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (MemoryAddressRangeException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public void testRegisterFunctions(){
		try {
			Storage.setRegisterContents("r31", "1");
			String val = Storage.getRegisterContents("r31");
			assertEquals("0000000000000001",val);
			Storage.setRegisterContents("r0", "F");
			val = Storage.getRegisterContents("r0");
			assertEquals("0000000000000000",val);
			Storage.setRegisterContents("r1", "77");
			val = Storage.getRegisterContents("r1");
			assertEquals("0000000000000077",val);
			//Map<String, String> register = Storage.getRegisterDump();
			//for(String regAddress: register.keySet()){
				//System.out.println(regAddress + " => " + register.get(regAddress));
			//}
		} catch (StorageInitializationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (RegisterAddressOverFlowException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
