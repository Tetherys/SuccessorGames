using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using System.IO;
using Newtonsoft.Json;

public class FriendSceneManager : MonoBehaviour, App42CallBack{

	public GameObject friendListItemPrefab;
	public GameObject friendList;

	private UIInput searchInput;
	private UILabel errorLabel;

	// Use this for initialization
	void Start () {
		searchInput = GameObject.Find ("SearchFriendInput").GetComponent<UIInput> ();
		errorLabel = GameObject.Find ("ErrorMessageLabel").GetComponent<UILabel> ();
		if(File.Exists(Application.persistentDataPath + GameManager.FRIEND_FILE_PATH))
		{
			this.LoadFriendsFromFile (Application.persistentDataPath + GameManager.FRIEND_FILE_PATH);
		}
		foreach(string friend in GameManager.instance.Friends)
		{
			AddFriendToFriendList(friend);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.LoadLevel ("MainScene");
		}
	}

	void OnDestroy()
	{
		//this.WriteFriendsToFile (Application.dataPath + GameManager.FRIEND_FILE_PATH);	
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
				this.AddFriendToFriendList(friendName);
				this.WriteFriendsToFile (Application.persistentDataPath + GameManager.FRIEND_FILE_PATH);
			}
		}
		else
		{
			errorLabel.text = "enter a username";
		}
		//GameManager.instance.NetworkService.UserService.GetUser(friendName, this);

	}

	private void AddFriendToFriendList(string friendName)
	{
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

	public void OnBackToMainButtonClicked() 
	{

	}

	private void LoadFriendsFromFile(string path)
	{
		StreamReader reader = new StreamReader (path);

		JsonSerializer js = new JsonSerializer ();
		JsonTextReader jreader = new JsonTextReader (reader);
		GameManager.instance.Friends = (HashSet<string>)js.Deserialize (jreader, typeof(HashSet<string>));
	}

	private void WriteFriendsToFile(string path)
	{
		string json = JsonConvert.SerializeObject (GameManager.instance.Friends, Formatting.Indented);
		File.WriteAllText (path, json);
	}

	public void OnSuccess (object response)
	{
		errorLabel.text = "Friend added";

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
		case NetworkService.ERRORCODE_USERNAME_NOT_FOUND:
			errorLabel.text = "User not found";
			break;
		}


		Debug.Log (e.Message);
	}
}
