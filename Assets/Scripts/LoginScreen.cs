using UnityEngine;
using System.Collections;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;


public class LoginScreen : MenuScreen, App42CallBack {

	private GameObject username;
	private GameObject emailaddress;
	private GameObject emailaddressLabel;
	private GameObject password;

	private UICheckbox loginCheckBox;
	private UICheckbox registerCheckBox;

	private NetworkService networkService;

	private UILabel errorMessage;
	
	// Use this for initialization
	void Start () {
		username = GameObject.Find ("UserNameInput");
		emailaddress = GameObject.Find ("EmailInput");
		emailaddressLabel = GameObject.Find ("EmailLabel");
		password = GameObject.Find ("PasswordInput");
		loginCheckBox = GameObject.Find ("LoginCheckBox").GetComponent<UICheckbox> ();
		registerCheckBox = GameObject.Find ("RegisterCheckBox").GetComponent<UICheckbox> ();
		networkService = GameObject.Find ("NetworkService").GetComponent<NetworkService> ();
		errorMessage = GameObject.Find ("ErrorMessageLabel").GetComponent<UILabel>();
		networkService.setResultLabel (errorMessage);

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
			networkService.CreateUser (user, email, pw, this);
		}
		else
		{
			networkService.Authenticate(user, pw);
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
		throw new NotImplementedException ();
	}

	public void OnException (Exception ex)
	{
		throw new NotImplementedException ();
	}

	#endregion
}
