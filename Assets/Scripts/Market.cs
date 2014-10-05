using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class Market{

	private const int MARKET_STALLS_SIZE = 5;

	private Dictionary<AnimalSpecie, int> marketStack;
	private AnimalSpecie[] marketStalls;
	private List<AnimalSpecie> animals;

	public void Initialize()
	{
		animals = new List<AnimalSpecie> ();
		if(marketStack != null)
		{
			foreach(KeyValuePair<AnimalSpecie, int> item in marketStack)
			{
				for(int i = 0; i < item.Value; i++)
				{
					animals.Add(item.Key);
				}
			}
		}
		ShuffleAnimals ();

		if(marketStalls == null)
		{
			marketStalls = new AnimalSpecie[MARKET_STALLS_SIZE];
			for(int j = 0; j < MARKET_STALLS_SIZE; j++)
			{
				marketStalls[j] = AnimalSpecie.NONE;
			}
			FillEmptyMarketStalls();
		}
	}

	public AnimalSpecie GiveSingleAnimal()
	{
		AnimalSpecie specie = animals [0];
		marketStack [specie] -= 1;
		animals.RemoveAt (0);
		return specie;
	}

	private void ShuffleAnimals()
	{
		System.Random rng = new System.Random();  
		int n = animals.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			AnimalSpecie value = animals[k];  
			animals[k] = animals[n];  
			animals[n] = value;  
		}  
	}

	private void FillEmptyMarketStalls()
	{
		for(int i = 0; i < MARKET_STALLS_SIZE; i++)
		{
			if(marketStalls[i] == AnimalSpecie.NONE)
			{
				marketStalls[i] = GiveSingleAnimal();
			}
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
	
	public AnimalSpecie[] MarketStalls {
		get {
			return this.marketStalls;
		}
		set {
			marketStalls = value;
		}
	}

	[JsonIgnore]
	public List<AnimalSpecie> Animals {
		get {
			return this.animals;
		}
		set {
			animals = value;
		}
	}
}
