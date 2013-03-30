using UnityEngine;
using System.Collections;

public class MainApplication : MonoBehaviour {
	
	private IDEController _ideController;
	
	// Use this for initialization
	void Start () {
		startApp();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void startApp(){
		_ideController = gameObject.AddComponent<IDEController>();
		_ideController.init();
	}
}
