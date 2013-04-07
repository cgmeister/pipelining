package edu.dlsu.mips.dto;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedHashMap;

import org.apache.commons.logging.impl.AvalonLogger;
import org.codehaus.jackson.map.ObjectMapper.DefaultTyping;

import edu.dlsu.mips.domain.PipelineProcess;
import edu.dlsu.mips.domain.PipelineStage;
import edu.dlsu.mips.util.SystemUtils;

public class PipelineDTO {
	
	private static PipelineDTO pipelineDTO;
	
	static{
		pipelineDTO = new PipelineDTO();
	}
	
	public static PipelineDTO getInstance(){
		return pipelineDTO;
	}
	
	private LinkedHashMap<String, ArrayList<PipelineStage>> pipelineMap;
	/*
	public PipelineDTO(){
		pipelineMap = new LinkedHashMap<String, ArrayList<PipelineStage>>();
	}*/
	
	public void updatePipelineStages(){
		
		ArrayList<PipelineStage> arrPipelineStage;
		Collection<PipelineProcess> activeProcess = SystemUtils.getActiveProcesses();
		Iterator iter = activeProcess.iterator();
		while(iter.hasNext()){
			PipelineProcess pipelineProcess = (PipelineProcess) iter.next();
			PipelineStage pipelineStage = decrementStage(pipelineProcess.getPipelineStage());
			arrPipelineStage = pipelineMap.get(pipelineProcess.getInstructionSet().getHexInstruction());
			if(arrPipelineStage == null){
				arrPipelineStage = new ArrayList<PipelineStage>();
				arrPipelineStage.add(pipelineStage);
				pipelineMap.put(pipelineProcess.getInstructionSet().getHexInstruction(), arrPipelineStage);
			} else {
				arrPipelineStage.add(pipelineStage);
				pipelineMap.put(pipelineProcess.getInstructionSet().getHexInstruction(), arrPipelineStage);
			}
		}
		
		//arrPipelineStage = pipelineMap.get(pipelineProcess.getInstructionSet().getHexInstruction());
		
		
		
		Collection<PipelineProcess> allProcess = SystemUtils.getAllProcesses();
		
		iter = allProcess.iterator(); 
		while(iter.hasNext()){
			PipelineProcess pipelineProcess = (PipelineProcess) iter.next();
			arrPipelineStage = pipelineMap.get(pipelineProcess.getInstructionSet().getHexInstruction());
			if(arrPipelineStage != null){
				if(!activeProcess.contains(pipelineProcess)){
					arrPipelineStage.add(PipelineStage.WB);
					pipelineMap.put(pipelineProcess.getInstructionSet().getHexInstruction(), arrPipelineStage);
				}
			}
			
		}
		 
	}
	
	
	
	
	public LinkedHashMap<String, ArrayList<PipelineStage>> getPipelineMap(){
		return pipelineMap;
	}
	
	private PipelineStage decrementStage(PipelineStage pipelineStage) {
		if (pipelineStage.equals(PipelineStage.ID)) {
			pipelineStage = PipelineStage.IF;
		} else if (pipelineStage.equals(PipelineStage.EXE)) {
			pipelineStage = PipelineStage.ID;
		} else if (pipelineStage.equals(PipelineStage.MEM)) {
			pipelineStage = PipelineStage.EXE;
		} else if (pipelineStage.equals(PipelineStage.WB)) {
			pipelineStage = PipelineStage.MEM;
		}
		return pipelineStage;
	}
	
	public void Initialize(){
		pipelineMap = new LinkedHashMap<String, ArrayList<PipelineStage>>();
	}
	
	/**
	private PipelineDTO(PipelineStage stage) {
		this.stage = stage;
	}
	
	public static PipelineDTO newInstance(PipelineStage stage) {
		return new PipelineDTO(stage);
	}

	public PipelineStage getStage() {
		return stage;
	}
	*/
}
