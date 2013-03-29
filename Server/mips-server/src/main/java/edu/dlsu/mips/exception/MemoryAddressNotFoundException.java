package edu.dlsu.mips.exception;

public class MemoryAddressNotFoundException extends Exception {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	public MemoryAddressNotFoundException(){
		System.err.println("Memory Address does not exists 0000 - FFFF only");
	}

}
