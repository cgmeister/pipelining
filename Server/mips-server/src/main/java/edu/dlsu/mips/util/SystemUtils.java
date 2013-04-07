package edu.dlsu.mips.util;

import java.util.Collection;

import com.google.common.collect.Lists;

import edu.dlsu.mips.domain.PipelineProcess;
import edu.dlsu.mips.domain.PipelineStage;

public class SystemUtils {

	public static int clockCycle = 0;
	private static Collection<PipelineProcess> activeProcesses;
	private static Collection<PipelineProcess> allProcesses;
	
	public static void addToAllProcess(PipelineProcess pipelineProcess) {
		allProcesses.add(pipelineProcess);
	}
	
	public static void addActiveProcess(PipelineProcess pipelineProcess) {
		activeProcesses.add(pipelineProcess);
	}
	
	public static void removeFromActiveProcess(PipelineStage pipelineStage) {
		Collection<PipelineProcess> newProcesses = Lists.newArrayList();
		for (PipelineProcess process : activeProcesses) {
			if (!process.getPipelineStage().equals(pipelineStage)) {
				newProcesses.add(process);
			}
		}
		activeProcesses = newProcesses;
	}
	
	public static PipelineProcess retrieveActiveProcess(PipelineStage pipelineStage) {
		for (PipelineProcess process : activeProcesses) {
			if (process.getPipelineStage().equals(pipelineStage)) {
				return process;
			}
		}
		return null;
	}
	
	public static boolean hasActiveProcess() {
		return !activeProcesses.isEmpty();
	}
	
	public static String incrementPc(String currentPc){
		int pcIntVal = Integer.parseInt(currentPc,2);
		pcIntVal += 4;
		return Integer.toBinaryString(pcIntVal);
	}

}
