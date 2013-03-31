using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Validator {

	private List<string> _jumpList;
	
	public List<string> JumpList {
		get {
			return this._jumpList;
		}
		set {
			_jumpList = value;
		}
	}
	
	private List<string[]> _instList;

	public List<string[]> InstList {
		get {
			return this._instList;
		}
	}
	
	public string[] processString(string str){
		string[] strArr = str.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.None);
		return strArr;
	}
	
	private void validateString (string[] strArr){
		addJumpLabels(strArr);
		validateStringContents();
	}
	
	private void validateStringContents(){
		
	}
	
	private void addJumpLabels(string[] strArr){
		int len = strArr.Length;
		for (int x=0; x<len; x++){
			string[] jmpStrArr = strArr[x].Split(new string[]{":"}, StringSplitOptions.None);
			if (jmpStrArr.Length > 1){
				string jmpStr = jmpStrArr[0];
				char jmpChar = jmpStr[0];
				if (Char.IsDigit(jmpChar)){
					_jumpList.Add(jmpStr);
				} else {
					_jumpList.Add("nil");
				}
			}
		}
	}	

	private bool validateJumpLabel (string str, int lineNum, int paramNum){
		bool hasJump = false;
		string[] jmpStrArr = str.Split(new string[]{":"}, StringSplitOptions.None);
		if (jmpStrArr.Length > 1){
			string jmpStr = jmpStrArr[0];
			char jmpChar = jmpStr[0];
			if (Char.IsDigit(jmpChar)){
				hasJump = true;
			}
		} else {
			IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0060_JUMP, lineNum, paramNum);
		}
		return hasJump;
	}
	
	private void checkForTypes(){
		
	}
	
	private void validateRegister(){
		
	}
	
	private void validateImmediate(){
		
	}
	
	private void validateJump(){
		
	}
}
