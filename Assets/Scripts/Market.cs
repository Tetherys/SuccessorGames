using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

public class Market: MonoBehaviour{

	private Dictionary<AnimalSpecie, int> marketStack;
	public StallManager stallManager;
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
		return stallManager.GetAnimalsInSelectedStalls ();
	}

	public List<Animal> GetAnimalsBySpecie(AnimalSpecie specie)
	{
		List<Animal> animals = stallManager.GetAnimalsInStallsBySpecie (specie);
		return animals;
	}

	public void PopulateMarketStalls(List<Animal> animals)
	{
		foreach (SingleStall stall in stallManager.GetEmptyStalls())
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
		return stallManager.GetAnimalSpecieFromAnimalsInStalls ();
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
