using UnityEngine;
using System.Collections;

public class OpcodeView : MonoBehaviour, ITab, IComponentView{
	
	public const string OPCODEBUTTON_ENABLED = "IDE_0010_IDE_Tab_OpCode_Enabled";
	public const string OPCODEBUTTON_DISABLED = "IDE_0009_IDE_Tab_OpCode_Disabled";

	private GameObject _opcodeGO;
	private GameObject _opcodeTabButtonGO;
		
	private UILabel _opcodeLabel;
	
	public Emissary opcodeEmissary = new Emissary();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void init(GameObject opcodeGO, GameObject opcodeTabButtonGO){
		_opcodeGO = opcodeGO;
		_opcodeTabButtonGO = opcodeTabButtonGO;
		initGUI();
		addListeners();
	}
	
	private void initGUI(){
		_opcodeLabel = _opcodeGO.transform.FindChild("Label").gameObject.GetComponent<UILabel>();
	}
	
	public void setOpcodeLabel(string text){
		_opcodeLabel.text = text;
	}
	
	private void onOpcodeTabButtonClick(GameObject go){
		opcodeEmissary.dispatch();
	}
	
	public void addListeners(){
		UIEventListener.Get(_opcodeTabButtonGO).onClick += onOpcodeTabButtonClick;
	}
	
	public void removeListeners(){
		UIEventListener.Get(_opcodeTabButtonGO).onClick -= onOpcodeTabButtonClick;
	}
	
	public void setTabButtonActive(bool val){
		if (val){
			_opcodeTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = OPCODEBUTTON_ENABLED;
		} else {
			_opcodeTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = OPCODEBUTTON_DISABLED;
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
