using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionProfileUI : MonoBehaviour {
	public Text costText;
	public Text helthText;
	public Text damageText;
	public Text statusText;
	public Text earnedCoinText;

	public Button dummyButton;
	public ScrollRect scrollview ;
	public UserLoginUI loginUI;
	public ModelSelector modelSelector;
	public UserProfileUI userProfileUI;

	private int selectionValue = 0;
	public 	int earnedCoins;

	// Use this for initialization
	void Start () {
		DataService db = DataService.getInstance();
		earnedCoins = db.GetEarnedCoinsForUser(loginUI.GetUserID());

		modelSelector.Reset();
		modelSelector.EnableObject(0);

		GameObject btnChild = GameObject.Instantiate(dummyButton.gameObject);
		btnChild.SetActive(true);
		btnChild.transform.parent = scrollview.content.transform;
	}

	public void OnSelectionChanged(Vector2 v) {
		Debug.Log(v.x.ToString());

		selectionValue = (int)(v.x*(modelSelector.array.Count-1));
		modelSelector.EnableObject(selectionValue);
	}

	public void AddObject() {
		DataService db = DataService.getInstance();

		int costOfSelectedObject = (int)modelSelector.modelData.x;
		if(costOfSelectedObject>earnedCoins) {
			Debug.Log("Not Enough coins to purchase the selected objects");
		} else {
			Debug.Log("you purchased the selected objects");
			earnedCoins-=costOfSelectedObject;
			if(userProfileUI.updateStatus){
				int currentLevel=userProfileUI.selectedObject.purchaseInfo.objectLevel;
//				Vector3 updatedata = modelSelector.GetUpdateCost(currentLevel);
				PurchaseInfo p =new PurchaseInfo();
				p.objectLevel=currentLevel+1;
				db.UpdatePurchace(p);
			} else {
				db.CreatePurchase(loginUI.GetUserID(), costOfSelectedObject, modelSelector.array[selectionValue].name, 0);

			}
		}
	}
	void Update (){
		Vector3 updateCost;
		int objLevel=userProfileUI.selectedObject.purchaseInfo.objectLevel;
		int displayLevel=objLevel+1;
		if(userProfileUI.updateStatus){
				statusText.text="Update to "+ displayLevel;
			updateCost=modelSelector.GetUpdateCost(objLevel);
		} else {
			updateCost=new Vector3(0,0,0);
			statusText.text="Purchase";
		}
		
		Vector3 selectedObjData=modelSelector.GetModelData(modelSelector.selectedModel);
		costText.text=(selectedObjData.x+updateCost.x).ToString();
		helthText.text=(selectedObjData.y+updateCost.y).ToString();
		damageText.text=(selectedObjData.z+updateCost.z).ToString();

		earnedCoinText.text=earnedCoins.ToString();
	}
}
