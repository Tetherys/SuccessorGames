using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	public GameObject gameList;
	public GameObject gameListItemPrefab;
	private UILabel gameListLabel;

	// Use this for initialization
	void Start () {
		gameListLabel = GameObject.Find ("GameListLabel").GetComponent<UILabel>();
		//gameListLabel.text = GameManager.instance.User.GetUserName() + "'s games";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit();
		}
	}

	public void OnCreateGameButtonClicked() 
	{
		NGUITools.AddChild (gameList, gameListItemPrefab);
		gameList.GetComponent<UIGrid>().Reposition ();
	}

	public void OnInstructionsButtonClicked()
	{
		Application.LoadLevel ("InstructionScene");
	}

	public void OnFriendsButtonClicked()
	{
		Application.LoadLevel ("FriendScene");
	}

	public void OnLogoutButtonClicked()
	{
		Application.LoadLevel ("LoginScene");
	}

}
