using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IDEProxy {
	
	private IDEDO _ideDO;
	public IDEDO IdeDO {
		get {
			return this._ideDO;
		}
	}	
	
	public void init(){
		_ideDO = new IDEDO();
		_ideDO.errorList = new List<ErrorDC>();
		_ideDO.instrList = new List<InstructionDC>();
		_ideDO.tempInstList = new List<string[]>();
		
		_ideDO.currentTab = TabType.OPCODE;
	}

	public void addErrorLog(ErrorDC errItem){
		if (!checkIfHasErrorItem(errItem)){
			_ideDO.errorList.Add(errItem);
		}
	}
	
	private bool checkIfHasErrorItem(ErrorDC errItem){
		bool exists = false;
		int len = _ideDO.errorList.Count;
		for (int x=0; x<len; x++){
			if (_ideDO.errorList[x].errorType == errItem.errorType && _ideDO.errorList[x].lineNum == errItem.lineNum && _ideDO.errorList[x].paramNum == errItem.paramNum){
				exists = true;
			}
		}
		return exists;
	}
	
	public void resetErrorLogs(){
		_ideDO.errorList = new List<ErrorDC>();
	}
	
	public void updateInstrList(List<InstructionDC> instrList){
		_ideDO.instrList = instrList;
	}
	
	public void updateTempInstrList(List<string[]> tempIntrsList){
		_ideDO.tempInstList = tempIntrsList;
	}
	
	#region instruction requests
	
	public void requestRegister(){
		if (!_ideDO.registerHasBeenRequested){
			_ideDO.registerHasBeenRequested = true;
			Application.ExternalCall("requestRegister");
		} else {
			updatedRegister(_ideDO.registerContent);
		}
	}
	
	public void requestMemory(int startNum, int endNum){
		if (!_ideDO.memoryHasBeenRequested){
			_ideDO.memoryHasBeenRequested = true;
			Application.ExternalCall("requestMemory", startNum, endNum);
		} else {
			updatedMemory(_ideDO.memoryContent);
		}
	}
	
	public void requestPipelineMap(){
		if (!_ideDO.pipelineHasBeenRequested){
			_ideDO.pipelineHasBeenRequested = true;
			Application.ExternalCall("requestPipelineMap");
		} else {
			updatedPiplineMap(_ideDO.pipelineMapContent);
		}
	}
	
	public void requestOpcode(){
		if (!_ideDO.opcodeHasBeenRequested){
			_ideDO.opcodeHasBeenRequested = true;
			Application.ExternalCall("requestOpcode");
		} else {
			updatedPiplineMap(_ideDO.registerContent);
		}
	}
	
	public void sendInstruction(string str){
		Debug.Log("String raw: " + str);
		string jsonStr = encode(str);
		sendData(jsonStr);
		Debug.Log("Sent Single Instruction: " + jsonStr);
	}
	
	public void sendInstructionList(List<string[]> strList){
		List<string> newStrList = joinStringList(strList);
		int len = newStrList.Count;
		string jsnStr = "";
		string retrnJson = "";
		for (int x=0; x<len; x++){
			jsnStr = decode(newStrList[x]);
			sendData(jsnStr);
			IDEEmissaryList.updateDisplayTab.dispatch();
		}
	}
	
	private List<string> joinStringList(List<string[]> strList){
		List<string> newStrList= new List<string>();
		int len = newStrList.Count;
		for (int x=0; x<len; x++){
			string joinedStr = String.Join("", strList[x]);
			newStrList.Add(joinedStr);
		}
		return newStrList;
	}
	
	private void sendData(string str){
		Application.ExternalCall("instrOperation", str);
	}
	
	private string decode(string json){
		return Convert.ToString(MiniJSON.jsonDecode(json));
	}
	
	private string encode(string str){
		return MiniJSON.jsonEncode(str);
	}
	#endregion instruction requests
	
	#region method calls from javascript
	private void updatedMemory(string str){
		_ideDO.memoryContent = str;
		IDEEmissaryList.updateMemory.dispatch(str);
	}
	
	private void updatedOpcode(string str){
		_ideDO.opcodeContent = str;
		IDEEmissaryList.updateOpcode.dispatch(str);
	}
	
	private void updatedPiplineMap(string str){
		_ideDO.pipelineMapContent = str;
		IDEEmissaryList.updatePipeline.dispatch(str);
	}
	
	private void updatedRegister(string str){
		_ideDO.registerContent = str;
		IDEEmissaryList.updateRegister.dispatch(str);
	}
	#endregion method calls from javascript
	
	/// <summary>
	/// Called by javascript if instruction was successful.
	/// </summary>
	private void hasBeenProcessed(){
		_ideDO.hasBeenProcessed = true;
		setAllRequestToFalse();
	}
	
	private void setAllRequestToFalse(){
		_ideDO.memoryHasBeenRequested = false;
		_ideDO.pipelineHasBeenRequested = false;
		_ideDO.opcodeHasBeenRequested = false;
		_ideDO.registerHasBeenRequested = false;
	}
	
	public void setCurrentTab(TabType tab){
		_ideDO.currentTab = tab;
	}
	
	public void setCurrentIndex(int index){
		_ideDO.currentInstIndex = index;
	}
	
	public void setProcessed(bool val){
		_ideDO.hasBeenProcessed = val;
	}
	
	public void setInstrList(List<string[]> instList){
		_ideDO.tempInstList = instList;
	}
}
	