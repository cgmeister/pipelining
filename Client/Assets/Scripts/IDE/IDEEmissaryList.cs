using UnityEngine;
using System.Collections;

public class IDEEmissaryList {
	public static Emissary singleButtonClickEmissary = new Emissary();
	public static Emissary fullButtonClickEmissary = new Emissary();
	public static Emissary<string> textChangedEmissary = new Emissary<string>();
	public static Emissary<ErrorType, int, int> validationErrorEmissary = new Emissary<ErrorType, int, int>(); // Error type, line number, param number
	public static Emissary<int, int> memoryRequestButton = new Emissary<int, int>();
}
