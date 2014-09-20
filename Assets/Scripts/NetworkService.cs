using UnityEngine;
using System;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class NetworkService {

	private const string apiKey = "d2331e831af08c7a8200084a42818186a997dd28e854212eb5bc692fca2f6024";
	private const string secretKey = "b30c10e608eac481f8f34c114e9b45afd718e2f7025c2a0b78d0608f5f828b09";

	private ServiceAPI service = null;
	private static NetworkService instance = null;

	private UserService userService = null;

	private User user = null;
	private UserResponse userResponse = null;

	private UILabel resultLabel;

	private NetworkService()
	{
		service = new ServiceAPI (apiKey, secretKey);
		userService = service.BuildUserService ();
		userResponse = new UserResponse ();
	}

	public static NetworkService getInstance()
	{
		if (instance == null) 
		{
			instance = new NetworkService();
		}
		return instance;
	}

	public void CreateUser(string username, string emailadress, string password) 
	{
		try
		{
			userService.CreateUser (username,password,emailadress, new UserResponse());
		}
		catch(App42Exception e)
		{
			Debug.Log(e.Message);
			resultLabel.text = e.Message;
		}
	}

	public void Authenticate(string username, string password)
	{
		try
		{
			userService.Authenticate(username, password, new UserResponse());
		}
		catch(App42Exception e)
		{
			Debug.Log(resultLabel);
			resultLabel.text = e.Message;
		}
	}

	public UILabel ResultLabel {
		get {
			return this.resultLabel;
		}
		set {
			Debug.Log ("setting result label" + value);
			resultLabel = value;
			userResponse.ErrorLabel = value;
		}
	}

	public User User {
		get {
			return this.user;
		}
		set {
			user = value;
		}
	}
}
