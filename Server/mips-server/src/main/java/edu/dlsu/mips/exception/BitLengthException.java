package edu.dlsu.mips.exception;

public class BitLengthException extends Exception {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	public BitLengthException() {
		System.err.println("Bit Length must be divisible by 4");
	}
	
}
