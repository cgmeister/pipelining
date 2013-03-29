package edu.dlsu.mips.exception;

public class RegisterAddressOverFlowException extends Exception {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	public RegisterAddressOverFlowException() {
		// TODO Auto-generated constructor stub
		System.err.println("Registers are only from 0 - 31 and should begin with the prefix R");
		
	}
	
}
