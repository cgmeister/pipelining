using UnityEngine;
using System.Collections;

public class ErrorView : MonoBehaviour {
	
	private GameObject _errorGO;
	private GameObject _errorTabButtonGO;
	
	private UILabel _errorLabel;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void init(GameObject errorGO){
		_errorGO = errorGO;
		initGUI();
		addListeners();
	}
	
	private void initGUI(){
		_errorLabel = _errorGO.transform.FindChild("Label").gameObject.GetComponent<UILabel>();
	}
	
	public void setErrorLabel(string text){
		_errorLabel.text = text;
	}
	
	public void addListeners(){
		
	}
	
	public void removeListeners(){
	
	}
	
	public void show(){
		addListeners();
	}
	
	public void hide(){
		removeListeners();
	}
	
	public void destroy(){
		removeListeners();
	}
}
