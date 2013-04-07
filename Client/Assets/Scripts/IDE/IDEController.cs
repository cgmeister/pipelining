using UnityEngine;
using System.Collections;
using System;

public class IDEController : MonoBehaviour {
	
	private IDEProxy _ideProxy;
	private IDEMediator _ideMediator;
	
	private Validator _validatorLogic;
	
	private bool _errorOnCompile = true;
	
	public void init(){
		initController();
		addListeners();
	}
	
	private void addListeners(){
		IDEEmissaryList.textChangedEmissary.add(onInputTextChanged);
		IDEEmissaryList.validationErrorEmissary.add(onValidationErrorFound);
		IDEEmissaryList.updateMemory.add(onMemoryUpdate);
		IDEEmissaryList.updateRegister.add(onRegisterUpdate);
		IDEEmissaryList.updateOpcode.add(onOpcodeUpdate);
		IDEEmissaryList.updatePipeline.add(onPipelineUpdate);
		IDEEmissaryList.singleButtonClickEmissary.add(onSingleButtonClick);
		IDEEmissaryList.singleButtonClickEmissary.add(onFullButtonClick);
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
		IDEEmissaryList.singleButtonClickEmissary.remove(onFullButtonClick);
		IDEEmissaryList.errorOnCompileButtonClickEmissary.remove(onCheckBoxClick);
	}
	
	private void onCheckBoxClick(bool val){
		_errorOnCompile = val;
	}
	
	private void onSingleButtonClick(string inputStr){
		checkTimeToValidate(inputStr, true);
		if (_ideProxy.IdeDO.errorList.Count == 0 && inputStr.Length > 0){
			Debug.Log(_ideProxy.IdeDO.currentInstIndex);
			//Debug.Log(_ideProxy.IdeDO.tempInstList[_ideProxy.IdeDO.currentInstIndex]);
			_ideProxy.setInstrList(_validatorLogic.InstList);
			string str = String.Join("", _ideProxy.IdeDO.tempInstList[_ideProxy.IdeDO.currentInstIndex]);
			_ideProxy.setCurrentIndex(_ideProxy.IdeDO.currentInstIndex+1);
			_ideProxy.sendInstruction(str);
		} else {
			ErrorDC err = new ErrorDC();
			err.errorType = ErrorType.ERROR_0081_SINGLEEXECUTION_ERRORPERSISTS;
			_ideProxy.addErrorLog(err);
			_ideMediator.updateErrorLogs(_ideProxy.IdeDO.errorList);
		}
	}
	
	private void onFullButtonClick(string inputStr){
		checkTimeToValidate(inputStr, true);
		if (_ideProxy.IdeDO.errorList.Count == 0  && inputStr.Length > 0){
			_ideProxy.setInstrList(_validatorLogic.InstList);
			_ideProxy.sendInstructionList(_ideProxy.IdeDO.tempInstList);
		} else {
			ErrorDC err = new ErrorDC();
			err.errorType = ErrorType.ERROR_0081_SINGLEEXECUTION_ERRORPERSISTS;
			_ideProxy.addErrorLog(err);
			_ideMediator.updateErrorLogs(_ideProxy.IdeDO.errorList);
		}
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
		_ideMediator.updateViewLabel(TabType.OPCODE, str);
	}
	
	private void onPipelineUpdate(string str){
		_ideMediator.updateViewLabel(TabType.PIPELINEMAP, str);
	}
	#endregion update views	
	private void onInputTextChanged(string str){
		_ideProxy.resetErrorLogs();
		
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
