using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class JsonTest : MonoBehaviour, App42CallBack {

	// Use this for initialization
	void Start () {

		GameBoardState board = new GameBoardState ();
		TextAsset boardstate = (TextAsset)Resources.Load ("new_game");
		string text = boardstate.text;
		board = (GameBoardState)JsonConvert.DeserializeObject<GameBoardState> (text);

		Debug.Log (board.State);;
		Debug.Log ("castle count : " + board.TokenStack.Count);

		foreach(KeyValuePair<TokenType, int[]> item in board.TokenStack)
		{
			Debug.Log(item.Key + " : " + item.Value[0]);
		}



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
