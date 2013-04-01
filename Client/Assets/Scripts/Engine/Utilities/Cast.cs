using UnityEngine;
using System.Collections;
using System;

public class Cast : MonoBehaviour {
	public static string enumToStr(Type enumType, object val){
		return Enum.GetName(enumType, val);
	}
}