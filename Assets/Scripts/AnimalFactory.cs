using UnityEngine;
using System.Collections.Generic;

public class AnimalFactory {

	private Dictionary<AnimalSpecie, string> animalPrefabs =  new Dictionary<AnimalSpecie, string>()
	{
		{ AnimalSpecie.CHICKEN, "Chicken" },
		{ AnimalSpecie.COW , "Cow" },
		{ AnimalSpecie.DONKEY, "Donkey" },
		{ AnimalSpecie.GOAT , "Goat" },
		{ AnimalSpecie.LAMA, "Lama" },
		{ AnimalSpecie.HORSE , "Horse" },
		{ AnimalSpecie.PIG , "Pig" }
	};

	public GameObject CreateAnimal(AnimalSpecie specie)
	{
		return (GameObject)GameObject.Instantiate (Resources.Load(animalPrefabs[specie]));
	}
}
