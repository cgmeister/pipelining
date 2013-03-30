using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IDEMediator : MonoBehaviour {
	
	private IDEGUIVC _ideGUIVC;
	private RegisterView _registerView;
	private	PipelineMapView _pipelineView;
	private OpcodeView _opcodeView;
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
		int len = 3;
		for (int x = 0; x<len; x++){
			_tabList.Add(false);
		}
		_tabList[0] = true;
	}
	
	private void addListeners(){
		_opcodeView.opcodeEmissary.add(onOpcodeButtonTabClicked);
		_pipelineView.pipelineMapEmissary.add(onPipelineButtonTabClicked);
		_registerView.registerEmissary.add(onRegisterButtonTabClicked);
	}
	
	private void removeListeners(){
		_opcodeView.opcodeEmissary.remove(onOpcodeButtonTabClicked);
		_pipelineView.pipelineMapEmissary.remove(onPipelineButtonTabClicked);
		_registerView.registerEmissary.remove(onRegisterButtonTabClicked);
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
	}
	
	private void initViewComponents(){
		_ideGUIVC = new IDEGUIVC();
		_ideGUIVC.initGUIComponents();
		
		_inputView = gameObject.AddComponent<InputView>();
		_inputView.init(_ideGUIVC.inputPanel, _ideGUIVC.singleButton, _ideGUIVC.fullButton, _ideGUIVC.camera, _ideGUIVC.inputBackHighlight);
		
		_registerView = gameObject.AddComponent<RegisterView>();
		_registerView.init(_ideGUIVC.outputPanel, _ideGUIVC.registerTabButton);
		
		_opcodeView = gameObject.AddComponent<OpcodeView>();
		_opcodeView.init(_ideGUIVC.outputPanel, _ideGUIVC.opcodeTabButton);
		
		_pipelineView = gameObject.AddComponent<PipelineMapView>();
		_pipelineView.init(_ideGUIVC.outputPanel, _ideGUIVC.pipelineTabButton);
		
		_errorView = gameObject.AddComponent<ErrorView>();
		_errorView.init(_ideGUIVC.errorPanel);
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
}