using UnityEngine;
using System.Collections;

public class PipelineMapView : MonoBehaviour, ITab, IComponentView {

	public const string PIPELINEBUTTON_ENABLED = "IDE_0006_IDE_Tab_PipelineMap_Enabled";
	public const string PIPELINEBUTTON_DISABLED = "IDE_0005_IDE_Tab_PipelineMap_Disabled";
	
	private GameObject _pipelineGO;
	private GameObject _pipelineTabButtonGO;
	
	private UILabel _pipelineLabel;
	
	public Emissary pipelineMapEmissary = new Emissary();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void init(GameObject pipelineGO, GameObject pipe1ineButtonGO){
		_pipelineGO = pipelineGO;
		_pipelineTabButtonGO = pipe1ineButtonGO;
		initGUI();
		addListeners();
	}
	
	private void initGUI(){
		_pipelineLabel = _pipelineGO.transform.FindChild("Label").gameObject.GetComponent<UILabel>();
	}
	
	public void setPipelinMapLabel(string text){
		_pipelineLabel.text = text;
	}
	
	public void setTabButtonActive(bool val){
		if (val){
			_pipelineTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = PIPELINEBUTTON_ENABLED;
		} else {
			_pipelineTabButtonGO.transform.FindChild("Background").GetComponent<UISlicedSprite>().spriteName = PIPELINEBUTTON_DISABLED;
		}
	}
	
	private void onPipelineTabButtonClicked(GameObject go){
		pipelineMapEmissary.dispatch();
	}
	
	public void addListeners(){
		UIEventListener.Get(_pipelineTabButtonGO).onClick += onPipelineTabButtonClicked;
	}
	
	public void removeListeners(){
		UIEventListener.Get(_pipelineTabButtonGO).onClick -= onPipelineTabButtonClicked;
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
