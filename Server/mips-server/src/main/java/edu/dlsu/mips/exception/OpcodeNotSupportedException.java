package edu.dlsu.mips.exception;

public class OpcodeNotSupportedException extends Exception{

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	public OpcodeNotSupportedException(){
		System.err.println("Opcode is not supported");
	}
	

}
