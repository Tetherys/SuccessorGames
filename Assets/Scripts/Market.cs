using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.IO;

public class Market: MonoBehaviour{

	private const int MARKET_STALLS_SIZE = 5;

	private Dictionary<AnimalSpecie, int> marketStack;
	public MarketStall[] marketStalls;
	private List<AnimalSpecie> animals;

	void Start()
	{
		Debug.Log ("start");
		Initialize ();
	}

	public void Initialize()
	{
		Debug.Log ("initialize");
		animals = new List<AnimalSpecie> ();
		if(marketStack == null)
		{
			StreamReader reader = new StreamReader (Application.dataPath + "/test.json");
			
			JsonSerializer js = new JsonSerializer ();
			JsonTextReader jreader = new JsonTextReader (reader);
			marketStack = (Dictionary<AnimalSpecie, int>)js.Deserialize (jreader, typeof(Dictionary<AnimalSpecie, int>));
			reader.Close();
		}

		if(marketStack != null)
		{
			foreach(KeyValuePair<AnimalSpecie, int> item in marketStack)
			{
				for(int i = 0; i < item.Value; i++)
				{
					animals.Add(item.Key);
				}
			}
			Debug.Log(animals.Count);
		}
		ShuffleAnimals ();

		foreach (MarketStall stall in marketStalls)
		{
			if (stall.Animal == null)
			{
				GameObject go = new AnimalFactory ().CreateAnimal (GiveSingleAnimal());
				Transform t = go.transform;
				t.parent = stall.transform;
				t.localPosition = Vector3.zero;
				t.localRotation = Quaternion.identity;
			}
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



	public Dictionary<AnimalSpecie, int> MarketStack {
		get {
			return this.marketStack;
		}
		set {
			marketStack = value;
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
