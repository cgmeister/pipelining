using UnityEngine;
using System.Collections;
using System;

public class InputView : MonoBehaviour {
	
	private GameObject _inputGO;
	private GameObject _singleButtonGO;
	private GameObject _fullButtonGO;
	private GameObject _cameraGO;
	private GameObject _backHighlightGO;
	private GameObject _resetGO;
	private GameObject _errorOnCompileGO;
	private GameObject _linNumGo;
	
	private UIInput _input;
	private UILabel _inputLabel;
	private UICamera _uiCamera;
	private UIDraggablePanel _inputDraggable;
	private int currentIndex;
	private UICheckbox _errorOnCompileCheckBox;
	private UILabel _lineNumLabel;
	private int _lineNumCtr = 1;
	
	private Vector3 lineNumPos;
	
	Vector2 scrollVect = new Vector2(1,1);
	
	public Emissary resetButtonEmissary = new Emissary();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update (){
		checkText();
	}
	
	private void checkText(){
		
	}

	private void OnSubmit(){
		_input.text += "\n";
		string[] strArr = _input.text.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.None);
		string[] lineNumArr = _lineNumLabel.text.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.None);
		
		if (lineNumArr.Length <= strArr.Length){
			_lineNumCtr += 1;
			_lineNumLabel.text += "\n" + _lineNumCtr;
		}
		
		//_input.text = _input.text.Substring(0, _input.text.Length - 1);
		if (_inputDraggable.verticalScrollBar.barSize < 1){
			_inputDraggable.verticalScrollBar.scrollValue = 1.2f;
		}
		//UICamera.selectedObject = _inputGO;
	}
	
	private void onTextChanged(GameObject go, string str){
		string inpStr = _input.text;
		IDEEmissaryList.textChangedEmissary.dispatch(str);
	}
	
	private void updateLineNumLabel(){
		_lineNumLabel.text = "";
		string str  = "";
		string[] strArr = _input.text.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.None);
		string[] lineNumArr = _lineNumLabel.text.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.None);
		for(int x=0; x<strArr.Length; x++){
			str += lineNumArr[x];
		}
		Debug.Log(str);
		_lineNumLabel.text = str;
	}

	public void init(GameObject inputGo, GameObject singleGO, GameObject fullGO, GameObject cameraGO, GameObject backHighlightGO, GameObject resetGO, GameObject errorOnCompileGO, GameObject lineNumGO){	
		_singleButtonGO = singleGO;
		_fullButtonGO = fullGO;
		_inputGO = inputGo;
		_cameraGO = cameraGO;
		_backHighlightGO = backHighlightGO;
		_errorOnCompileGO = errorOnCompileGO;
		_linNumGo = lineNumGO;
		_resetGO = resetGO;
		initGUI();
		addListeners();
	}
	
	private void initGUI(){
		_input = _inputGO.GetComponent<UIInput>();
		_input.eventReceiver = this.gameObject;
		_inputLabel = _input.transform.FindChild("Label").GetComponent<UILabel>();
		_uiCamera = _cameraGO.GetComponent<UICamera>();
		_inputDraggable = _input.GetComponent<UIDraggablePanel>();
		_errorOnCompileCheckBox = _errorOnCompileGO.GetComponent<UICheckbox>();
		_lineNumLabel = _linNumGo.GetComponent<UILabel>();
		lineNumPos = new Vector3(570,0,-35);
	}
	
	public void updateHighLight(int lineNum){
		Debug.Log("highlight updated");
		lineNumPos.y = (lineNum * -10) - 68;
		//Debug.Log(lineNum + ":" + lineNumPos.x);
		//Debug.Log(lineNum + ":" + lineNumPos.y);
		//Debug.Log(lineNum + ":" + lineNumPos.z);
		_backHighlightGO.transform.localPosition = lineNumPos;
		_backHighlightGO.GetComponent<UISlicedSprite>().spriteName = "IDE_0003_IDE_Button_Full_Disabled";
		_backHighlightGO.GetComponent<UISlicedSprite>().spriteName = "IDE_0003_IDE_Button_Full_Enabled";
	}
	
	public void hideHighlight(){
		Debug.Log("highlight hidden");
		lineNumPos.y = 100;
		_backHighlightGO.transform.localPosition = lineNumPos;
		_backHighlightGO.SetActive(false);
	}
	
	public void showHighlight(){
		Debug.Log("highlight showed");
		_backHighlightGO.SetActive(true);
		lineNumPos.y = -68;
		_backHighlightGO.transform.localPosition = lineNumPos;
		_backHighlightGO.GetComponent<UISlicedSprite>().spriteName = "IDE_0003_IDE_Button_Full_Disabled";
		_backHighlightGO.GetComponent<UISlicedSprite>().spriteName = "IDE_0003_IDE_Button_Full_Enabled";
	}
	
	public void setPipelinMapLabel(string text){
		_input.text = text;
	}
	
	private void addListeners(){
		UIEventListener.Get(_inputGO).onInput += onTextChanged;
		UIEventListener.Get(_singleButtonGO).onClick += onSingleClickHandler;
		UIEventListener.Get(_fullButtonGO).onClick += onFullClickHandler;
		UIEventListener.Get(_resetGO).onClick += onResetClick;
		UIEventListener.Get(_errorOnCompileGO).onClick += onCheckBoxClick;
	}
	
	private void removeListeners(){
		UIEventListener.Get(_inputGO).onInput -= onTextChanged;
		UIEventListener.Get(_singleButtonGO).onClick -= onSingleClickHandler;
		UIEventListener.Get(_fullButtonGO).onClick -= onFullClickHandler;
		UIEventListener.Get(_resetGO).onClick += onResetClick;
		UIEventListener.Get(_errorOnCompileGO).onClick -= onCheckBoxClick;
	}
	
	private void onCheckBoxClick(GameObject go){
		IDEEmissaryList.errorOnCompileButtonClickEmissary.dispatch(_errorOnCompileCheckBox.isChecked);
	}
	
	private void onResetClick(GameObject go){
		_input.text = "";
		_lineNumCtr = 1;
		_lineNumLabel.text = "1";
		resetButtonEmissary.dispatch();
	}
		
	private void onSingleClickHandler(GameObject go){
		IDEEmissaryList.singleButtonClickEmissary.dispatch(_input.text);
	}
	
	private void onFullClickHandler(GameObject go){
		IDEEmissaryList.fullButtonClickEmissary.dispatch(_input.text);
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
