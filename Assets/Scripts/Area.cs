using UnityEngine;
using System.Collections.Generic;

public class Area : MonoBehaviour {

	public MultiStall donkeyHerd;

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
				this.AddDonkeyToHerd(specie);
			}
		}
	}

	protected void AddDonkeyToHerd(AnimalSpecie specie)
	{
		Transform t = new AnimalFactory ().CreateAnimal (specie).transform;
		t.parent = donkeyHerd.transform;
		
		float x = -0.3f + (float)donkeyHerd.Animals.Count / 5;
		
		t.localPosition = new Vector3(x,0,0);
		t.localRotation = Quaternion.identity;
		
		donkeyHerd.Animals.Add(t.gameObject.GetComponent<Animal>());
	}
	
}
