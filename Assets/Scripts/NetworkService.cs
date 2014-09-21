using UnityEngine;
using System;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;

public class NetworkService {

	private const string apiKey = "d2331e831af08c7a8200084a42818186a997dd28e854212eb5bc692fca2f6024";
	private const string secretKey = "b30c10e608eac481f8f34c114e9b45afd718e2f7025c2a0b78d0608f5f828b09";
	public const int ERRORCODE_USERNAME_EXISTS = 2001, ERRORCODE_INVALID_USERNAME_PASSWORD = 2002, ERRORCODE_EMAILADRESS_EXISTS = 2005, ERRORCODE_SERVERERROR = 1500, ERRORCODE_NOINTERNET = 0;

	private ServiceAPI service = null;
	private static NetworkService instance = null;

	private UserService userService = null;
	private User user = null;

	private UILabel resultLabel;

	private NetworkService()
	{
		service = new ServiceAPI (apiKey, secretKey);
		userService = service.BuildUserService ();
	}

	public static NetworkService getInstance()
	{
		if (instance == null) 
		{
			instance = new NetworkService();
		}
		return instance;
	}

	public void CreateUser(string username, string emailadress, string password, App42CallBack callBack) 
	{
		try
		{
			userService.CreateUser (username,password,emailadress, callBack);
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
			Debug.Log(resultLabel);
			resultLabel.text = e.Message;
		}
	}

	public UILabel ResultLabel {
		get {
			return this.resultLabel;
		}
		set {
			resultLabel = value;
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

	public UserService UserService {
		get {
			return this.userService;
		}
		set {
			userService = value;
		}
	}
}
