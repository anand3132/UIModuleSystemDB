using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserLoginUI : MonoBehaviour {
	public GameObject LoginWindow;
	public Text addButtonText;
	public Text userName;

	// Use this for initialization
	void Start () {
		DataService db = DataService.getInstance();
		userName.text = "defaultUser";
		if (db.CreateDB() == true) {
			db.CreateUser(userName.text, 500);
		}
		addButtonText.text="+";
	}

	public string GetUserID() {
		return userName.text;
	}
}
