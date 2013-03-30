package edu.dlsu.mips.util;


import junit.framework.TestCase;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.exception.TrapException;

public class InstructionRunnerTest extends TestCase  {
	
	@Override
	protected void setUp() throws Exception {
		// TODO Auto-generated method stub
		Storage.initializeStorage();
	}
	
	public void testInstructionRunner(){
		
		try {
			
			Storage.setRegisterContents("R2", "0000010000000000");
			Storage.setRegisterContents("R1", "FFFFFFFFFFFFFFFF");
			InstructionRunner.DADD("R2", "R1");
			InstructionRunner.DSUB("R2", "R1");
			InstructionRunner.AND("R2", "R1");
			InstructionRunner.XOR("R2", "R1");
		} catch (StorageInitializationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			fail();
		} catch (RegisterAddressOverFlowException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			fail();
		} catch (TrapException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			fail();
		}
		
	}

}
