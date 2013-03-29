package edu.dlsu.mips.exception;

public class JumpAddressException extends Exception {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	public JumpAddressException(){
		System.err.println("Jump Address must be divisible by 4");
	}

}
