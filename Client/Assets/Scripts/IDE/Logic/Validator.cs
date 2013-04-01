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
			string[] newStrArr = new string[]{""};
			string[] jmpStrArr = strArr[x].Split(new string[]{":"}, StringSplitOptions.None);
			if (jmpStrArr.Length > 1){
				if (checkAndValidateJumpLabel(jmpStrArr[0], x+1, -1)){
					validateInstruction(jmpStrArr[1], x+1);
				}
			} else {
				validateInstruction(strArr[x], x+1);
			}
			_instList.Add(newStrArr);
		}
	}
	
	private void validateInstruction(string str, int lineNum){
		InstructionType instType;
		OpcodeType opcodeType;
		string[] strArr = str.Split(new string[]{","}, StringSplitOptions.None);
		
		opcodeType = getOpcodeType(strArr[0]);
		Debug.Log("OPCODE: " + opcodeType.ToString());
		if (opcodeType != OpcodeType.Nil){				
			Debug.Log(strArr.Length);
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
		switch(opcodeType){
			case OpcodeType.BNEZ:{				
				if (len <= 3){
					if (len > 1){
						validateRegister(strArr[1], lineNum, 1);
					} 
					if (len > 2){
						validateJump(strArr[2], lineNum, 2);
					}
				}
				sendNumArgError(strArr.Length, 3, lineNum);
				break;
			}
			case OpcodeType.LD:{
				if (len <= 3){
					if (len > 1){
						validateRegister(strArr[1], lineNum, 1);
					} 
					if (len > 2){
						validateImmediateOffset(strArr[2], lineNum, 2, true);
					}
				}
				sendNumArgError(strArr.Length, 4, lineNum);
				break;
			}
			case OpcodeType.SD:{
				if (len <= 3){
					if (len > 1){
						validateRegister(strArr[1], lineNum, 1);
					} 
					if(len > 2){
						validateImmediateOffset(strArr[2], lineNum, 2, true);
					}
				}
				sendNumArgError(strArr.Length, 4, lineNum);
				break;
			}
			case OpcodeType.DADDI:{
				if (strArr.Length <= 4){
					if (len > 1){
						validateRegister(strArr[1], lineNum, 1);
					}	
					if (len > 2){
						validateRegister(strArr[2], lineNum, 2);
					}
					if (len > 3){
						validateImmediateOffset(strArr[3], lineNum, 3, false);
					}
				}
				sendNumArgError(strArr.Length, 3, lineNum);
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
		if (isOffset){
			string[] offArr = str.Split(new string[]{"("}, StringSplitOptions.None);
			if (offArr.Length == 2){
				// check immediate
				string offStr = offArr[0];
				string regStr = offArr[1];
				string comRegStr = regStr.Remove(regStr.Length-1);
				if (offStr.Length != 4 && checkIfAllAreDigits(comRegStr)){
					IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
					Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
				} else if (regStr[regStr.Length -1] != ')' && !validateRegister(comRegStr, lineNum, paramNum)){
				} else {
					valid = true;
				}
			} else {
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0040_OFFSET, lineNum, paramNum);
				Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0040_OFFSET));
			}
		} else {
			
			if (str[0] != '#'){
				IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
				Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
			} else {
				string digitStr = str.Remove(0,1);
				if (!checkIfAllAreDigits(digitStr)){
					IDEEmissaryList.validationErrorEmissary.dispatch(ErrorType.ERROR_0030_IMMEDIATE, lineNum, paramNum);
					Debug.Log("Validation error sent: " + Enum.GetName(typeof(ErrorType), ErrorType.ERROR_0030_IMMEDIATE));
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
