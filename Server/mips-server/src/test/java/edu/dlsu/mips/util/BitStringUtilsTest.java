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


}
