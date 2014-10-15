using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class GameBoard : MonoBehaviour {

	public UIButton actionButton;
	public UILabel actionButtonLabel;

	private const int STARTING_NUMBER_OF_ANIMALS = 5;

	public Market market;
	public Castle castle;
	public PlayerArea playerArea;
	public MultiStall opponentArea;
	private GameBoardState boardState;

	public delegate void Action();
	Action buttonAction;

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
		castle.TokenStack = state.TokenStack;

		if(boardState.State == GameState.NEW_GAME)
		{
			boardState.Player1Animals = this.DealStartingAnimals();
			boardState.Player2Animals = this.DealStartingAnimals();
		}
		playerArea.Initialize (boardState.Player1Animals);

		foreach(Stall stall in market.stallManager.stalls)
		{
			stall.OnSelect += this.UpdateAction;
		}

		foreach(Stall stall in playerArea.playerStalls.stalls)
		{
			stall.OnSelect += this.UpdateAction;
		}

		playerArea.donkeyHerd.OnSelect += this.UpdateAction;
		WriteGameBoardStateToFile ();
	}

	private void UpdateAction ()
	{
		CheckForPossibleAction ();
	}

	private List<AnimalSpecie> DealStartingAnimals()
	{
		List<AnimalSpecie> animals = new List<AnimalSpecie> ();		
		for(int i = 0; i < STARTING_NUMBER_OF_ANIMALS; i++)
		{
			animals.Add(market.GiveSingleAnimalSpecieFromStack());
		}
		return animals;
	}

	public void WriteGameBoardStateToFile()
	{
		boardState.MarketStalls = market.GetAnimalSpeciesInMarketStalls ();
		boardState.Player1Animals = playerArea.GetAnimalSpeciesInPlayerStalls ();

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

	public void OnTakeDonkeysButtonClicked()
	{
		playerArea.AddAnimalsToPlayerArea(market.GetAnimalsBySpecie (AnimalSpecie.DONKEY));
		market.PopulateMarketStalls (null);
		WriteGameBoardStateToFile ();
	}

	public void OnTakeAnimalButtonClicked()
	{
		playerArea.AddAnimalsToPlayerArea (market.GetSelectedAnimals());
		market.PopulateMarketStalls (null);
		WriteGameBoardStateToFile ();
	}

	public void OnTradeAnimalsButtonClicked()
	{
		List<Animal> selectedMarketAnimals = market.GetSelectedAnimals ();
		List<Animal> selectedPlayerAnimals = playerArea.GetSelectedAnimals (selectedMarketAnimals.Count);
		market.PopulateMarketStalls (selectedPlayerAnimals);
		playerArea.AddAnimalsToPlayerArea (selectedMarketAnimals);
		WriteGameBoardStateToFile ();
	}

	public void OnAnimalsForTokensButtonClicked()
	{
		List<Animal> selectedPlayerAnimals = playerArea.GetSelectedAnimals (0);
		boardState.Player1Tokens  = castle.TradeAnimalsForTokens (selectedPlayerAnimals);
		WriteGameBoardStateToFile ();
	}

	private void CheckForPossibleAction()
	{
		EnableActionButton (false, "");
		if (playerArea.AnythingSelectedInPlayerArea ()) 
		{
			if(market.stallManager.GetNumberOfSelectedStalls() == 1 && market.stallManager.GetNumberOfSelectedStallBySpecie(AnimalSpecie.DONKEY) == 0)
			{
				EnableActionButton(true, "take animal");
				buttonAction = OnTakeAnimalButtonClicked;
			}

			if(market.stallManager.GetNumberOfSelectedStallBySpecie(AnimalSpecie.DONKEY) == market.stallManager.GetNumberOfSelectedStalls() && market.stallManager.GetNumberOfSelectedStalls() >= 1)
			{
				EnableActionButton(true, "take all \n donkeys");
				buttonAction = OnTakeDonkeysButtonClicked;
			}
		}
		else
		{
			if(market.stallManager.GetNumberOfSelectedStalls() == 0 )
			{
				if(playerArea.playerStalls.CheckIfSelectedAnimalsAreSameSpecie() && !playerArea.donkeyHerd.Selected)
				{
					if(playerArea.playerStalls.CheckIfSelectedAnimalsHaveMinimumNumber(AnimalType.EXPENSIVE, 2) || playerArea.playerStalls.CheckIfSelectedAnimalsHaveMinimumNumber(AnimalType.CHEAP, 1))
					{
						EnableActionButton(true, "turn in animals \n for tokens");
						buttonAction = OnAnimalsForTokensButtonClicked;
					}
				}
			}
			else
			{
				int numberOfSelectedMarketStalls = market.stallManager.GetNumberOfSelectedStalls();
				int numberOfSelectedPlayerStalls = playerArea.playerStalls.GetNumberOfSelectedStalls();
				bool donkeyHerdSelected = playerArea.donkeyHerd.Selected;
				int numberOfDonkey = playerArea.donkeyHerd.Animals.Count;
				if(!donkeyHerdSelected)
				{
					if(numberOfSelectedMarketStalls == numberOfSelectedPlayerStalls)
					{
						EnableActionButton(true, "trade animals");
						buttonAction = OnTradeAnimalsButtonClicked;
					}
				}
				else
				{
					if((numberOfSelectedMarketStalls - (numberOfDonkey + numberOfSelectedPlayerStalls)) <= 0 
					   && (numberOfSelectedPlayerStalls - numberOfSelectedMarketStalls) < 0)
					{
						EnableActionButton(true, "trade animals");
						buttonAction = OnTradeAnimalsButtonClicked;
					}
				}
			}
		}
	}

	private void EnableActionButton(bool enable, string message)
	{
		actionButton.gameObject.SetActive (enable);
		if(enable)
		{
			actionButtonLabel.text = message;
		}
	}

	public void OnActionButtonClicked()
	{
		buttonAction ();
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


