package edu.dlsu.mips.util;

import edu.dlsu.mips.domain.InstructionSet;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.util.InstructionSetBuilder;
import junit.framework.TestCase;

public class OpcodeBuilderTest extends TestCase {
	
	public void testOpcodeBuilder(){
		try {
			try {
				InstructionSet is = InstructionSetBuilder.buildInstructionSet("J 4 1");
				System.out.println(is.getHexInstruction());
				System.out.println(is.getTargetLine());
				System.out.println(is.getBinaryInstruction());
				System.out.println(is.getInstructionLine());
				InstructionSetBuilder.buildInstructionSet("XOR R5, R1, R3 2");
				InstructionSetBuilder.buildInstructionSet("DADD R5, R1, R3 3");
				InstructionSetBuilder.buildInstructionSet("DADDI R3, R0, #1002 4");
				InstructionSetBuilder.buildInstructionSet("LD R1, 1000(R2) 5");
				is = InstructionSetBuilder.buildInstructionSet("BNEZ R5, 4, 6 6");
				System.out.println(is.getHexInstruction());
				System.out.println(is.getTargetLine());
				System.out.println(is.getBinaryInstruction());
				System.out.println(is.getInstructionLine());
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
