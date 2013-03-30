package edu.dlsu.mips.util;
import java.math.BigInteger;

import edu.dlsu.mips.exception.BitLengthException;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.exception.TrapException;

public class InstructionRunner {
	
	private static BigInteger upperLimit = BitStringUtils.hexToSignedDec("7FFFFFFFFFFFFFFF");
	private static BigInteger lowerLimit = BitStringUtils.hexToSignedDec("8000000000000000");
	
	public static String DADD(String rs, String rt) throws StorageInitializationException, 
	RegisterAddressOverFlowException, TrapException{
		String rsValue = Storage.getRegisterContents(rs);
		String rtValue = Storage.getRegisterContents(rt);
		BigInteger rsIntValue = null;
		BigInteger rtIntValue = null;
		if(BitStringUtils.isNegativeHex(rsValue)){
			rsValue = BitStringUtils.doTwosComplement(rsValue);
			rsIntValue = new BigInteger(rsValue, 16);
			rsIntValue = rsIntValue.negate();
		}else{
			rsIntValue =  BitStringUtils.hexToSignedDec(rsValue);
		}
		if(BitStringUtils.isNegativeHex(rtValue)){
			rtValue = BitStringUtils.doTwosComplement(rtValue);
			rtIntValue = new BigInteger(rtValue, 16);
			rtIntValue = rtIntValue.negate();
		}else{
			rtIntValue =  BitStringUtils.hexToSignedDec(rtValue);
		}
		BigInteger sum = rsIntValue.add(rtIntValue);
		if(sum.compareTo(upperLimit)==1 || sum.compareTo(lowerLimit)==-1){
			throw new TrapException("An register overflow occured due to DADD");
		};
		
		
		if(sum.compareTo(new BigInteger("-1"))==-1){
			BigInteger base = new BigInteger("2");
			BigInteger baseValue = base.pow(64);
			sum = baseValue.add(sum);
		}
		try {
			String sumHexValue = BitStringUtils.doSignedHexSignExtend(sum.toString(16), 64);
			return sumHexValue;
		} catch (BitLengthException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}
	public static String DSUB(String rs, String rt) throws StorageInitializationException, RegisterAddressOverFlowException, TrapException{
		String rsValue = Storage.getRegisterContents(rs);
		String rtValue = Storage.getRegisterContents(rt);
		BigInteger rsIntValue = null;
		BigInteger rtIntValue = null;
		if(BitStringUtils.isNegativeHex(rsValue)){
			rsValue = BitStringUtils.doTwosComplement(rsValue);
			rsIntValue = new BigInteger(rsValue, 16);
			rsIntValue = rsIntValue.negate();
		}else{
			rsIntValue =  BitStringUtils.hexToSignedDec(rsValue);
		}
		if(BitStringUtils.isNegativeHex(rtValue)){
			rtValue = BitStringUtils.doTwosComplement(rtValue);
			rtIntValue = new BigInteger(rtValue, 16);
			rtIntValue = rtIntValue.negate();
		}else{
			rtIntValue =  BitStringUtils.hexToSignedDec(rtValue);
		}
		BigInteger difference = rsIntValue.subtract(rtIntValue);
		if(difference.compareTo(upperLimit)==1 || difference.compareTo(lowerLimit)==-1){
			throw new TrapException("An register overflow occured due to DSUB");
		};
		if(difference.compareTo(new BigInteger("-1"))==-1){
			BigInteger base = new BigInteger("2");
			BigInteger baseValue = base.pow(64);
			difference = baseValue.add(difference);
		}
		try {
			String sumHexValue = BitStringUtils.doSignedHexSignExtend(difference.toString(16), 64);
			return sumHexValue;
		} catch (BitLengthException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}
	
	public static String AND(String rs, String rt) throws StorageInitializationException, RegisterAddressOverFlowException{
		String rsValue = Storage.getRegisterContents(rs);
		String rtValue = Storage.getRegisterContents(rt);
		String rsBinValue = BitStringUtils.convertHexToBinary(rsValue);
		String rtBinValue = BitStringUtils.convertHexToBinary(rtValue);
		String binResult = "";
		for(int i=0; i<rsBinValue.length(); i++){
			if(rsBinValue.charAt(i)==rtBinValue.charAt(i)){
				binResult = binResult + "0";
			}else{
				binResult = binResult + "1";
			}
		}
		String hexResult = BitStringUtils.convertBinaryToHex(binResult);
		return  hexResult;
	}
	
	public static String XOR(String rs, String rt) throws StorageInitializationException, RegisterAddressOverFlowException{
		String rsValue = Storage.getRegisterContents(rs);
		String rtValue = Storage.getRegisterContents(rt);
		String rsBinValue = BitStringUtils.convertHexToBinary(rsValue);
		String rtBinValue = BitStringUtils.convertHexToBinary(rtValue);
		String binResult = "";
		for(int i=0; i<rsBinValue.length(); i++){
			if(rsBinValue.charAt(i)==rtBinValue.charAt(i)){
				binResult = binResult + rsBinValue.charAt(i);
			}else{
				binResult = binResult + "0";
			}
		}
		String hexResult = BitStringUtils.convertBinaryToHex(binResult);
		return  hexResult;
	}
	
	public static String SLT(String rs, String rt){
		
		return "";
	}
	
	public static String J(String targetPc){
		return "";
	}
	
	public static String DADDI(String rs, String imm){
		return "";
	}
	
	public static String BNEZ(String rs, String currentPc, String targetPc){
		return "";
	}
	

}
