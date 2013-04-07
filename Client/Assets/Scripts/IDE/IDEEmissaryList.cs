using UnityEngine;
using System.Collections;

public class IDEEmissaryList {
	public static Emissary<string> singleButtonClickEmissary = new Emissary<string>();
	public static Emissary<string> fullButtonClickEmissary = new Emissary<string>();
	public static Emissary<string> textChangedEmissary = new Emissary<string>();
	public static Emissary<ErrorType, int, int> validationErrorEmissary = new Emissary<ErrorType, int, int>(); // Error type, line number, param number
	public static Emissary<int, int> memoryRequestButton = new Emissary<int, int>();
	
	public static Emissary<string> updateOpcode = new Emissary<string>();
	public static Emissary<string> updateRegister = new Emissary<string>();
	public static Emissary<string> updateMemory = new Emissary<string>();
	public static Emissary<string> updatePipeline = new Emissary<string>();
	
	public static Emissary updateDisplayTab = new Emissary();
}
