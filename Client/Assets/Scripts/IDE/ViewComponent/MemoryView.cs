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
		
		_memoryStartInput.onSubmit = OnStartSubmit;
		_memoryEndInput.onSubmit = OnEndSubmit;
		setInputActivity(false);
	}
	
	public void setmemoryLabel(string text){
		_memoryLabel.text = text;
	}
	
	private void onStartTextChanged(GameObject go, string str){
		removeNonDigits(_memoryStartInput);
		validateString(_memoryStartInput);
	}
	
	private void onEndTextChanged(GameObject go, string str){
		removeNonDigits(_memoryEndInput);
		validateString(_memoryEndInput);
	}
	
	private void removeNonDigits(UIInput input){
		string str = input.text;
		int len = str.Length;
		for (int x=0; x<len; x++){
			if (!char.IsDigit(str[x])){
				input.text = str.Remove(x, len - x);
				Debug.Log(input.text);
				break;
			}
		}
	}
	
	private void validateDigit(UIInput input, string str){
		if (str.Length != 0){
			if (!char.IsDigit(str[0])){
				input.text = "";
			}
		}
	}
	
	private void validateString(UIInput input){
		if (input.text.Length > 2){
			input.text = input.text.Remove(2);
		}
	}
	
	private void OnStartSubmit(string str){
		//validateString(_memoryStartInput, _memoryStartInput.text);
	}
	
	private void OnStartSubmit(GameObject go){
		OnStartSubmit(_memoryStartInput.text);
	}
	
	private void OnEndSubmit(string str){
		//validateString(_memoryEndInput, _memoryEndInput.text);
		int endVal = int.Parse(_memoryEndInput.text);
		int startVal = int.Parse(_memoryStartInput.text);
		if (endVal < startVal){
			_memoryStartInput.text = _memoryEndInput.text;
		}
	}
	
	private void OnEndSubmit(GameObject go){
		OnEndSubmit(_memoryEndInput.text);
	}
	
	private void onMemoryTabButtonClick(GameObject go){
		memoryEmissary.dispatch();
	}
	
	private void onRequestButtonClicked(GameObject go){
		if (int.Parse(_memoryEndInput.text) != 0){
			IDEEmissaryList.memoryRequestButton.dispatch(int.Parse(_memoryStartInput.text), int.Parse(_memoryEndInput.text));
		}
	}
	
	public void addListeners(){
		UIEventListener.Get(_memoryTabButtonGO).onClick += onMemoryTabButtonClick;
		UIEventListener.Get(_memoryRequestButtonGO).onClick += onRequestButtonClicked;
		UIEventListener.Get(_memoryStartInputGO).onInput += onStartTextChanged;
		UIEventListener.Get(_memoryEndInputGO).onInput += onEndTextChanged;	
		UIEventListener.Get(_memoryStartInputGO).onSubmit += OnStartSubmit;
		UIEventListener.Get(_memoryEndInputGO).onSubmit += OnEndSubmit;
	}
	
	public void removeListeners(){
		UIEventListener.Get(_memoryTabButtonGO).onClick -= onMemoryTabButtonClick;
		UIEventListener.Get(_memoryRequestButtonGO).onClick -= onRequestButtonClicked;
		UIEventListener.Get(_memoryStartInputGO).onInput -= onStartTextChanged;
		UIEventListener.Get(_memoryStartInputGO).onSubmit -= OnStartSubmit;
		UIEventListener.Get(_memoryEndInputGO).onSubmit -= OnEndSubmit;
	}
	
	public void setTabButtonActive(bool val){
		if (val){
			_memoryTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = MEMORYBUTTON_ENABLED;
		} else {
			_memoryTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = MEMORYBUTTON_DISABLED;
		}
	}
	
	public void setInputActivity(bool val){
		_memoryEndInputGO.SetActive(val);
		_memoryStartInputGO.SetActive(val);
		_memoryRequestButtonGO.SetActive(val);
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