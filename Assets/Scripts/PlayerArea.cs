using UnityEngine;
using System.Collections.Generic;

public class PlayerArea : MonoBehaviour {

	public StallManager playerStalls;
	public MultiStall donkeyHerd;

	public void Initialize(List<AnimalSpecie> animals)
	{
		foreach(AnimalSpecie specie in animals)
		{
			Animal animal = new AnimalFactory().CreateAnimal(specie).GetComponent<Animal>();
			this.AddAnimalToPlayerArea(animal);
		}
	}

	public void AddAnimalToPlayerArea(Animal animal)
	{
		if(animal.specie == AnimalSpecie.DONKEY)
		{
			this.donkeyHerd.AddAnimal(animal);
		}
		else
		{
			playerStalls.AddAnimalToStalls(animal);
		}
	}

	public void AddAnimalsToPlayerArea(List<Animal> animals)
	{
		animals.ForEach (animal => this.AddAnimalToPlayerArea(animal));
	}

	public List<Animal> GetSelectedAnimals(int maxSize)
	{
		List<Animal> selectedAnimals = playerStalls.GetAnimalsInSelectedStalls ();
		if(selectedAnimals.Count < maxSize)
		{
			if(donkeyHerd.Selected)
			{
				for(int i = selectedAnimals.Count; i < maxSize; i++)
				{
					selectedAnimals.Add(donkeyHerd.GetAnimal ());
				}
				donkeyHerd.Selected = !donkeyHerd.Selected;
			}
		}

		return selectedAnimals;
	}

	public List<AnimalSpecie> GetAnimalSpeciesInPlayerStalls()
	{
		List<AnimalSpecie> market = playerStalls.GetAnimalSpecieFromAnimalsInStalls ();
		donkeyHerd.Animals.ForEach (animal => market.Add(AnimalSpecie.DONKEY));
		return market;
	}

	public bool AnythingSelectedInPlayerArea()
	{
		if(playerStalls.GetNumberOfSelectedStalls() == 0 && !donkeyHerd.Selected)
		{
			return true;
		}
		return false;
	}
	
}
