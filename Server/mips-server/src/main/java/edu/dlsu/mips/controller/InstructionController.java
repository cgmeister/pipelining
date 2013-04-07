package edu.dlsu.mips.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import edu.dlsu.mips.domain.Instruction;
import edu.dlsu.mips.domain.PipelineProcess;
import edu.dlsu.mips.dto.InstructionDTO;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.MemoryAddressOverFlowException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;
import edu.dlsu.mips.exception.TrapException;
import edu.dlsu.mips.processor.PipelineProcessor;
import edu.dlsu.mips.util.SystemUtils;

@Controller
@RequestMapping("/Instruction")
public class InstructionController {
	
	private PipelineProcessor pipelineProcessor;
	private Instruction instruction;
	
	@Autowired
	public void setPipelineProcessor(PipelineProcessor pipelineProcessor){
		this.pipelineProcessor = pipelineProcessor;
	}
	@Autowired
	public void setInstruction(Instruction instruction){
		this.instruction = instruction;
	}
	
	
	@RequestMapping(value = "/send", method = RequestMethod.GET)
	@ResponseBody
	public InstructionDTO sendInstruction(@RequestParam("instruction") String instruction){
		
		InstructionDTO instructionDTO = InstructionDTO.getInstance();
		instructionDTO.setInstruction(instruction);
		PipelineProcess pipelineProcess = PipelineProcess.newInstance(instructionDTO.getInstructionSet());
		
		this.instruction.setInstruction(instruction);
		//SystemUtils.addActiveProcess(pipelineProcess);
		try {
			pipelineProcessor.processInstruction(this.instruction);
		} catch (JumpAddressException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (OperandException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (StorageInitializationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (RegisterAddressOverFlowException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (TrapException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (OpcodeNotSupportedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (MemoryAddressOverFlowException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return instructionDTO;
		
	}
	
}
