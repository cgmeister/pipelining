package edu.dlsu.mips.util;

import java.math.BigInteger;

import edu.dlsu.mips.exception.BitLengthException;

public class BitStringUtils {

	public static String doSignedHexSignExtend(String hex, int targetBinaryBits)
			throws BitLengthException {
		if (targetBinaryBits % 4 == 0) {
			String addressPad = "";
			String convertedValue = "";
			for (int j = hex.length() - 1; j >= 0; j--) {
				int intValue = Integer.parseInt(
						Character.toString(hex.charAt(j)), 16);
				String binaryValue = Integer.toBinaryString(intValue);
				for (int i = 0; i < (4 - binaryValue.length()); i++) {
					addressPad += "0";
				}
				convertedValue = addressPad + binaryValue + convertedValue;
			}
			addressPad = "";
			for (int i = 0; i < (targetBinaryBits - convertedValue.length()); i++) {
				addressPad += convertedValue.charAt(0);
			}
			String paddedValue = addressPad + convertedValue;
			BigInteger binaryIntVal = new BigInteger(paddedValue, 2);
			String finalHexString = Long.toHexString(binaryIntVal.longValue());
			if (finalHexString.length() != (targetBinaryBits / 4)) {
				addressPad = "";
				for (int i = 0; i < ((targetBinaryBits / 4) - finalHexString
						.length()); i++) {
					addressPad += "0";
				}
				finalHexString = addressPad + finalHexString;
			}
			return finalHexString.toUpperCase();
		} else {
			throw new BitLengthException();
		}
	}

	public static String doUnsignedHexSignExtension(String hex,
			int targetBinaryBits) throws BitLengthException {
		if (targetBinaryBits % 4 == 0) {
			String padding = "";
			for (int i = 0; i < (targetBinaryBits / 4) - hex.length(); i++) {
				padding += "0";
			}
			return padding + hex;
		} else {
			throw new BitLengthException();
		}
	}

	public static String doUnsignedBinarySignExtention(int number,
			int targetBinaryBits) {
		String binaryString = Integer.toBinaryString(number);
		String padding = "";
		for (int i = 0; i < (targetBinaryBits - binaryString.length()); i++) {
			padding += "0";
		}
		return binaryString = padding + binaryString;
	}

}
