package edu.dlsu.mips.processor;

import edu.dlsu.mips.domain.Instruction;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OperandException;

public interface PipelineProcessor {

	void processInstruction(Instruction instruction) throws JumpAddressException, OperandException;
	
}
