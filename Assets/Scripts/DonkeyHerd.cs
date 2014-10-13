using UnityEngine;
using System.Collections.Generic;

public class Herd : MonoBehaviour {

	public MultiStall herd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(List<AnimalSpecie> animals)
	{
		foreach(AnimalSpecie specie in animals)
		{
			if(specie == AnimalSpecie.DONKEY)
			{
				this.AddAnimalToHerd(specie);
			}
		}
	}

	public void AddAnimalToHerd(AnimalSpecie specie)
	{
		Transform t = new AnimalFactory ().CreateAnimal (specie).transform;
		t.parent = herd.transform;
		
		float x = -50 + (float)herd.Animals.Count * 25;
		
		t.localPosition = new Vector3(x,0,0);
		t.localRotation = Quaternion.identity;

		Animal animal = t.gameObject.GetComponent<Animal> ();
		animal.GetComponent<SpriteRenderer> ().sortingOrder = herd.Animals.Count;

		herd.Animals.Add(animal);
	}
	
}
