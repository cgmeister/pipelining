using UnityEngine;
using System;
using System.Collections;

#pragma warning disable 0693

/// <summary>
/// Author: Hedrick David / hedrick_david@ymail.com
/// This is the events class used to dispatch Emissarys using
/// a broadcast and subscription model.
/// 
/// Note: Currently you can have as much as 3 types of parameters and types. 
/// This is to have ample amount of parameter passing while
/// not over passing. If you need to pass many parameters using Emissarys,
/// consider creating an <code>EventArgs</code> or create your own object class.
/// 
/// </summary>
/// 
public class Emissary<T, U, V>  {

	/// <summary>
	/// The Emissary generic type delegate
	/// </summary>
    public delegate void EmissaryHandler<T, U, V>(T param1, U param2, V param3);

	/// <summary>
	/// The Emissary generic type event
	/// </summary>
    private event EmissaryHandler<T, U, V> EventEmissary;

	/// <summary>
	/// Initializes a new instance of the <see cref="Emissarys"/> class.
	/// </summary>
	public Emissary(){}

	/// <summary>
	/// Add a Emissary event.
	/// </summary>
	/// <param name='function'>
	/// The function to be executed when the Emissary is dispatched.
	/// </param>
    public void add(EmissaryHandler<T, U, V> function){
        EventEmissary += function;
    }

	/// <summary>
	/// Remove a Emissary event.
	/// </summary>
	/// <param name='function'>
	/// The function of the event used to be executed when a Emissary is dispatched. 
	/// </param>

    public void remove(EmissaryHandler<T, U, V> function){   
        EventEmissary -= function;
    }   

	/// <summary>
	/// Dispatch an event with the corresponding events argument.
	/// </summary>
	/// <param name='ea'>
	/// The attachment to be sent.
	/// </param>
    public void dispatch(T param1, U param2, V param3){
		try{
	        if (EventEmissary != null){
	            EventEmissary(param1, param2, param3);
	        } else {
				//Debug.Log("No event listeners found");
			}
		} catch (Exception e){
			Debug.Log("Dispatch Error: " + e.Message);
		}
    }
}

/// <summary>
/// This is the events class used to dispatch Emissarys using
/// a broadcast and subscription model.
/// </summary>
public class Emissary<T, U>  {

	/// <summary>
	/// The Emissary generic type delegate
	/// </summary>
    public delegate void EmissaryHandler<T, U>(T param1, U param2);

	/// <summary>
	/// The Emissary generic type event
	/// </summary>
    private event EmissaryHandler<T, U> EventEmissary;

	/// <summary>
	/// Initializes a new instance of the <see cref="Emissarys"/> class.
	/// </summary>
	public Emissary(){}

	/// <summary>
	/// Add a Emissary event.
	/// </summary>
	/// <param name='function'>
	/// The function to be executed when the Emissary is dispatched.
	/// </param>
    public void add(EmissaryHandler<T, U> function){
        EventEmissary += function;
    }

	/// <summary>
	/// Remove a Emissary event.
	/// </summary>
	/// <param name='function'>
	/// The function of the event used to be executed when a Emissary is dispatched. 
	/// </param>

    public void remove(EmissaryHandler<T, U> function){   
        EventEmissary -= function;
    }   

	/// <summary>
	/// Dispatch an event with the corresponding events argument.
	/// </summary>
	/// <param name='ea'>
	/// The attachment to be sent.
	/// </param>
    public void dispatch(T param1, U param2){
		try{
	        if (EventEmissary != null){
	            EventEmissary(param1, param2);
	        } else {
				//Debug.Log("No event listeners found");
			}
		} catch (Exception e){
			Debug.Log("Dispatch Error: " + e.Message);
		}
    }
}

/// <summary>
/// This is the events class used to dispatch Emissarys using
/// a broadcast and subscription model.
/// </summary>
public class Emissary<T>  {

	/// <summary>
	/// The Emissary generic type delegate
	/// </summary>
    public delegate void EmissaryHandler<T>(T param);

	/// <summary>
	/// The Emissary generic type event
	/// </summary>
    private event EmissaryHandler<T> EventEmissary;

	/// <summary>
	/// Initializes a new instance of the <see cref="Emissarys"/> class.
	/// </summary>
	public Emissary(){}

	/// <summary>
	/// Add a Emissary event.
	/// </summary>
	/// <param name='function'>
	/// The function to be executed when the Emissary is dispatched.
	/// </param>
    public void add(EmissaryHandler<T> function){
        EventEmissary += function;
    }

	/// <summary>
	/// Remove a Emissary event.
	/// </summary>
	/// <param name='function'>
	/// The function of the event used to be executed when a Emissary is dispatched. 
	/// </param>

    public void remove(EmissaryHandler<T> function){   
        EventEmissary -= function;
    }   

	/// <summary>
	/// Dispatch an event with the corresponding events argument.
	/// </summary>
	/// <param name='ea'>
	/// The attachment to be sent.
	/// </param>
    public void dispatch(T param){
		try{
	        if (EventEmissary != null){
	            EventEmissary(param);
	        } else {
				//Debug.Log("No event listeners found");
			}
		} catch (Exception e){
			Debug.Log("Dispatch Error: " + e.Message + " - Trying to send a param: " + typeof(T).ToString());
		}
    }
}

public class Emissary  {

	/// <summary>
	/// The Emissary generic type delegate
	/// </summary>
    public delegate void EmissaryHandler();

	/// <summary>
	/// The Emissary generic type event
	/// </summary>
    private event EmissaryHandler EventEmissary;

	/// <summary>
	/// Initializes a new instance of the <see cref="Emissarys"/> class.
	/// </summary>
	public Emissary(){}

	/// <summary>
	/// Add a Emissary event.
	/// </summary>
	/// <param name='function'>
	/// The function to be executed when the Emissary is dispatched.
	/// </param>
    public void add(EmissaryHandler function){
        EventEmissary += function;
    }

	/// <summary>
	/// Remove a Emissary event.
	/// </summary>
	/// <param name='function'>
	/// The function of the event used to be executed when a Emissary is dispatched. 
	/// </param>

    public void remove(EmissaryHandler function){   
        EventEmissary -= function;
    }   

	/// <summary>
	/// Dispatch an event Emissary.
	/// </summary>
    public void dispatch(){
		try {
	        if (EventEmissary != null){
	            EventEmissary();
	        } else {
				//Debug.Log("No event listeners found");
			}
		} catch (Exception e){
			Debug.Log("Dispatch error: " + e.Message);
		}
    }
}