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
		string trimStr = str.Replace(" ", "");
		string[] strArr = trimStr.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.None);
		return strArr;
	}

	
	public void validateInstructionFromStringArray (string[] strArr){
		_instList = new List<string[]>();
		_jumpList = new List<string>();
		
		addJumpLabels(strArr);
		validateStringContents(strArr);
	}
	
	private void validateStringContents(string[] strArr){
		int len = strArr.Length;
		for (int x=0; x<len; x++){
			if (strArr[x] == ""){
				continue;
			}
			string[] newStrArr = new string[]{};
			string[] jmpStrArr = strArr[x].Split(new string[]{":"}, StringSplitOptions.None);
			//Debug.Log(jmpStrArr.Length);
			if (jmpStrArr.Length > 1){
				if (checkAndValidateJumpLabel(jmpStrArr[0], x+1, -1)){
					validateInstruction(jmpStrArr[1], x+1);
					 newStrArr = jmpStrArr[1].Split(new string[]{""}, StringSplitOptions.None);
					_instList.Add(newStrArr);
				}
			} else {
				validateInstruction(strArr[x], x+1);
				newStrArr = strArr[x].Split(new string[]{""}, StringSplitOptions.None);
				_instList.Add(newStrArr);
			}
		}
	}
	
	private void validateInstruction(string str, int lineNum){
		InstructionType instType;
		OpcodeType opcodeType;
		string[] strArr = str.Split(new string[]{","}, StringSplitOptions.None);
		
		opcodeType = getOpcodeType(strArr[0]);
		//Debug.Log("OPCODE: " + opcodeType.ToString());
		if (opcodeType != OpcodeType.Nil){				
			//Debug.Log(strArr.Length);
			instType = getInststructionType(opcodeType);
			if(strArr.Length > 1){
				checkForTypes(strArr, opcodeType, lineNum, instType);
			} else {
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0071_INSTRUCTION_INVALIDNUMBEROFARGUMENTS, lineNum, -2);
				Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0071_INSTRUCTION_INVALIDNUMBEROFARGUMENTS));
			}	
		} else {
			IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0010_OPCODE, lineNum, 0);
			Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0010_OPCODE));
		}
	}
	
	private void addJumpLabels(string[] strArr){
		int len = strArr.Length;
		for (int x=0; x<len; x++){ 
			string[] jmpStrArr = strArr[x].Split(new string[]{":"}, StringSplitOptions.None); 
			if (jmpStrArr.Length > 1){
				string jmpStr = jmpStrArr[0];
				Debug.Log(jmpStr);
				char jmpChar = jmpStr[0];
				if (Char.IsDigit(jmpChar)){  
					_jumpList.Add(jmpStr);
				} else { 
					
				}
			} else {
				_jumpList.Add("nil");
			}
			
			//Debug.Log(_jumpList.Count);
			//Debug.Log(_jumpList[_jumpList.Count-1]);
		}
	}

	private bool checkAndValidateJumpLabel (string str, int lineNum, int paramNum){
		bool hasJump = false;
		string[] jmpStrArr = str.Split(new string[]{":"}, StringSplitOptions.None);
		if (jmpStrArr.Length > 1){
			string jmpStr = jmpStrArr[0];
			char jmpChar = jmpStr[0];
			if (!Char.IsDigit(jmpChar)){
				hasJump = true;
			} else {
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0060_JUMP, lineNum, paramNum);
				Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0060_JUMP));
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
		if (strArr.Length <= 4){
			bool valid = true;
			for (int x=0; x<strArr.Length-1; x++){
				if(valid){
					valid = validateRegister(strArr[x+1], lineNum, x+1);
				} else {
					break;
				}
			}
		}	
	}
	
	private void sendNumArgError(int len ,int max, int lineNum){
		if (len != max){
			IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0071_INSTRUCTION_INVALIDNUMBEROFARGUMENTS, lineNum, -2);
			Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0071_INSTRUCTION_INVALIDNUMBEROFARGUMENTS));
		}
	}
	
	private void checkIType(string[] strArr, OpcodeType opcodeType, int lineNum){
		int len = strArr.Length;
		Debug.Log("ITLen: " + len);
		switch(opcodeType){
			case OpcodeType.BNEZ:{				
				if (len <= 3){
					if (len > 1) validateRegister(strArr[1], lineNum, 1); else sendNumArgError(strArr.Length, 3, lineNum);
					if (len > 2) validateJump(strArr[2], lineNum, 2); else sendNumArgError(strArr.Length, 3, lineNum);
				} else {
					sendNumArgError(strArr.Length, 3, lineNum);
				}
				break;
			}
			case OpcodeType.LD:{
				if (len <= 3){
					if (len > 1) validateRegister(strArr[1], lineNum, 1); else sendNumArgError(strArr.Length, 3, lineNum);
					if (len > 2) validateImmediateOffset(strArr[2], lineNum, 2, true); else sendNumArgError(strArr.Length, 3, lineNum);
				} else {
					sendNumArgError(strArr.Length, 3, lineNum);
				}
				break;
			}
			case OpcodeType.SD:{
				if (len <= 3){
					if (len > 1) validateRegister(strArr[1], lineNum, 1); else sendNumArgError(strArr.Length, 3, lineNum);
					if(len > 2)	validateImmediateOffset(strArr[2], lineNum, 2, true); else sendNumArgError(strArr.Length, 3, lineNum);
				} else {
					sendNumArgError(strArr.Length, 3, lineNum);
				}
				break;
			}
			case OpcodeType.DADDI:{
				if (len <= 4){
					if (len > 1) validateRegister(strArr[1], lineNum, 1); else sendNumArgError(strArr.Length, 4, lineNum);
					if (len > 2) validateRegister(strArr[2], lineNum, 2); else sendNumArgError(strArr.Length, 4, lineNum);
					if (len > 3) validateImmediateOffset(strArr[3], lineNum, 3, false); else sendNumArgError(strArr.Length, 4, lineNum);
				} else {
					sendNumArgError(strArr.Length, 4, lineNum);
				}
				break;
			}
		}
	}
	
	private void checkJType(string[] strArr, int lineNum){
		validateJump(strArr[1], lineNum, 1); 
	}
	
	private OpcodeType getOpcodeType(string str){
		OpcodeType opCode;
		switch(str){
			case "DADD":{
				opCode = OpcodeType.DADD;
				break;
			}
			case "DSUB":{
				opCode = OpcodeType.DSUB;
				break;
			}
			case "OR":{
				opCode = OpcodeType.OR;
				break;
			}
			case "XOR":{
				opCode = OpcodeType.XOR;
				break;
			}
			case "SLT":{
				opCode = OpcodeType.SLT;
				break;
			}
			case "BNEZ":{
				opCode = OpcodeType.BNEZ;
				break;
			}
			case "LD":{
				opCode = OpcodeType.LD;
				break;
			}
			case "SD":{
				opCode = OpcodeType.SD;
				break;
			}
			case "DADDI":{
				opCode = OpcodeType.DADDI;
				break;
			}
			case "J":{
				opCode = OpcodeType.J;
				break;
			}
			default :{
				opCode = OpcodeType.Nil;
				break;
			}
		}
		return opCode;
	}
	
	private InstructionType getInststructionType(OpcodeType opcodeType){
		InstructionType instType;
		switch(opcodeType){
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
			Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0020_REGISTER));
		}
		return valid;
	}
	
	private bool validateImmediateOffset(string str, int lineNum, int paramNum, bool isOffset){
		bool valid = false;
		Debug.Log("Validate immediate");
		if (isOffset){
			string[] offArr = str.Split(new string[]{"("}, StringSplitOptions.None);
			Debug.Log("string length: " + offArr.Length);
			if (offArr.Length == 2){
				// check immediate
				string offStr = offArr[0];
				string regStr = offArr[1];
				string comRegStr;
				Debug.Log(regStr.Length);
				if (regStr.Length > 0){
					comRegStr = regStr.Remove(regStr.Length-1);
					if (regStr[regStr.Length -1] != ')'){
						validateRegister(regStr, lineNum, paramNum);						
						IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0040_OFFSET, lineNum, paramNum);
						Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0040_OFFSET));
						IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0050_SYNTAX, lineNum, paramNum);
						Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0050_SYNTAX));
					} else {
						validateRegister(comRegStr, lineNum, paramNum);						
					}
				} else {	
					IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0020_REGISTER, lineNum, paramNum);
					Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0020_REGISTER));
					IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0040_OFFSET, lineNum, paramNum);
					Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0040_OFFSET));
				}
				if (offStr.Length != 4 || !checkIfAllAreHex(offStr)){
					IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
					Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
				}
			} else if(str.Length != 4 || !checkIfAllAreHex(str)){
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
				Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0040_OFFSET, lineNum, paramNum);
				Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0040_OFFSET));
			} else {
				Debug.Log("test 1");
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0040_OFFSET, lineNum, paramNum);
				Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0040_OFFSET));
			}
		} else {
			if (str.Length > 0){
				Debug.Log(str[0]);
				if (str[0] != '#'){
					IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
					Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
				} else {
					string digitStr = str.Remove(0,1);
					if (digitStr.Length == 4 ){
						if (!checkIfAllAreHex(digitStr)){
							IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
							Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
						}
					} else {
						IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
						Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
					}
				}
			} else {
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
				Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
			}
		}
		return valid;
	}
	
	private bool checkIfAllAreHex(string str){
		int len = str.Length;
		bool allHex = true;
		for (int x=0; x<len; x++){
			if (x > 3){
				break;
			}
			if (!Char.IsDigit(str[x]) && !checkIfHexChar(str[x])){
				allHex = false;
				break;
			} 
		}
		return allHex;
	} 
	
	private bool checkIfHexChar(char chr){
		bool isHexChar = false;
		switch(chr){
			case 'A':{
				isHexChar = true;
				break;
			}
			case 'B':{
				isHexChar = true;
				break;
			}
			case 'C':{
				isHexChar = true;
				break;
			}
			case 'D':{
				isHexChar = true;
				break;
			}
			case 'E':{
				isHexChar = true;
				break;
			}
			case 'F':{
				isHexChar = true;
				break;
			}
		}
		return isHexChar;
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
			IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0061_JUMP_NOJUMPABLEINSTRUCTION, lineNum, paramNum);
			Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0061_JUMP_NOJUMPABLEINSTRUCTION));
		}
	}
}
