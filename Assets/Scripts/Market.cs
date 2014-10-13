using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.IO;

public class Market: MonoBehaviour{

	private Dictionary<AnimalSpecie, int> marketStack;
	public SingleStall[] marketStalls;
	private List<AnimalSpecie> animals;
	
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

		PopulateMarketStalls (null);

	}

	public Animal GiveSingleAnimalFromStack()
	{
		Animal animal = new AnimalFactory ().CreateAnimal (GiveSingleAnimalSpecieFromStack()).GetComponent<Animal>();
		return animal;
	}

	public AnimalSpecie GiveSingleAnimalSpecieFromStack()
	{
		AnimalSpecie specie =  this.animals[0];
		marketStack [specie] -= 1;
		animals.RemoveAt (0);
		return specie;
	}

	public List<Animal> GetSelectedAnimals()
	{
		List<Animal> selectedAnimals = new List<Animal> ();
		foreach(SingleStall stall in marketStalls)
		{
			if(stall.Selected)
			{
				selectedAnimals.Add(stall.Animal);
				stall.Selected = !stall.Selected;
				stall.Animal = null;
			}
		}
		return selectedAnimals;
	}

	public List<Animal> GetAnimalsBySpecie(AnimalSpecie specie)
	{
		List<Animal> animalsBySpecie = new List<Animal>();
		foreach (SingleStall stall in this.marketStalls)
		{
			if (stall.Animal != null)
			{
				if(stall.Animal.specie == specie)
				{
					animalsBySpecie.Add(stall.Animal);
					stall.Animal = null;
				}
			}
		}
		return animalsBySpecie;
	}

	public void PopulateMarketStalls(List<Animal> animals)
	{
		foreach (SingleStall stall in this.marketStalls)
		{
			if (stall.Animal == null)
			{
				if(animals == null)
				{
					stall.Animal = GiveSingleAnimalFromStack();
				}
				else
				{
					stall.Animal = animals[0];
					animals.RemoveAt(0);
				}
			}
		}
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
