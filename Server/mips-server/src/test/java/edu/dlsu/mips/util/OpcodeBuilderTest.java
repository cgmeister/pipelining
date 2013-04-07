package edu.dlsu.mips.util;

import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.util.InstructionSetBuilder;
import junit.framework.TestCase;

public class OpcodeBuilderTest extends TestCase {
	
	public void testOpcodeBuilder(){
		try {
			try {
				InstructionSetBuilder.buildInstructionSet("J C");
				InstructionSetBuilder.buildInstructionSet("XOR R5, R1, R3");
				InstructionSetBuilder.buildInstructionSet("DADD R5, R1, R3");
				InstructionSetBuilder.buildInstructionSet("DADDI R3, R0, #1002");
				InstructionSetBuilder.buildInstructionSet("LD R1, 1000(R2)");
				InstructionSetBuilder.buildInstructionSet("BNEZ R5, C, 14");
			} catch (OperandException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (OpcodeNotSupportedException e) {
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
