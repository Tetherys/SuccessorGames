using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class JsonTest : MonoBehaviour, App42CallBack {

	// Use this for initialization
	void Start () {

		GameBoard board = new GameBoard ();
		StreamReader reader = new StreamReader (Application.dataPath + "/new_game.json");
		
		JsonSerializer js = new JsonSerializer ();
		JsonTextReader jreader = new JsonTextReader (reader);
		board = (GameBoard)js.Deserialize (jreader, typeof(GameBoard));
		reader.Close();

		Debug.Log (board);

		Debug.Log ("castle : " + board.Castle);
		Debug.Log ("castle count : " + board.Castle.TokenStack.Count);

		foreach(KeyValuePair<TokenType, int[]> item in board.Castle.TokenStack)
		{
			Debug.Log(item.Key + " : " + item.Value[0]);
		}

		board.Initialize ();

		string json = JsonConvert.SerializeObject (board, Formatting.Indented, 
		                                           new JsonSerializerSettings { 
			NullValueHandling = NullValueHandling.Ignore,
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
			Converters = new List<Newtonsoft.Json.JsonConverter> {
				new Newtonsoft.Json.Converters.StringEnumConverter()
			}
		});
		File.WriteAllText (Application.dataPath + "/test.json", json);


		NetworkService service = new NetworkService ();
		//service.StorageService.InsertJSONDocument ("FRIENDS", "tom's games", json, this);


	}
	
	// Update is called once per frame
	void Update () {
	
	}



	#region App42CallBack implementation
	public void OnSuccess (object response)
	{
		Debug.Log (response);
		Storage storage = (Storage)response;
		IList<Storage.JSONDocument> jsondoclist = storage.GetJsonDocList ();
		for(int i = 0; i < jsondoclist.Count; i++)
		{
			Debug.Log(jsondoclist[i].GetDocId());
		}
	}
	public void OnException (System.Exception ex)
	{
		App42Exception e = (App42Exception)ex;
		int errorCode = e.GetAppErrorCode ();

		Debug.Log (e.Message);
	}
	#endregion
}
