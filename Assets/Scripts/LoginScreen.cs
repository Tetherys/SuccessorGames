using UnityEngine;
using System.Collections;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;


public class LoginScreen : MonoBehaviour, App42CallBack{

	private GameObject username;
	private GameObject emailaddress;
	private GameObject emailaddressLabel;
	private GameObject password;

	private UICheckbox loginCheckBox;
	private UICheckbox registerCheckBox;

	private UILabel errorMessage;
	
	// Use this for initialization
	void Start () {
		username = GameObject.Find ("UserNameInput");
		emailaddress = GameObject.Find ("EmailInput");
		emailaddressLabel = GameObject.Find ("EmailLabel");
		password = GameObject.Find ("PasswordInput");
		loginCheckBox = GameObject.Find ("LoginCheckBox").GetComponent<UICheckbox> ();
		registerCheckBox = GameObject.Find ("RegisterCheckBox").GetComponent<UICheckbox> ();
		errorMessage = GameObject.Find ("ErrorMessageLabel").GetComponent<UILabel>();
		NetworkService.getInstance().ResultLabel = errorMessage;

		this.SetEmailAdressInput (false);
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape) )
		{
			Application.Quit();
		}

	}

	/*
	 * Called when the submit button is clicked
	 */
	public void Submit()
	{
		string user = username.GetComponent<UIInput> ().text;
		string pw = password.GetComponent<UIInput> ().text;
		string email = emailaddress.GetComponent<UIInput> ().text;

		if(registerCheckBox.isChecked == true)
		{
			NetworkService.getInstance().CreateUser(user, email, pw, this);
		}
		else
		{
			NetworkService.getInstance().Authenticate(user, pw, this);
		}
		//Application.LoadLevel("MainScene");
	}

	/*
	 * 
	 */
	public void Login()
	{
		
	}

	/*
	 * 
	 */
	public void Register()
	{
		
	}

	/*
	 * Called when the login checkbox is pressed
	 */
	public void OnLoginCheckBoxActivate()
	{
		this.CheckBoxSwapper (loginCheckBox, registerCheckBox);
	}

	/*
	 * Called when the register checkbox is pressed
	 */
	public void OnRegisterCheckBoxActivate()
	{
		this.CheckBoxSwapper (registerCheckBox, loginCheckBox);
		if(registerCheckBox != null)
		{
			if(registerCheckBox.isChecked == true)
			{
				this.SetEmailAdressInput(true);
			}
			else
			{
				this.SetEmailAdressInput(false);
			}
		}
	}

	/*
	 * shows/hides the emailaddres label and input
	 */ 
	private void SetEmailAdressInput(bool active)
	{
		NGUITools.SetActive (emailaddress, active);
		NGUITools.SetActive (emailaddressLabel, active);
	}

	/*
	 * Swaps 2 checkboxes from active to unactive or vice versa, so that 1 of the checkboxes is always active 
	 */
	private void CheckBoxSwapper(UICheckbox active, UICheckbox unactive)
	{
		if(active != null)
		{
			if(active.isChecked == true)
			{
				if(unactive.isChecked == true)
				{
					unactive.isChecked = false;
				}
			}
			else
			{
				if(unactive.isChecked == false)
				{
					unactive.isChecked = true;
				}
			}
		}
	}
	
	#region App42CallBack implementation
	public void OnSuccess (object response)
	{
		try
		{
			if(response is User)
			{
				User userObj = (User)response;
				NetworkService.getInstance().User = userObj;
				if(String.IsNullOrEmpty(userObj.GetSessionId()))
				{
					errorMessage.color = Color.green;
					errorMessage.text = "Succesfully created account with username: " + userObj.GetUserName();
				}
				else
				{
					Application.LoadLevel("MainScene");
				}
			}
		}
		catch(App42Exception e)
		{
			errorMessage.text = e.Message;
		}
	}
	public void OnException (Exception ex)
	{
		errorMessage.text = "";
		App42Exception exception = (App42Exception)ex;
		int errorCode = exception.GetAppErrorCode ();
		switch (errorCode) 
		{
		case NetworkService.ERRORCODE_USERNAME_EXISTS:
			errorMessage.text = "Username already taken";
			break;
		case NetworkService.ERRORCODE_EMAILADRESS_EXISTS:
			errorMessage.text = "EmailAdress already taken";
			break;
		case NetworkService.ERRORCODE_INVALID_USERNAME_PASSWORD:
			errorMessage.text = "Username/password dont match";
			break;
		case NetworkService.ERRORCODE_SERVERERROR:
			errorMessage.text = "No connection to the server";
			break;
		case NetworkService.ERRORCODE_NOINTERNET:
			errorMessage.text = "Check your internet connection";
			break;
		}

	}
	#endregion
}
