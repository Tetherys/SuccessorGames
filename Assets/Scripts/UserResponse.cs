using UnityEngine;
using System;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class UserResponse : App42CallBack {

	private const int ERRORCODE_USERNAME_EXISTS = 2001, ERRORCODE_EMAILADRESS_EXISTS = 2005;
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
				errorLabel.color = Color.green;
				errorLabel.text = "Succesfully created account with username: " + userObj.GetUserName();
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
		App42Exception exception = (App42Exception)e.GetBaseException();

		int errorCode = exception.GetAppErrorCode ();

		switch (errorCode) 
		{
			case ERRORCODE_USERNAME_EXISTS:
				errorMessage = "Username already taken";
				break;
			case ERRORCODE_EMAILADRESS_EXISTS:
				errorMessage = "EmailAdress already taken";
				break;
		}

		errorLabel.text = errorMessage;
		Debug.Log(errorMessage);
	}

	public void setErrorMessage(string message)
	{
		errorMessage = message;
	}

	public string getResult()
	{
		return result;
	}

	public string getErrorMessage()
	{
		return errorMessage;
	}

	public void setErrorLabel(UILabel label)
	{
		errorLabel = label;
	}
}
