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
	
}
	