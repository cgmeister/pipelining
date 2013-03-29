using UnityEngine;
using System.Collections;

public class RegisterView : MonoBehaviour , ITab, IComponentView {
	
	public const string REGISTERBUTTON_ENABLED = "IDE_0008_IDE_Tab_Register_Enabled";
	public const string REGISTERBUTTON_DISABLED = "IDE_0007_IDE_Tab_Register_Disabled";
	
	private GameObject _registerGO;
	private GameObject _registerTabButtonGO;
	
	private UILabel _registerLabel;
	
	public Emissary registerEmissary = new Emissary();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void init(GameObject registerGO, GameObject registerTabButtonGO){
		_registerGO = registerGO;
		_registerTabButtonGO = registerTabButtonGO;
		initGUI();
		addListeners();
	}
	
	private void initGUI(){
		_registerLabel = _registerGO.transform.FindChild("Label").gameObject.GetComponent<UILabel>();
	}
	
	public void setRegisterLabel(string text){
		_registerLabel.text = text;
	}
	
	public void setTabButtonActive(bool val){
		if (val){
			_registerTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = REGISTERBUTTON_ENABLED;
		} else {
			_registerTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = REGISTERBUTTON_DISABLED;
		}
	}
	
	private void onRegisterTabClicked(GameObject go){
		registerEmissary.dispatch();
	}
	
	public void addListeners(){
		UIEventListener.Get(_registerTabButtonGO).onClick += onRegisterTabClicked;
	}
	
	public void removeListeners(){
		UIEventListener.Get(_registerTabButtonGO).onClick -= onRegisterTabClicked;
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
