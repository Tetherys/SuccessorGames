using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.IO;

public class Market: MonoBehaviour{

	private const int MARKET_STALLS_SIZE = 5;

	private Dictionary<AnimalSpecie, int> marketStack;
	public SingleStall[] marketStalls;
	private List<AnimalSpecie> animals;

	void Start()
	{

	}

	public void Initialize(Dictionary<AnimalSpecie, int> marketStack, List<AnimalSpecie> marketStalls)
	{
		this.marketStack = marketStack;

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

		foreach (SingleStall stall in this.marketStalls)
		{
			if (stall.Animal == null)
			{
				GameObject go = new AnimalFactory ().CreateAnimal (GiveSingleAnimalFromStack());
				Transform t = go.transform;
				t.parent = stall.transform;
				t.localPosition = Vector3.zero;
				t.localRotation = Quaternion.identity;

				Animal animal = go.GetComponent<Animal>();
				stall.Animal = animal;
			}
		}

	}

	public AnimalSpecie GiveSingleAnimalFromStack()
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

	public List<AnimalSpecie> GetAnimalSpeciesInMarketStalls()
	{
		List<AnimalSpecie> market = new List<AnimalSpecie> ();
		foreach(SingleStall stall in this.marketStalls)
		{
			market.Add(stall.Animal.specie);
		}
		return market;
	}

	public Dictionary<AnimalSpecie, int> MarketStack {
		get {
			return this.marketStack;
		}
		set {
			marketStack = value;
		}
	}

	public List<AnimalSpecie> Animals {
		get {
			return this.animals;
		}
		set {
			animals = value;
		}
	}
}
