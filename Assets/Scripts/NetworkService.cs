using UnityEngine;
using System;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class NetworkService : MonoBehaviour, App42CallBack{

	private string apiKey = "d2331e831af08c7a8200084a42818186a997dd28e854212eb5bc692fca2f6024";
	private string secretKey = "b30c10e608eac481f8f34c114e9b45afd718e2f7025c2a0b78d0608f5f828b09";

	private const int ERRORCODE_USERNAME_EXISTS = 2001, ERRORCODE_INVALID_USERNAME_PASSWORD = 2002, ERRORCODE_EMAILADRESS_EXISTS = 2005;

	private ServiceAPI service = null;
	private UserService userService = null;

	private User user = null;

	private UILabel resultLabel;

	// Use this for initialization
	void Start () {
		service = new ServiceAPI (apiKey, secretKey);
		userService = service.BuildUserService ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateUser(string username, string emailadress, string password) 
	{
		try
		{
			userService.CreateUser (username,password,emailadress,this);
		}
		catch(App42Exception e)
		{
			Debug.Log(e.Message);
			resultLabel.text = e.Message;
		}
	}

	public void Authenticate(string username, string password, App42CallBack callBack)
	{
		try
		{
			userService.Authenticate(username, password, callBack);
		}
		catch(App42Exception e)
		{
			Debug.Log(e.Message);
			resultLabel.text = e.Message;
		}
	}

	public void setResultLabel(UILabel label)
	{
		resultLabel = label;
	}

	#region App42CallBack implementation

	public void OnSuccess (object response)
	{
		try
		{
			if(response is User)
			{
				user = (User)response;
				resultLabel.color = Color.green;
				resultLabel.text = "Succesfully created account with username: " + user.GetUserName();
			}
		}
		catch(App42Exception e)
		{
			resultLabel.text = e.Message;
			Debug.Log(e.Message);
		}
	}
	
	//exception handlling
	public void OnException(Exception e)
	{
		App42Exception exception = (App42Exception)e;
		
		int errorCode = exception.GetAppErrorCode ();
		string errorMessage = "";

		switch (errorCode) 
		{
		case ERRORCODE_USERNAME_EXISTS:
			errorMessage = "Username already taken";
			break;
		case ERRORCODE_EMAILADRESS_EXISTS:
			errorMessage = "EmailAdress already taken";
			break;
		case ERRORCODE_INVALID_USERNAME_PASSWORD:
			errorMessage = "Username/password dont match";
			break;
		}
		
		resultLabel.text = errorMessage;
		Debug.Log(errorMessage);
	}

	#endregion

	User User {
		get {
			return this.user;
		}
		set {
			user = value;
		}
	}
}
