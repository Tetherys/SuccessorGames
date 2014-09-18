using UnityEngine;
using System.Collections;

public class FriendScreen : MenuScreen {

	public GameObject friendListItemPrefab;
	public GameObject friendList;

	// Use this for initialization
	void Start () {
	
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
		NGUITools.AddChild (friendList, friendListItemPrefab);
		friendList.GetComponent<UIGrid>().Reposition ();
	}

	public void OnBackToMainButtonClicked() 
	{
		Application.LoadLevel ("MainScene");
	}
}
