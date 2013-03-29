package edu.dlsu.mips.util;

import edu.dlsu.mips.exception.BitLengthException;
import edu.dlsu.mips.util.BitStringUtils;
import junit.framework.TestCase;


public class BitStringUtilsTest extends TestCase {
	
	public void testSignExtend(){
		try {
			assertEquals("FFFFFFFFFFFFFFF8", BitStringUtils.doSignedHexSignExtend("8", 64));
			assertEquals("00101", BitStringUtils.doUnsignedBinarySignExtention(5, 5));
		} catch (BitLengthException e) {
			fail();
		}
	}


}
