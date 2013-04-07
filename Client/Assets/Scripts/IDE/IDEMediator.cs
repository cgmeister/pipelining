using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IDEMediator : MonoBehaviour {
	
	private IDEGUIVC _ideGUIVC;
	private RegisterView _registerView;
	private	PipelineMapView _pipelineView;
	private OpcodeView _opcodeView;
	private MemoryView _memoryView;
	private InputView _inputView;
	private ErrorView _errorView;
	
	private List<bool> _tabList;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void init(){
		initViewComponents();
		initTabList();
		addListeners();
	}
	
	private void initTabList(){
		_tabList = new List<bool>();
		int len = 4;
		for (int x = 0; x<len; x++){
			_tabList.Add(false);
		}
		_tabList[0] = true;
	}
	
	private void addListeners(){
		_opcodeView.opcodeEmissary.add(onOpcodeButtonTabClicked);
		_pipelineView.pipelineMapEmissary.add(onPipelineButtonTabClicked);
		_registerView.registerEmissary.add(onRegisterButtonTabClicked);
		_memoryView.memoryEmissary.add(onMemoryTabClicked);
		_inputView.resetButtonEmissary.add(onResetButtonClicked);
		
		IDEEmissaryList.memoryRequestButton.add(onMemoryRequest);
	}
	
	private void removeListeners(){
		_opcodeView.opcodeEmissary.remove(onOpcodeButtonTabClicked);
		_pipelineView.pipelineMapEmissary.remove(onPipelineButtonTabClicked);
		_registerView.registerEmissary.remove(onRegisterButtonTabClicked);
		_memoryView.memoryEmissary.remove(onMemoryTabClicked);
		_inputView.resetButtonEmissary.remove(onResetButtonClicked);
		
		IDEEmissaryList.memoryRequestButton.remove(onMemoryRequest);
	}
	
	private void onResetButtonClicked(){
		_errorView.setErrorLabel("Error logs will show here...");
	}
	
	private void onMemoryRequest(int start, int end){
		Debug.Log("Memory request sent: " + start + " - " + end);
	}
	
	private void onMemoryTabClicked(){
		tabItemEnabled((int)TabType.MEMORY);
		actDeactTabs();
	}
	
	private void onOpcodeButtonTabClicked(){
		tabItemEnabled((int)TabType.OPCODE);
		actDeactTabs();
	}
	
	private void onPipelineButtonTabClicked(){
		tabItemEnabled((int)TabType.PIPELINEMAP);
		actDeactTabs();
	}
	
	private void onRegisterButtonTabClicked(){
		tabItemEnabled((int)TabType.REGISTER);
		actDeactTabs();
	}
	
	private void tabItemEnabled(int index){
		int len = _tabList.Count;
		for (int x = 0; x<len; x++){
			_tabList[x] = index == x ? true : false;
		}
	}
	
	private void actDeactTabs(){
		_opcodeView.setTabButtonActive(_tabList[(int)TabType.OPCODE]);
		_pipelineView.setTabButtonActive(_tabList[(int)TabType.PIPELINEMAP]);
		_registerView.setTabButtonActive(_tabList[(int)TabType.REGISTER]);
		_memoryView.setTabButtonActive(_tabList[(int)TabType.MEMORY]);
		_memoryView.setInputActivity(_tabList[(int)TabType.MEMORY]);
	}
	
	private void initViewComponents(){
		_ideGUIVC = new IDEGUIVC();
		_ideGUIVC.initGUIComponents();
		
		_inputView = gameObject.AddComponent<InputView>();
		_inputView.init(_ideGUIVC.inputPanel, _ideGUIVC.singleButton, _ideGUIVC.fullButton, _ideGUIVC.camera, _ideGUIVC.inputBackHighlight, _ideGUIVC.resetButton, _ideGUIVC.errorOnCompile);
		
		_registerView = gameObject.AddComponent<RegisterView>();
		_registerView.init(_ideGUIVC.outputPanel, _ideGUIVC.registerTabButton);
		
		_opcodeView = gameObject.AddComponent<OpcodeView>();
		_opcodeView.init(_ideGUIVC.outputPanel, _ideGUIVC.opcodeTabButton);
		
		_pipelineView = gameObject.AddComponent<PipelineMapView>();
		_pipelineView.init(_ideGUIVC.outputPanel, _ideGUIVC.pipelineTabButton);
		
		_errorView = gameObject.AddComponent<ErrorView>();
		_errorView.init(_ideGUIVC.errorPanel);
		
		_memoryView = gameObject.AddComponent<MemoryView>();
		_memoryView.init(_ideGUIVC.outputPanel, _ideGUIVC.memoryTabButton, _ideGUIVC.memoryStartInput, _ideGUIVC.memoryEndInput, _ideGUIVC.requestButton);
	}
	
	public void destroy(){ 
		removeListeners(); 
		destroyViews(); 
	} 
	
	public void destroyViews(){ 
		_opcodeView.destroy(); 
		DestroyImmediate(_opcodeView);  
		_errorView.destroy(); 
		DestroyImmediate(_errorView); 
		_pipelineView.destroy(); 
		DestroyImmediate(_pipelineView); 
		_registerView.destroy(); 
		DestroyImmediate(_registerView); 
		_inputView.destroy(); 
		DestroyImmediate(_inputView); 
	}
	
	public void updateErrorLogs(List<ErrorDC> errorList){ 
		int len = errorList.Count;
		string str = ""; 
		for (int x=0; x<len; x++){ 
			ErrorType errType = errorList[x].errorType; 
			str += errType.ToString(); 
			
			if (errType == ErrorType.ERROR_0020_REGISTER || errType == ErrorType.ERROR_0040_OFFSET || errType == ErrorType.ERROR_0030_IMMEDIATE){
				str += " : Line number " + errorList[x].lineNum; 
				str += " : Parameter number " + errorList[x].paramNum + ".\n";
			} else if(errType == ErrorType.ERROR_0080_SINGLEEXECUTION || errType == ErrorType.ERROR_0081_SINGLEEXECUTION_ERRORPERSISTS || errType == ErrorType.ERROR_0090_FULLEXECUTION || errType == ErrorType.ERROR_0091_FULLEXECUTION_ERRORPERSISTS){
				str += ".\n";
			} else {
				str += " : Line number " + errorList[x].lineNum; 
				str += ".\n";
			}
		}
		_errorView.setErrorLabel(str);
	}
	
	public void updateViewLabel(TabType type, string str){
		switch(type){
			case TabType.MEMORY:{
				_memoryView.setmemoryLabel(str);
				break;
			}
			case TabType.OPCODE:{
				_opcodeView.setOpcodeLabel(str);
				break;
			}
			case TabType.PIPELINEMAP:{
				_pipelineView.setPipelinMapLabel(str);
				break;
			}
			case TabType.REGISTER:{
				_registerView.setRegisterLabel(str);
				break;
			}
		}
	}
}