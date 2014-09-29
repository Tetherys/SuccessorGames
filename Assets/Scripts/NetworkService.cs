using UnityEngine;
using System;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class NetworkService {

	private const string API_KEY = "d2331e831af08c7a8200084a42818186a997dd28e854212eb5bc692fca2f6024";
	private const string SECRET_KEY = "b30c10e608eac481f8f34c114e9b45afd718e2f7025c2a0b78d0608f5f828b09";
	public const int ERRORCODE_USERNAME_EXISTS = 2001, ERRORCODE_INVALID_USERNAME_PASSWORD = 2002, ERRORCODE_EMAILADRESS_EXISTS = 2005, ERRORCODE_SERVERERROR = 1500, ERRORCODE_NOINTERNET = 0
		               , ERRORCODE_USERNAME_NOT_FOUND = 2000;

	private ServiceAPI service = null;

	private UserService userService = null;
	private StorageService storageService = null;

	public NetworkService()
	{
		service = new ServiceAPI (API_KEY, SECRET_KEY);
		userService = service.BuildUserService ();
		storageService = service.BuildStorageService();
	}

	public UserService UserService {
		get {
			return this.userService;
		}
		set {
			userService = value;
		}
	}

	public StorageService StorageService {
		get {
			return this.storageService;
		}
		set {
			storageService = value;
		}
	}
}


