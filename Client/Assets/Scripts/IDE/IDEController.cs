using UnityEngine;
using System.Collections;
using System;

public class IDEController : MonoBehaviour {
	
	private IDEProxy _ideProxy;
	private IDEMediator _ideMediator;
	
	private Validator _validatorLogic;
	
	public void init(){
		initController();
		addListeners();
		
	}
	
	private void addListeners(){
		IDEEmissaryList.textChangedEmissary.add(onInputTextChanged);
		IDEEmissaryList.validationErrorEmissary.add(onValidationErrorFound);
	}
	
	private void removeListeners(){
		IDEEmissaryList.textChangedEmissary.remove(onInputTextChanged);
		IDEEmissaryList.validationErrorEmissary.remove(onValidationErrorFound);
	}
	
	private void onInputTextChanged(string str){
		string[] procStr = _validatorLogic.processString(str);
		//Debug.Log(String.Join(",", procStr));
		_validatorLogic.validateInstructionFromStringArray(procStr);
	}
	
	private void onValidationErrorFound(ErrorType errType, int lineNum, int paramNum){
		ErrorDC dc = new ErrorDC();
		dc.errorType = errType;
		dc.lineNum = lineNum;
		dc.paramNum = paramNum;
		Debug.Log("Error Type: " + errType.ToString() + " Line Num: " + lineNum + " Param Num: " + paramNum);
		_ideProxy.AddError(dc);
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
