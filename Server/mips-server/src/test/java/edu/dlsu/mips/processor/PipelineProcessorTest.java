package edu.dlsu.mips.processor;

import org.junit.Test;
import org.mockito.Mockito;

import edu.dlsu.mips.domain.Instruction;
import edu.dlsu.mips.domain.Opcode;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.processor.impl.PipelineProcessorImpl;
import edu.dlsu.mips.util.OpcodeBuilder;

public class PipelineProcessorTest {

	@Test
	public void shouldBuildOpcode() throws JumpAddressException, OperandException {
		PipelineProcessor pipelineProcessor = new PipelineProcessorImpl();
		Instruction instruction = Mockito.mock(Instruction.class);
		
		pipelineProcessor.processInstruction(instruction);
		Opcode actual = OpcodeBuilder.buildOpcode(instruction.getInstruction());
	}

}
