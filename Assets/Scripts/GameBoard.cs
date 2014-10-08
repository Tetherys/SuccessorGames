using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GameBoard {
	
	private const int STARTING_NUMBER_OF_ANIMALS = 5;

	private GameState state;
	private string player1Username, player2Username;
	private int turn;

	//private Market market;
	private Castle castle;
	private PlayerCart player1, player2;

	public void Initialize()
	{
		player1 = new PlayerCart ();
		player2 = new PlayerCart ();
		//market.Initialize ();
		if (state == GameState.NEW_GAME) 
		{
			DealStartingAnimals(player1);
			DealStartingAnimals(player2);
		}
	}

	private void DealStartingAnimals(PlayerCart cart)
	{
		List<AnimalSpecie> animals = new List<AnimalSpecie> ();		
		for(int i = 0; i < STARTING_NUMBER_OF_ANIMALS; i++)
		{
			//animals.Add(market.GiveSingleAnimal());
		}
		cart.Animals = animals;
	}

#region Properties

	public GameState State {
		get {
			return this.state;
		}
		set {
			state = value;
		}
	}

	public string Player1Username {
		get {
			return this.player1Username;
		}
		set {
			player1Username = value;
		}
	}

	public string Player2Username {
		get {
			return this.player2Username;
		}
		set {
			player2Username = value;
		}
	}

	public int Turn {
		get {
			return this.turn;
		}
		set {
			turn = value;
		}
	}
	/*
	public Market Market {
		get {
			return this.market;
		}
		set {
			market = value;
		}
	}
*/
	public Castle Castle {
		get {
			return this.castle;
		}
		set {
			castle = value;
		}
	}

	public PlayerCart Player1 {
		get {
			return this.player1;
		}
		set {
			player1 = value;
		}
	}

	public PlayerCart Player2 {
		get {
			return this.player2;
		}
		set {
			player2 = value;
		}
	} 
#endregion
}


