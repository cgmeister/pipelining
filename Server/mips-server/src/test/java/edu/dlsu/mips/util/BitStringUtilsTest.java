package edu.dlsu.mips.util;

import edu.dlsu.mips.exception.BitLengthException;
import edu.dlsu.mips.util.BitStringUtils;
import junit.framework.TestCase;


public class BitStringUtilsTest extends TestCase {
	
	public void testSignExtend() throws BitLengthException{
			assertEquals("FFFFFFFFFFFFFFF8", BitStringUtils.doSignedHexSignExtend("8", 64));
			assertEquals("00101", BitStringUtils.doUnsignedBinarySignExtention(5, 5));
	}
	public void testTwosComplement(){
		BitStringUtils.doTwosComplement("C000000000000001");
	
	}
	
	public void testHexToBinaryConverter(){
		BitStringUtils.convertHexToBinary("0000000000000000");
	}
	
	public void testBinaryToHexConverter(){
		BitStringUtils.convertBinaryToHex("1111111111111111111111101111111111111111111111111111111111111111");
	}
	
	public void testShiftOperations(){
		assertEquals("00001100", BitStringUtils.shiftLeft("00000011", 2));
		assertEquals("00110000", BitStringUtils.shiftRight("01100000", 1));
		assertEquals("00000000", BitStringUtils.shiftLeft("00000011", 21));
		assertEquals("00000000", BitStringUtils.shiftRight("01100000", 11));
	}


}
