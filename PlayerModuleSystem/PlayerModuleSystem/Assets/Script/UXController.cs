using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UXController : MonoBehaviour {
	public Button addToProfileButton;
	public SelectionProfileUI selectionProfileUI;
	public UserProfileUI userProfileUI;
	public UserLoginUI loginProfileUI;
	public ModelSelector modelSet;

	// Use this for initialization
	void Start () {
		SwitchToUserLogin();
	}

	public void SwitchToUserLogin(){
		loginProfileUI.gameObject.SetActive(true);
		userProfileUI.gameObject.SetActive(false);
		selectionProfileUI.gameObject.SetActive(false);
		modelSet.gameObject.SetActive(false);
	}
	public void SwitchToUserProfileUI(){
		loginProfileUI.gameObject.SetActive(false);
		userProfileUI.gameObject.SetActive(true);
		selectionProfileUI.gameObject.SetActive(false);
		modelSet.gameObject.SetActive(false);
	}
	public void SwitchToSelectionProfileUI(){
		loginProfileUI.gameObject.SetActive(false);
		userProfileUI.gameObject.SetActive(false);
		selectionProfileUI.gameObject.SetActive(true);
		modelSet.gameObject.SetActive(true);
	}

	public void AddButtonPressed(){
		selectionProfileUI.AddObject();
		SwitchToUserProfileUI();
	}
	public void PlayButtonPressed(){
		
		Debug.Log("As there is no game I am incrementing the level");
	}
}
