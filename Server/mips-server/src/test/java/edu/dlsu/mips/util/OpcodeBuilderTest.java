package edu.dlsu.mips.util;

import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.util.OpcodeBuilder;
import junit.framework.TestCase;

public class OpcodeBuilderTest extends TestCase {
	
	public void testOpcodeBuilder(){
		try {
			try {
				OpcodeBuilder.buildOpcode("J C");
				OpcodeBuilder.buildOpcode("XOR R5, R1, R3");
				OpcodeBuilder.buildOpcode("DADD R5, R1, R3");
				OpcodeBuilder.buildOpcode("DADDI R3, R0, #1002");
				OpcodeBuilder.buildOpcode("LD R1, 1000(R2)");
				OpcodeBuilder.buildOpcode("BNEZ R5, C, 14");
			} catch (OperandException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
		} catch (JumpAddressException e) {
			// TODO Auto-generated catch block
			fail();
			e.printStackTrace();
		}
	}

}
