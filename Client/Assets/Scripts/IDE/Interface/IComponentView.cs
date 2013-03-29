using UnityEngine;
using System.Collections;

public interface IComponentView {
	
	void addListeners();
	
	void removeListeners();
	
	void show();
	
	void hide();
	
	void destroy();
}
