using UnityEngine;
using System.Collections.Generic;

//class that holds all necessary info to create the gameboard
public class GameBoardState 
{
	private GameState state;
	private string player1;
	private string player2;
	private int turn;
	private Dictionary<AnimalSpecie, int> marketStack;
	private List<AnimalSpecie> marketStalls, player1Animals, player2Animals;
	private Dictionary<TokenType, int[]> tokenStack;
	private List<Token> player1Tokens, player2Tokens;

	public GameState State {
		get {
			return this.state;
		}
		set {
			state = value;
		}
	}

	public string Player1 {
		get {
			return this.player1;
		}
		set {
			player1 = value;
		}
	}

	public string Player2 {
		get {
			return this.player2;
		}
		set {
			player2 = value;
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

	public Dictionary<AnimalSpecie, int> MarketStack {
		get {
			return this.marketStack;
		}
		set {
			marketStack = value;
		}
	}

	public List<AnimalSpecie> MarketStalls {
		get {
			return this.marketStalls;
		}
		set {
			marketStalls = value;
		}
	}

	public List<AnimalSpecie> Player1Animals {
		get {
			return this.player1Animals;
		}
		set {
			player1Animals = value;
		}
	}

	public List<AnimalSpecie> Player2Animals {
		get {
			return this.player2Animals;
		}
		set {
			player2Animals = value;
		}
	}

	public Dictionary<TokenType, int[]> TokenStack {
		get {
			return this.tokenStack;
		}
		set {
			tokenStack = value;
		}
	}

	public List<Token> Player1Tokens {
		get {
			return this.player1Tokens;
		}
		set {
			player1Tokens = value;
		}
	}

	public List<Token> Player2Tokens {
		get {
			return this.player2Tokens;
		}
		set {
			player2Tokens = value;
		}
	}
}
