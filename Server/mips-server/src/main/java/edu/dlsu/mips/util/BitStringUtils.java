package edu.dlsu.mips.util;

import java.math.BigInteger;

import edu.dlsu.mips.exception.BitLengthException;

public class BitStringUtils {

<<<<<<< HEAD
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
=======

	public static String doSignedHexSignExtend(String hex, int targetBinaryBits) throws BitLengthException{
		if(targetBinaryBits%4==0){
			boolean isNegative =
					hex.startsWith("8") || hex.startsWith("9") ||
					hex.startsWith("A") || hex.startsWith("B") ||
					hex.startsWith("C") || hex.startsWith("D") ||
					hex.startsWith("E") || hex.startsWith("F");
			int padding = (targetBinaryBits/4) - hex.length();
			String sign = "0";
			if(isNegative){
				sign = "F";
			}
			String signPadding = "";
			for(int i=0; i<padding; i++){
				signPadding += sign;
>>>>>>> fe81a347ef993325c99626fd2cd17f2acfb7dab9
			}
			String finalHexString = signPadding+hex;
			return finalHexString.toUpperCase();
		} else {
			throw new BitLengthException();
		}
	}

<<<<<<< HEAD
	public static String doUnsignedHexSignExtension(String hex,
			int targetBinaryBits) throws BitLengthException {
		if (targetBinaryBits % 4 == 0) {
=======
	public static String doUnsignedHexSignExtension(String hex, int targetBinaryBits) throws BitLengthException{
		if(targetBinaryBits%4==0){
>>>>>>> fe81a347ef993325c99626fd2cd17f2acfb7dab9
			String padding = "";
			for (int i = 0; i < (targetBinaryBits / 4) - hex.length(); i++) {
				padding += "0";
			}
			return padding + hex;
		} else {
			throw new BitLengthException();
		}
	}

<<<<<<< HEAD
	public static String doUnsignedBinarySignExtention(int number,
			int targetBinaryBits) {
=======
	public static String doUnsignedBinarySignExtention(int number, int targetBinaryBits){
>>>>>>> fe81a347ef993325c99626fd2cd17f2acfb7dab9
		String binaryString = Integer.toBinaryString(number);
		String padding = "";
		for (int i = 0; i < (targetBinaryBits - binaryString.length()); i++) {
			padding += "0";
		}
		return binaryString = padding + binaryString;
	}


	public static BigInteger hexToSignedDec(String hex)  {
		if (hex == null) {
			throw new NullPointerException("hexToDec: hex String is null.");
		}

		// If you want to pad "FFF" to "0FFF" do it here.

		hex = hex.toUpperCase();

		// Check if high bit is set.
		boolean isNegative =
				hex.startsWith("8") || hex.startsWith("9") ||
				hex.startsWith("A") || hex.startsWith("B") ||
				hex.startsWith("C") || hex.startsWith("D") ||
				hex.startsWith("E") || hex.startsWith("F");

		BigInteger temp;

		if (isNegative) {
			// Negative number
			temp = new BigInteger(hex, 16);
			BigInteger subtrahend = BigInteger.ONE.shiftLeft(hex.length() * 4);
			temp = temp.subtract(subtrahend);
		} else {
			// Positive number
			temp = new BigInteger(hex, 16);
		}


		return temp;
	}


	public static boolean isNegativeHex(String hex){

		boolean isNegative =
				hex.startsWith("8") || hex.startsWith("9") ||
				hex.startsWith("A") || hex.startsWith("B") ||
				hex.startsWith("C") || hex.startsWith("D") ||
				hex.startsWith("E") || hex.startsWith("F");	 
		return isNegative;
	}

	public static String doTwosComplement(String hex){
		String binaryString = "";
		for(int i=0; i<hex.length(); i++){
			int intVal = Integer.parseInt(Character.toString(hex.charAt(i)), 16);
			String binaryVal = Integer.toBinaryString(intVal);
			String padding = "";
			for(int j=0; j<4-binaryVal.length(); j++){
				padding += "0";
			}
			binaryString += padding + binaryVal;
		}
		boolean hasFirstOne = false;
		String finalString = "";
		for(int i=binaryString.length()-1; i>=0; i--){
			String currentBit = Character.toString(binaryString.charAt(i));
			if(!hasFirstOne && currentBit.equalsIgnoreCase("1")){
				hasFirstOne = true;
				finalString = currentBit + finalString;
			}else if(hasFirstOne){
				if(currentBit.equalsIgnoreCase("0")){
					finalString = "1" + finalString;
				}else{
					finalString = "0" + finalString;
				}
			}else{
				finalString = currentBit + finalString;
			}
		}
		BigInteger intNum = new BigInteger(finalString, 2);
		String hexString = intNum.toString(16);
		try {
			hexString = BitStringUtils.doUnsignedHexSignExtension(hexString, 16);
		} catch (BitLengthException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return hexString.toUpperCase();
	}

	public static String convertHexToBinary(String hex) {
		String finalBitString = "";
		for(int i=0; i<hex.length(); i++){
			int intVal = Integer.parseInt(Character.toString(hex.charAt(i)), 16);
			String binaryVal = Integer.toBinaryString(intVal);
			String padding = "";
			for(int j=0; j<4-binaryVal.length(); j++){
				padding += "0";
			}
			finalBitString += padding + binaryVal;
		}
		return finalBitString;
	}
	
	public static String convertBinaryToHex(String binary) {
		String finalHexString = "";
		for(int i=0; i<binary.length(); i+=4){
			String binaryValue = binary.substring(i, i+4);
			int intVal = Integer.parseInt(binaryValue,2);
			String hexVal = Integer.toHexString(intVal);
			finalHexString += hexVal;
		};
		return finalHexString.toUpperCase();
	}

}