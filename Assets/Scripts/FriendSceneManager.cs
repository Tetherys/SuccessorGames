using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;

public class FriendSceneManager : MonoBehaviour, App42CallBack{

	public GameObject friendListItemPrefab;
	public GameObject friendList;

	private UIInput searchInput;
	private UILabel errorLabel;

	// Use this for initialization
	void Start () {
		searchInput = GameObject.Find ("SearchFriendInput").GetComponent<UIInput> ();
		errorLabel = GameObject.Find ("ErrorMessageLabel").GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.LoadLevel ("MainScene");
		}
	}
	
	public void OnAddFriendButtonClicked() 
	{

		string friendName = searchInput.text;
		//string username = GameManager.instance.User.userName;
		if(!string.IsNullOrEmpty(friendName))
		{
			if(friendName.Equals("tom"))
			{
				errorLabel.text = "cant make yourself a friend";
			}
			else if(GameManager.instance.Friends.Contains(friendName))
			{
				errorLabel.text = "is already a friend";
			}
			else
			{
				GameManager.instance.Friends.Add(friendName);
				GameObject go = GameObject.Instantiate(friendListItemPrefab) as GameObject;
				go.GetComponentInChildren<UILabel>().text = friendName;
				Transform t = go.transform;
				t.parent = friendList.transform;
				t.localPosition = Vector3.zero;
				t.localRotation = Quaternion.identity;
				t.localScale = Vector3.one;
				go.layer = friendList.layer;
				friendList.GetComponent<UIGrid>().Reposition ();
			}
		}
		else
		{
			errorLabel.text = "enter a username";
		}
		//GameManager.instance.NetworkService.UserService.GetUser(friendName, this);

	}

	public void OnBackToMainButtonClicked() 
	{
		NGUITools.AddChild (friendList, friendListItemPrefab);
		friendList.GetComponent<UIGrid>().Reposition ();
	}


	public void OnSuccess (object response)
	{
		throw new System.NotImplementedException ();
	}

	public void OnException (Exception ex)
	{
		App42Exception e = (App42Exception)ex;
		int errorCode = e.GetAppErrorCode ();
		switch (errorCode) 
		{
		case NetworkService.ERRORCODE_SERVERERROR:
			errorLabel.text = "No connection to the server";
			break;
		case NetworkService.ERRORCODE_NOINTERNET:
			errorLabel.text = "Check your internet connection";
			break;
		}

		Debug.Log (e.Message);
	}
}
