using UnityEngine;
using System.Collections;

public class InputView : MonoBehaviour {
	
	private GameObject _inputGO;
	private GameObject _singleButtonGO;
	private GameObject _fullButtonGO;
	private GameObject _cameraGO;
	private GameObject _backHighlightGO;
	private UIInput _input;
	private UILabel _inputLabel;
	private UICamera _uiCamera;
	private UIDraggablePanel _inputDraggable;
	private int currentIndex;
	
	Vector2 scrollVect = new Vector2(1,1);
	
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
		//_input.text = _input.text.Substring(0, _input.text.Length - 1);
		if (_inputDraggable.verticalScrollBar.barSize < 1){
			_inputDraggable.verticalScrollBar.scrollValue = 1.2f;
		}
		//UICamera.selectedObject = _inputGO;
	}
	
	private void onTextChanged(GameObject go, string str){
		IDEEmissaryList.textChangedEmissary.dispatch(str);
	}

	public void init(GameObject inputGo, GameObject singleGO, GameObject fullGO, GameObject cameraGO, GameObject backHighlightGO){	
		_singleButtonGO = singleGO;
		_fullButtonGO = fullGO;
		_inputGO = inputGo;
		_cameraGO = cameraGO;
		_backHighlightGO = backHighlightGO;
		initGUI();
		addListeners();
	}
	
	private void initGUI(){
		_input = _inputGO.GetComponent<UIInput>();
		_input.eventReceiver = this.gameObject;
		_inputLabel = _input.transform.FindChild("Label").GetComponent<UILabel>();
		_uiCamera = _cameraGO.GetComponent<UICamera>();
		_inputDraggable = _input.GetComponent<UIDraggablePanel>();
		_backHighlightGO.SetActive(false);
	}
	
	public void setPipelinMapLabel(string text){
		_input.text = text;
	}
	
	private void addListeners(){
		UIEventListener.Get(_inputGO).onInput += onTextChanged;
		UIEventListener.Get(_singleButtonGO).onClick += onSingleClickHandler;
		UIEventListener.Get(_singleButtonGO).onClick += onFullClickHandler;
	}
	
	private void removeListeners(){
		UIEventListener.Get(_inputGO).onInput -= onTextChanged;
		UIEventListener.Get(_singleButtonGO).onClick -= onSingleClickHandler;
		UIEventListener.Get(_singleButtonGO).onClick -= onFullClickHandler;
	}
		
	private void onSingleClickHandler(GameObject go){
		IDEEmissaryList.singleButtonClickEmissary.dispatch();
	}
	
	private void onFullClickHandler(GameObject go){
		IDEEmissaryList.fullButtonClickEmissary.dispatch();
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
