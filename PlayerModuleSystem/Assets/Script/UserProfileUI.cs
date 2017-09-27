using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserProfileUI : MonoBehaviour {
	public UserLoginUI loginUI;
	public ScrollRect scrollView;
	public Button dummyButton; 
	public UXController uxcontroller;
	public SelectionProfileUI selectionProfileUI;
	public UserObject selectedObject;
	public bool updateStatus=false;

	void Start () {
		DataService db = DataService.getInstance();
		string userID = loginUI.GetUserID();	
		foreach(PurchaseInfo p in db.GetPurchaseInfo(userID)) {
			GameObject userObject = GameObject.Instantiate(dummyButton.gameObject);
			userObject.GetComponentInChildren<Text>().text=p.objects+"_"+p.objectLevel;
			userObject.GetComponent<UserObject>().purchaseInfo = p;
			userObject.SetActive(true);
			userObject.transform.parent = scrollView.content.transform;
		}
	}

	public void ObjUpdateBtClick() {
		string objName=EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
		selectedObject = EventSystem.current.currentSelectedGameObject.GetComponent<UserObject>();
		string[] substrings = objName.Split('_');
		int currentLevel=Convert.ToInt32(substrings[1]);
		if(currentLevel<5){
			uxcontroller.SwitchToSelectionProfileUI();
			updateStatus=true;
		}else{
			updateStatus=false;
			Debug.Log("Reached Maximum Level");
		}
	}
}
