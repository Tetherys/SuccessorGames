using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerCart {
	
	private const int MAX_CART_SIZE = 7;

	private List<AnimalSpecie> animals;
	private List<Token> tokens;
	
	private int donkeyCount;




	public List<AnimalSpecie> Animals {
		get {
			return this.animals;
		}
		set {
			animals = value;
		}
	}

	public List<Token> Tokens {
		get {
			return this.tokens;
		}
		set {
			tokens = value;
		}
	}

	[JsonIgnore]
	public int DonkeyCount {
		get {
			return this.donkeyCount;
		}
		set {
			donkeyCount = value;
		}
	}
}
