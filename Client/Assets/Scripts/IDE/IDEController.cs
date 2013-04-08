using UnityEngine;
using System.Collections;
using System;

public class IDEController : MonoBehaviour {
	
	private IDEProxy _ideProxy;
	private IDEMediator _ideMediator;
	
	private Validator _validatorLogic;
	
	private bool _errorOnCompile = true;
	private bool _highlightHidden = true;
	
	public void init(){
		initController();
		addListeners();
		setMockData();
	}
	
	private void setMockData(){
		_ideProxy.getMockJsonFile();
	}
	
	
	private void addListeners(){
		Debug.Log("Added Listeners");
		IDEEmissaryList.textChangedEmissary.add(onInputTextChanged);
		IDEEmissaryList.validationErrorEmissary.add(onValidationErrorFound);
		IDEEmissaryList.updateMemory.add(onMemoryUpdate);
		IDEEmissaryList.updateRegister.add(onRegisterUpdate);
		IDEEmissaryList.updateOpcode.add(onOpcodeUpdate);
		IDEEmissaryList.updatePipeline.add(onPipelineUpdate);
		IDEEmissaryList.singleButtonClickEmissary.add(onSingleButtonClick);
		IDEEmissaryList.fullButtonClickEmissary.add(onFullButtonClick);
		IDEEmissaryList.errorOnCompileButtonClickEmissary.add(onCheckBoxClick);
	}
	
	private void removeListeners(){
		IDEEmissaryList.textChangedEmissary.remove(onInputTextChanged);
		IDEEmissaryList.validationErrorEmissary.remove(onValidationErrorFound);
		IDEEmissaryList.updateMemory.remove(onMemoryUpdate);
		IDEEmissaryList.updateRegister.remove(onRegisterUpdate);
		IDEEmissaryList.updateOpcode.remove(onOpcodeUpdate);
		IDEEmissaryList.updatePipeline.remove(onPipelineUpdate);
		IDEEmissaryList.singleButtonClickEmissary.remove(onSingleButtonClick);
		IDEEmissaryList.fullButtonClickEmissary.remove(onFullButtonClick);
		IDEEmissaryList.errorOnCompileButtonClickEmissary.remove(onCheckBoxClick);
	}
	
	private void onCheckBoxClick(bool val){
		_errorOnCompile = val;
	}
	
	private void onSingleButtonClick(string inputStr){
		Debug.Log("Instruction String: " + inputStr);
		checkTimeToValidate(inputStr, true);
		if (_ideProxy.IdeDO.errorList.Count == 0 && inputStr.Length > 0){
			//Debug.Log(_ideProxy.IdeDO.tempInstList[_ideProxy.IdeDO.currentInstIndex]);
			_ideMediator.resetErrorLog();
			if (_highlightHidden) {
				Debug.Log("show");
				_ideMediator.showHighlight();
				_highlightHidden = false;
			} else {
				Debug.Log("update");
				_ideMediator.updateHighlight(checkForLastIndex());
			}
			
			_ideProxy.setInstrList(_validatorLogic.InstList);
			sendInstruction();
			_ideProxy.setCurrentIndex(_ideProxy.IdeDO.currentInstIndex+1);
		} else {
			Debug.Log("hide");
			_ideMediator.hideHighlight();
			_highlightHidden = true;
			_ideProxy.setCurrentIndex(0);
			ErrorDC err = new ErrorDC();
			err.errorType = ErrorType.ERROR_0081_SINGLEEXECUTION_ERRORPERSISTS;
			_ideProxy.addErrorLog(err);
			_ideMediator.updateErrorLogs(_ideProxy.IdeDO.errorList);
		}
	}
	
	private int checkForLastIndex(){
		int index = _ideProxy.IdeDO.currentInstIndex;
		int len = _ideProxy.IdeDO.tempInstList.Count;
		int lineNum = 0;
		if(len > 1){
			string lastInstr = _ideProxy.IdeDO.tempInstList[index-1];
			string[] strArr = lastInstr.Split(new string[]{" "}, StringSplitOptions.None);
			if (strArr[0] == "J"){
				lineNum = Convert.ToInt32(strArr[strArr.Length-2]);
			} else if(_ideProxy.IdeDO.jumpFromInstruction) {
				lineNum = index;
				_ideProxy.setJumpFromInstr(false);
			} else {
				lineNum = index + 1;
			}
		}
		return lineNum;
	}
	
