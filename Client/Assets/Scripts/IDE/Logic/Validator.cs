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
		str.Replace(" ", String.Empty);
		string[] strArr = str.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.None);
		return strArr;
	}
	
	private void validateString (string[] strArr){
		addJumpLabels(strArr);
		validateStringContents(strArr);
	}
	
	private void validateStringContents(string[] strArr){
		int len = strArr.Length;
		for (int x=0; x<len; x++){
			if (strArr[x] == ""){
				continue;
			}
			string[] newStrArr = new string[]{""};
			string[] jmpStrArr = strArr[x].Split(new string[]{":"}, StringSplitOptions.None);
			if (jmpStrArr.Length > 1){
				if (checkAndValidateJumpLabel(jmpStrArr[0], x, -1)){
					validateInstruction(jmpStrArr[1], x);
				} 
			} else {
				validateInstruction(strArr[x], x);
			}	
			_instList.Add(newStrArr);			
		}
	}
	
	private void validateInstruction(string str, int lineNum){
		InstructionType instType;
		OpcodeType opcodeType = (OpcodeType)Enum.Parse(typeof(OpcodeType), str);
		string[] strArr = str.Split(new string[]{","}, StringSplitOptions.None);
		instType = validateOpcode(str);
		if (instType != InstructionType.Nil){
			if(strArr.Length > 1){
				checkForTypes(strArr, opcodeType, lineNum, instType);
			} else {
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0071_INSTRUCTION_INSUFFICIENTPARAMS, lineNum, 0);
			}
		} else {
			IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0010_OPCODE, lineNum, 0);
		}
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

	private bool checkAndValidateJumpLabel (string str, int lineNum, int paramNum){
		bool hasJump = false;
		string[] jmpStrArr = str.Split(new string[]{":"}, StringSplitOptions.None);
		if (jmpStrArr.Length > 1){
			string jmpStr = jmpStrArr[0];
			char jmpChar = jmpStr[0];
			if (Char.IsDigit(jmpChar)){
				hasJump = true;
			} else {
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0060_JUMP, lineNum, paramNum);
			}
		} 
		return hasJump;
	}
	
	private void checkForTypes(string[] strArr, OpcodeType opcodeType, int lineNum, InstructionType instType){
		switch(instType){
			case InstructionType.R:{
				checkRType(strArr, lineNum);
				break;
			}
			case InstructionType.I:{
				checkIType(strArr, opcodeType, lineNum);
				break;
			}
			case InstructionType.J:{
				checkJType(strArr, lineNum);
				break;
			}
		}
	}
	
	private void checkRType(string[] strArr, int lineNum){
		if (strArr.Length == 3){
			bool valid = true;
			for (int x=0; x<3; x++){
				if(valid){
					valid = validateRegister(strArr[x+1], lineNum, x+1);
				} else {
					break;
				}
			}
		} else {
			IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0071_INSTRUCTION_INSUFFICIENTPARAMS, lineNum, -2);
		}
	}
	
	private void checkIType(string[] strArr, OpcodeType opcodeType, int lineNum){
		switch(opcodeType){
			case OpcodeType.BNEZ:{
				validateRegister(strArr[1], lineNum, 1);
				validateJump(strArr[2], lineNum, 2);
				break;
			}
			case OpcodeType.LD:{
				validateRegister(strArr[1], lineNum, 1);
				validateImmediateOffset(strArr[2], lineNum, 2, true);
				break;
			}
			case OpcodeType.SD:{
				validateRegister(strArr[1], lineNum, 1);
				validateImmediateOffset(strArr[2], lineNum, 2, true);
				break;
			}
			case OpcodeType.DADDI:{
				validateRegister(strArr[1], lineNum, 1);
				validateImmediateOffset(strArr[2], lineNum, 2, false);
				break;
			}
		}
	}
	
	private void checkJType(string[] strArr, int lineNum){
		validateJump(strArr[1], lineNum, 1); 
	}
	
	private InstructionType validateOpcode(string str){
		InstructionType instType;
		switch((OpcodeType)Enum.Parse(typeof(OpcodeType), str)){
			case OpcodeType.DADD:{
				instType = InstructionType.R;
				break;
			}
			case OpcodeType.DSUB:{
				instType = InstructionType.R;
				break;
			}
			case OpcodeType.OR:{
				instType = InstructionType.R;
				break;
			}
			case OpcodeType.XOR:{
				instType = InstructionType.R;
				break;
			}
			case OpcodeType.SLT:{
				instType = InstructionType.R;
				break;
			}
			case OpcodeType.BNEZ:{
				instType = InstructionType.I;
				break;
			}
			case OpcodeType.LD:{
				instType = InstructionType.I;
				break;
			}
			case OpcodeType.SD:{
				instType = InstructionType.I;
				break;
			}
			case OpcodeType.DADDI:{
				instType = InstructionType.I;
				break;
			}
			case OpcodeType.J:{
				instType = InstructionType.J;
				break;
			}
			default :{
				instType = InstructionType.Nil;
				break;
			}
		}
		return instType;
	}
	
	private bool validateRegister(string str, int lineNum, int paramNum){
		bool valid = false;
		string[] registerList = Enum.GetNames(typeof(RegisterType));
		int len = registerList.Length;
		for (int x=0; x<len; x++){
			if (registerList[x] == str){
				valid = true;
				break;
			}
		}
		if (!valid){
			IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0020_REGISTER, lineNum, paramNum);
		}
		return valid;
	}
	
	private bool validateImmediateOffset(string str, int lineNum, int paramNum, bool isOffset){
		bool valid = false;
		if (isOffset){
			string[] offArr = str.Split(new string[]{"("}, StringSplitOptions.None);
			if (offArr.Length == 2){
				// check immediate
				string offStr = offArr[0];
				string regStr = offArr[1];
				string comRegStr = regStr.Remove(regStr.Length-1);
				if (offStr.Length != 4 && checkIfAllAreDigits(comRegStr)){
					IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
				} else if (regStr[regStr.Length -1] != ')' && !validateRegister(comRegStr, lineNum, paramNum)){
				} else {
					valid = true;
				}
			} else {
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0040_OFFSET, lineNum, paramNum);
			}
		} else {
			
			if (str[0] != '#'){
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
			} else {
				string digitStr = str.Remove(0,1);
				if (!checkIfAllAreDigits(digitStr)){
					IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
				}
			}
		}
		return valid;
	}
	
	private bool checkIfAllAreDigits(string str){
		int len = str.Length;
		bool notNumeric = false;
		for (int x=0; x<len; x++){
			if (!Char.IsDigit(str[x])){
				notNumeric = true;
			}
		}
		return notNumeric;
	}
	
	private void validateJump(string str, int lineNum, int paramNum){
		bool valid = false;
		int len = _jumpList.Count;
		for (int x=0; x<len; x++){
			if (str == _jumpList[x]){
				valid = true;
				break;
			}
		}
		if (!valid){
			IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0060_JUMP, lineNum, paramNum);
		}
	}
}
