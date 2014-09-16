using UnityEngine;
using System.Collections;

public class LoginScreen : MenuScreen {

	private UIInput username;
	private UIInput emailaddress;
	private UIInput password;
	private UICheckbox loginCheckBox;
	private UICheckbox registerCheckBox;
	
	// Use this for initialization
	void Start () {
		username = GameObject.Find ("UserNameInput").GetComponent<UIInput> ();
		emailaddress = GameObject.Find ("EmailInput").GetComponent<UIInput> ();
		password = GameObject.Find ("PasswordInput").GetComponent<UIInput> ();
		loginCheckBox = GameObject.Find ("LoginCheckBox").GetComponent<UICheckbox> ();
		registerCheckBox = GameObject.Find ("RegisterCheckBox").GetComponent<UICheckbox> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void login()
	{
		Debug.Log (username.text + emailaddress.text);
	}

	public void OnLoginCheckBoxActivate()
	{
		this.CheckBoxSwapper (loginCheckBox, registerCheckBox);
	}

	public void OnRegisterCheckBoxActivate()
	{
		this.CheckBoxSwapper (registerCheckBox, loginCheckBox);
	}

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


}
