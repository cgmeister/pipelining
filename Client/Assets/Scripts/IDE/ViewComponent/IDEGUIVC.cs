using UnityEngine;
using System.Collections;

public class IDEGUIVC {
	
	public const string OUTPUT_ERRORPANEL = "Error Panel";
	public const string OUTPUT_TABPANEL = "Output Panel";
	public const string INPUT_TABPANEL = "Input Panel";
	public const string INPUT_BACKHIGHLIGHT = "Highlight Background";
	
	public const string BUTTON_OPCODETAB = "Opcode Tab";
	public const string BUTTON_REGISTERTAB = "Register Tab";
	public const string BUTTON_PIPELINETAB = "Pipeline Tab";
	public const string BUTTON_MEMORYTAB = "Memory Tab";
	
	public const string BUTTON_SINGLE = "Single Line Button";
	public const string BUTTON_FULL = "Full Button";
	public const string BUTTON_RESET = "Reset Button";
	
	public const string INPUT_MEMORYSTART = "Memory Start Input";
	public const string INPUT_MEMORYEND = "Memory End Input";
	public const string BUTTON_MEMORYREQUESTBUTTON = "Memory Request Button";
	
	public const string CAMERA = "Camera";
	
	public GameObject errorPanel;
	public GameObject inputPanel;
	public GameObject outputPanel;
	public GameObject opcodeTabButton;
	public GameObject registerTabButton;
	public GameObject pipelineTabButton;
	public GameObject memoryTabButton;
	public GameObject singleButton;
	public GameObject fullButton;
	public GameObject resetButton;
	public GameObject inputBackHighlight;
	public GameObject camera;
	public GameObject memoryStartInput;
	public GameObject memoryEndInput;
	public GameObject requestButton;
	
	public void initGUIComponents(){
		errorPanel = GameObject.Find(OUTPUT_ERRORPANEL);
		inputPanel = GameObject.Find(INPUT_TABPANEL);
		outputPanel = GameObject.Find(OUTPUT_TABPANEL);
		
		opcodeTabButton = GameObject.Find(BUTTON_OPCODETAB);
		pipelineTabButton = GameObject.Find(BUTTON_PIPELINETAB);
		registerTabButton = GameObject.Find(BUTTON_REGISTERTAB);
		memoryTabButton = GameObject.Find(BUTTON_MEMORYTAB);
		
		singleButton = GameObject.Find(BUTTON_SINGLE);
		fullButton = GameObject.Find(BUTTON_FULL);
		resetButton = GameObject.Find(BUTTON_RESET);
		
		memoryStartInput = GameObject.Find(INPUT_MEMORYSTART);
		memoryEndInput = GameObject.Find(INPUT_MEMORYEND);
		requestButton = GameObject.Find(BUTTON_MEMORYREQUESTBUTTON);
		
		inputBackHighlight = GameObject.Find(INPUT_BACKHIGHLIGHT);
		
		camera = GameObject.Find(CAMERA);
	}
}
