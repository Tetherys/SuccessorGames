using UnityEngine;
using System.Collections.Generic;

public class PlayerArea : Area {

	public SingleStall[] playerStalls;

	public void Initialize(List<AnimalSpecie> animals)
	{
		Debug.Log ("player area initializes");
		foreach(AnimalSpecie specie in animals)
		{
			if(specie == AnimalSpecie.DONKEY)
			{
				this.AddDonkeyToHerd(specie);
			}
			else
			{
				this.AddAnimalToStalls(specie);
			}
		}
	}

	private void AddAnimalToStalls(AnimalSpecie specie)
	{
		SingleStall stall = this.GetEmptyStall ();

		if(stall != null)
		{
			Transform t = new AnimalFactory ().CreateAnimal (specie).transform;
			t.parent = stall.transform;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;

			stall.Animal = t.gameObject.GetComponent<Animal>();
		}
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
