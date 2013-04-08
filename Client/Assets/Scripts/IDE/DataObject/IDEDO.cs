using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IDEDO {
	public List<ErrorDC> errorList = new List<ErrorDC>();
	public List<InstructionDC> instrList = new List<InstructionDC>();
	public List<string> tempInstList = new List<string>();
	
	public string opcodeContent;
	public string memoryContent;
	public string registerContent;
	public string pipelineMapContent;
	
	public int currentInstIndex = 0;
	public TabType currentTab;
	public bool hasBeenProcessed = false;
	
	public bool memoryHasBeenRequested = false;
	public bool registerHasBeenRequested = false;
	public bool pipelineHasBeenRequested = false;
	public bool opcodeHasBeenRequested = false;
}
