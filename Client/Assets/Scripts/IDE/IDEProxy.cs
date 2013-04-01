using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	}
	
	public string[] getMemory(int startNum, int endNum){
		string[] memArr = new string[]{""};
		
		return memArr;
	}
	
	public void addErrorLog(ErrorDC errItem){
		_ideDO.errorList.Add(errItem);
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
	
	public string requestRegister(){
		string str = "";
		
		
		return str;
	}
}
	