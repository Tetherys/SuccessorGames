using UnityEngine;
using System;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class UserResponse : App42CallBack {

	private const int ERRORCODE_USERNAME_EXISTS = 2001, ERRORCODE_INVALID_USERNAME_PASSWORD = 2002, ERRORCODE_EMAILADRESS_EXISTS = 2005;
	private string result = "";
	private string errorMessage = "";
	private UILabel errorLabel = null;

	public void OnSuccess (object response)
	{
		try
		{
			if(response is User)
			{
				User userObj = (User)response;
				result = userObj.ToString();
				if(String.IsNullOrEmpty(userObj.GetSessionId()))
				{
					errorLabel.color = Color.green;
					errorLabel.text = "Succesfully created account with username: " + userObj.GetUserName();
				}
				else
				{
					Application.LoadLevel("MainScene");
				}
			}
		}
		catch(App42Exception e)
		{
			errorMessage = e.GetMessage();
			errorLabel.text = errorMessage;
			Debug.Log(errorMessage);
		}
	}

	//exception handlling
	public void OnException(Exception e)
	{
		//reset errorlabel
		errorMessage = "";
		App42Exception exception = (App42Exception)e;
		
		int errorCode = exception.GetAppErrorCode ();
		
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
		
		errorLabel.text = errorMessage;
		Debug.Log(errorMessage);
	}

	public string Result {
		get {
			return this.result;
		}
		set {
			result = value;
		}
	}

	public string ErrorMessage {
		get {
			return this.errorMessage;
		}
		set {
			errorMessage = value;
		}
	}

	public UILabel ErrorLabel {
		get {
			return this.errorLabel;
		}
		set {
			errorLabel = value;
		}
	}
}
