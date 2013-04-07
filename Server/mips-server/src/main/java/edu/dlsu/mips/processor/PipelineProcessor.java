package edu.dlsu.mips.processor;

import edu.dlsu.mips.domain.Instruction;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.MemoryAddressOverFlowException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.exception.TrapException;

public interface PipelineProcessor {

	void processInstruction(Instruction instruction)
			throws JumpAddressException, OperandException,
			StorageInitializationException, RegisterAddressOverFlowException,
			TrapException, OpcodeNotSupportedException,
			MemoryAddressOverFlowException;
	
}
