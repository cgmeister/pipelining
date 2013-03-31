using UnityEngine;
using System.Collections;

public class IDEController : MonoBehaviour {
	
	private IDEProxy _ideProxy;
	private IDEMediator _ideMediator;
	
	public void init(){
		initController();
		addListeners();
	}
	
	private void addListeners(){
		IDEEmissaryList.textChangedEmissary.add(onInputTextChanged);
	}
	
	private void removeListeners(){
		IDEEmissaryList.textChangedEmissary.remove(onInputTextChanged);
	}
	
	private void onInputTextChanged(string str){
		
	}
	
	private void initController(){
		_ideMediator = gameObject.AddComponent<IDEMediator>();
		_ideMediator.init();
		
		_ideProxy = new IDEProxy();
		_ideProxy.init();
	}
	
	private void destroy(){
		removeListeners();
		_ideMediator.destroy();
	}
}
