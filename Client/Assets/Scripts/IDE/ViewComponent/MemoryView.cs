using UnityEngine;
using System.Collections;
using System;

public class MemoryView : MonoBehaviour {
	
	public const string MEMORYBUTTON_ENABLED = "IDE_0008_IDE_Tab_Register_Enabled";
	public const string MEMORYBUTTON_DISABLED = "IDE_0007_IDE_Tab_Register_Disabled";
	
	private GameObject _memoryGO;
	private GameObject _memoryTabButtonGO;
	private GameObject _memoryStartInputGO;
	private GameObject _memoryEndInputGO;
	private GameObject _memoryRequestButtonGO;
	
	private UILabel _memoryLabel;
	private UIInput _memoryStartInput;
	private UIInput _memoryEndInput;
	
	public Emissary memoryEmissary = new Emissary();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void init(GameObject memoryGO, GameObject memoryTabButtonGO, GameObject memoryStartInputGO, GameObject memoryEndInputGO, GameObject memoryRequestButtonGO){
		_memoryGO = memoryGO;
		_memoryTabButtonGO = memoryTabButtonGO;
		_memoryStartInputGO = memoryStartInputGO;
		_memoryEndInputGO = memoryEndInputGO;
		_memoryRequestButtonGO = memoryRequestButtonGO;

		initGUI();
		addListeners();
	}
	
	private void initGUI(){
		_memoryLabel = _memoryGO.transform.FindChild("Label").gameObject.GetComponent<UILabel>();
		_memoryStartInput = _memoryStartInputGO.GetComponent<UIInput>();
		_memoryEndInput = _memoryEndInputGO.GetComponent<UIInput>();
	}
	
	public void setmemoryLabel(string text){
		_memoryLabel.text = text;
	}
	
	private void onStartTextChanged(GameObject go, string str){
		Debug.Log(str);
		if (!char.IsDigit(str[0])){
			_memoryStartInput.text = "0";
		}
	}
	
	private void onEndTextChanged(GameObject go, string str){
		Debug.Log(str);
		if (!char.IsDigit(str[0])){
			_memoryEndInput.text = "0";
		}
	}
	
	private void onMemoryTabButtonClick(GameObject go){
		memoryEmissary.dispatch();
	}
	
	private void onRequestButtonClicked(GameObject go){
		IDEEmissaryList.memoryRequestButton.dispatch(int.Parse(_memoryStartInput.text), int.Parse(_memoryEndInput.text));
	}
	
	public void addListeners(){
		UIEventListener.Get(_memoryTabButtonGO).onClick += onMemoryTabButtonClick;
		UIEventListener.Get(_memoryRequestButtonGO).onClick += onRequestButtonClicked;
		UIEventListener.Get(_memoryStartInputGO).onInput += onStartTextChanged;
		UIEventListener.Get(_memoryEndInputGO).onInput += onEndTextChanged;
	}
	
	public void removeListeners(){
		UIEventListener.Get(_memoryTabButtonGO).onClick -= onMemoryTabButtonClick;
		UIEventListener.Get(_memoryRequestButtonGO).onClick -= onRequestButtonClicked;
		UIEventListener.Get(_memoryStartInputGO).onInput += onStartTextChanged;
		UIEventListener.Get(_memoryEndInputGO).onInput += onEndTextChanged;
	}
	
	public void setTabButtonActive(bool val){
		if (val){
			_memoryTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = MEMORYBUTTON_ENABLED;
		} else {
			_memoryTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = MEMORYBUTTON_DISABLED;
		}
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
