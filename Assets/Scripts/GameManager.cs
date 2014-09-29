using UnityEngine;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.user;

public class GameManager : MonoBehaviour {

	public static GameManager instance{ get; private set;}

	//services
	private NetworkService networkService;

	//persistence game data
	private User user;
	public const string DATABASE_NAME = "FRIENDS";
	public const string FRIEND_FILE_PATH = "/friends.json";

	private HashSet<string> friends = new HashSet<string>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake()
	{
		if (instance != null && instance != this) 
		{
			Destroy(gameObject);
		}

		instance = this;
		networkService = new NetworkService ();
		DontDestroyOnLoad (gameObject);
	}

	void OnDestroy()
	{
		instance = null;
		networkService = null;
	}

	public NetworkService NetworkService {
		get {
			return this.networkService;
		}
		set {
			networkService = value;
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

	public HashSet<string> Friends {
		get {
			return this.friends;
		}
		set {
			friends = value;
		}
	}
}
