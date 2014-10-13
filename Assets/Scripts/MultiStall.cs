using UnityEngine;
using System.Collections.Generic;

public class MultiStall : Stall {

	private List<Animal> animals;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
		
		t.localPosition = new Vector3(x,0,0);
		t.localRotation = Quaternion.identity;

		animal.GetComponent<SpriteRenderer> ().sortingOrder = this.Animals.Count;
		
		this.Animals.Add(animal);
	}
}
