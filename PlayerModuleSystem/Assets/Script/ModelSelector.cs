using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelector : MonoBehaviour {
	public Vector3 axis = new Vector3(0, 1, 0);
	public float speed = 10;
	public List<GameObject> array;
	public GameObject selectedModel=null;
	public Vector3 modelData=new Vector3(0,0,0); //cost, health, damage

	void Start() {
		Reset();
		if (array.Count > 0) {
			array[0].SetActive(true);
		}
	}

	public void Reset() {
		foreach(GameObject g in array) {
			g.SetActive(false);
		}
	}

	public void EnableObject(int index) {
		if (index<0 || index>=array.Count) {
			return;
		}
		Debug.Log(index);
		selectedModel=transform.GetChild(index).gameObject;

		Reset();
		array[index].SetActive(true);
	}		

	public Vector3 GetUpdateCost(int level) {
		Vector3 updateData=new Vector3(0,0,0);
		switch(level){
		case 0:{
				updateData=modelData;			}
			break;
		case 1:{
				updateData=modelData+modelData*.5f;			}
			break;
		case 2:{
				updateData=new Vector3(20,44,120);			}
			break;
		case 3:{
				updateData=new Vector3(20,44,120);			}
			break;
		case 4:{
				updateData=new Vector3(20,44,120);			}
			break;
		
		default:{
				updateData=new Vector3(20,44,120);			}
			break;
		}
		return updateData;
	}

	public Vector3 GetModelData(GameObject model) {
		switch(model.name){
		case "Cube":{
				modelData=new Vector3(20,44,120);
			}
			break;
		case "Sphere":{
				modelData=new Vector3(40,85,250);
			}
			break;
		case "Cylinder":{
				modelData=new Vector3(12,42,80);
			}
			break;
		case "Capsule":{
				modelData=new Vector3(30,82,32);
			}
			break;
		default:{
				Debug.Log(model.name+" No such model");
			}
			break;
		}
		return modelData;
	}

	// Update is called once per frame
	void Update () {
		selectedModel.transform.Rotate(Vector3.up, speed);
	}
}
