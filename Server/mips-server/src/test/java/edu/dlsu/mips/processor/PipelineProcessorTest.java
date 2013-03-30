package edu.dlsu.mips.processor;

import org.junit.Ignore;
import org.junit.Test;
import org.mockito.Mockito;

import edu.dlsu.mips.domain.Instruction;
import edu.dlsu.mips.domain.InstructionSet;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.exception.TrapException;
import edu.dlsu.mips.processor.impl.PipelineProcessorImpl;
import edu.dlsu.mips.util.InstructionSetBuilder;

public class PipelineProcessorTest {

	@Test
	@Ignore
	public void shouldBuildOpcode() throws JumpAddressException, OperandException, StorageInitializationException, 
	RegisterAddressOverFlowException, TrapException, OpcodeNotSupportedException{
		PipelineProcessor pipelineProcessor = new PipelineProcessorImpl();
		Instruction instruction = Mockito.mock(Instruction.class);
		
		pipelineProcessor.processInstruction(instruction);
		InstructionSet actual = InstructionSetBuilder.buildOpcode(instruction.getInstruction());
	}

}
