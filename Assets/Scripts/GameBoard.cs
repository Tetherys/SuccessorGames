using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class GameBoard : MonoBehaviour {

	private const int STARTING_NUMBER_OF_ANIMALS = 5;

	public Market market;
	public PlayerArea playerArea;
	public Area opponentArea;
	private GameBoardState boardState;

	void Start()
	{
		TextAsset boardstate = (TextAsset)Resources.Load ("new_game");
		string text = boardstate.text;
		Initialize((GameBoardState)JsonConvert.DeserializeObject<GameBoardState> (text));
	}

	public void Initialize(GameBoardState state)
	{
		this.boardState = state;
		market.Initialize(boardState.MarketStack, boardState.MarketStalls);

		if(boardState.State == GameState.NEW_GAME)
		{
			boardState.Player1Animals = this.DealStartingAnimals();
			boardState.Player2Animals = this.DealStartingAnimals();
		}
		playerArea.Initialize (boardState.Player1Animals);
		opponentArea.Initialize (boardState.Player2Animals);

		WriteGameBoardStateToFile ();
	}

	private List<AnimalSpecie> DealStartingAnimals()
	{
		List<AnimalSpecie> animals = new List<AnimalSpecie> ();		
		for(int i = 0; i < STARTING_NUMBER_OF_ANIMALS; i++)
		{
			animals.Add(market.GiveSingleAnimalFromStack());
		}
		return animals;
	}

	public void WriteGameBoardStateToFile()
	{
		boardState.MarketStalls = market.GetAnimalSpeciesInMarketStalls ();
		string json = JsonConvert.SerializeObject (this.boardState, Formatting.Indented, 
		                                           new JsonSerializerSettings { 
			NullValueHandling = NullValueHandling.Ignore,
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
			Converters = new List<Newtonsoft.Json.JsonConverter> {
				new Newtonsoft.Json.Converters.StringEnumConverter()
			}
		});
		File.WriteAllText (Application.dataPath + "/test.json", json);
	}

#region Properties

	public GameBoardState BoardState {
		get {
			return this.boardState;
		}
		set {
			boardState = value;
		}
	}
#endregion
}


