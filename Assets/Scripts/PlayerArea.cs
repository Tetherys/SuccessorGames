using UnityEngine;
using System.Collections.Generic;

public class PlayerArea : MonoBehaviour {

	public SingleStall[] playerStalls;
	public MultiStall donkeyHerd;

	public void Initialize(List<AnimalSpecie> animals)
	{
		foreach(AnimalSpecie specie in animals)
		{
			Animal animal = new AnimalFactory().CreateAnimal(specie).GetComponent<Animal>();
			this.AddAnimalToStalls(animal);
		}
	}

	public void AddAnimalToStalls(Animal animal)
	{
		if(animal.specie == AnimalSpecie.DONKEY)
		{
			this.donkeyHerd.AddAnimal(animal);
		}
		else
		{
			SingleStall stall = this.GetEmptyStall ();
			
			if(stall != null)
			{
				stall.Animal = animal;
			}
		}
	}

	public void AddAnimalsToStalls(List<Animal> animals)
	{
		foreach(Animal animal in animals)
		{
			this.AddAnimalToStalls(animal);
		}
	}

	public List<Animal> GetSelectedAnimals(int maxSize)
	{
		List<Animal> selectedAnimals = new List<Animal> ();
		foreach(SingleStall stall in playerStalls)
		{
			if(stall.Selected)
			{
				selectedAnimals.Add(stall.Animal);
				stall.Selected = !stall.Selected;
				stall.Animal = null;
			}
		}
		if(selectedAnimals.Count < maxSize)
		{
			Debug.Log("adding donkeys");
			if(donkeyHerd.Selected)
			{
				for(int i = selectedAnimals.Count; i < maxSize; i++)
				{
					selectedAnimals.Add(donkeyHerd.Animals[donkeyHerd.Animals.Count - 1]);
					donkeyHerd.Animals.RemoveAt(donkeyHerd.Animals.Count - 1);
					Debug.Log("added donkey");
				}
				donkeyHerd.Selected = !donkeyHerd.Selected;
			}
		}

		return selectedAnimals;
	}

	public List<AnimalSpecie> GetAnimalSpeciesInPlayerStalls()
	{
		List<AnimalSpecie> market = new List<AnimalSpecie> ();
		foreach(SingleStall stall in this.playerStalls)
		{
			if(stall.Animal != null)
				market.Add(stall.Animal.specie);
		}
		foreach(Animal animal in donkeyHerd.Animals)
		{
			market.Add (AnimalSpecie.DONKEY);
		}

		return market;
	}


	private SingleStall GetEmptyStall()
	{
		foreach(SingleStall stall in playerStalls)
		{
			if(stall.Animal == null)
			{
				return stall;
			}
		}
		return null;
	}
}