	private void sendInstruction(){
		if (_ideProxy.IdeDO.tempInstList.Count > 0){
			Debug.Log(_ideProxy.IdeDO.currentInstIndex);
			Debug.Log(_ideProxy.IdeDO.tempInstList.Count);
			if (_ideProxy.IdeDO.currentInstIndex < _ideProxy.IdeDO.tempInstList.Count){
				string str = _ideProxy.IdeDO.tempInstList[_ideProxy.IdeDO.currentInstIndex];
				Debug.Log("Request Sent");
				_ideMediator.updateErrorViewLabel("Instruction Sent: " + str);
				_ideProxy.sendInstruction(str);
			}
		}
	}
	
	private void onFullButtonClick(string inputStr){
		_ideMediator.hideHighlight();
		_highlightHidden = true;
		checkTimeToValidate(inputStr, true);
		if (_ideProxy.IdeDO.errorList.Count == 0  && inputStr.Length > 0){
			_ideMediator.resetErrorLog();
			_ideProxy.setInstrList(_validatorLogic.InstList);
			_ideProxy.sendInstructionList(_ideProxy.IdeDO.tempInstList);
			setInstructionListView();
		} else {
			ErrorDC err = new ErrorDC();
			err.errorType = ErrorType.ERROR_0081_SINGLEEXECUTION_ERRORPERSISTS;
			_ideProxy.addErrorLog(err);
			_ideMediator.updateErrorLogs(_ideProxy.IdeDO.errorList);
		}
	}
	
	private void setInstructionListView(){
		int len = _ideProxy.IdeDO.tempInstList.Count;
		string str = "Instruction Set: \n";
		for (int x=0; x<len; x++){
			str += _ideProxy.IdeDO.tempInstList[x] + "\n";
		}
		_ideMediator.updateErrorViewLabel(str);
	}
	
	private bool checkTimeToValidate(string str, bool inCompile){	
		//Debug.Log(_errorOnCompile + " : " + inCompile);
		bool executed = false;
		if (_errorOnCompile == inCompile){
			executed = true;
			string[] procStr = _validatorLogic.processString(str);
			_validatorLogic.validateInstructionFromStringArray(procStr);
		}
		return executed;
	}
	
	#region update views 
	private void onMemoryUpdate(string str){
		_ideMediator.updateViewLabel(TabType.MEMORY, str);
	}
	
	private void onRegisterUpdate(string str){
		_ideMediator.updateViewLabel(TabType.REGISTER, str);
	}
	
	private void onOpcodeUpdate(string str){
		Debug.Log("opcode Updated");
		_ideMediator.updateViewLabel(TabType.OPCODE, str);
	}
	
	private void onPipelineUpdate(string str){
		_ideMediator.updateViewLabel(TabType.PIPELINEMAP, str);
	}
	#endregion update views	
	private void onInputTextChanged(string str){
		//Debug.Log("Input Text Changed");
		_ideProxy.resetErrorLogs();
		_ideProxy.setCurrentIndex(0);
		if (!_highlightHidden){
			_ideMediator.hideHighlight();
			_highlightHidden = true;
		}
		//Debug.Log(String.Join(",", procStr));
		if (checkTimeToValidate(str, false)){
			updateErrorOutput();
		}
	}
	
	private void onValidationErrorFound(ErrorType errType, int lineNum, int paramNum){
		ErrorDC dc = new ErrorDC();
		dc.errorType = errType;
		dc.lineNum = lineNum;
		dc.paramNum = paramNum;
		Debug.Log("Error Type: " + errType.ToString() + " Line Num: " + lineNum + " Param Num: " + paramNum);
		_ideProxy.addErrorLog(dc);
	}
	
	private void updateErrorOutput(){
		_ideMediator.updateErrorLogs(_ideProxy.IdeDO.errorList);
	}
	
	private void initController(){
		_ideMediator = gameObject.AddComponent<IDEMediator>();
		_ideMediator.init();
		
		_ideProxy = new IDEProxy();
		_ideProxy.init();
		
		_validatorLogic = new Validator();
	
	}
	
	private void destroy(){
		removeListeners();
		_ideMediator.destroy();
	}
}
