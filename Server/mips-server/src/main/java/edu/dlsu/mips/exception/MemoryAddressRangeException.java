package edu.dlsu.mips.exception;

public class MemoryAddressRangeException extends Exception {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	public MemoryAddressRangeException(){
		System.err.println("Memory Start Range cannot be Greater than End Range");
	}
}
