using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class MultiStall : Stall {

	private List<Animal> animals;

	public List<Animal> Animals {
		get {
			if(animals == null)
				animals = new List<Animal>();
			return this.animals;
		}
		set {
			animals = value;
		}
	}

	public void AddAnimal(Animal animal)
	{
		Transform t = animal.transform;
		t.parent = this.transform;
		float x = -50 + (float)this.Animals.Count * 25;
		t.localPosition = new Vector3(x,20,0);
		t.localRotation = Quaternion.identity;

		animal.GetComponent<SpriteRenderer> ().sortingOrder = this.Animals.Count;
		CanBeSelected = true;
		this.Animals.Add(animal);
	}

	public Animal GetAnimal()
	{
		Animal animal = animals.Last ();
		animals.Remove (animal);
		CheckIfStallIsEmpty ();
		return animal;
	}

	public List<Animal> GetMultipleAnimals(int count)
	{
		List<Animal> animals = new List<Animal> ();
		for(int i = 0; i < count; i++)
		{
			animals.Add(GetAnimal());
		}
		return animals;
	}

	private void CheckIfStallIsEmpty()
	{
		if(animals.Count == 0)
		{
			CanBeSelected = false;
		}
	}
}
