using UnityEngine;


public class Animal
{
	private AnimalSpecie specie;
	private AnimalType type;

	public Animal ()
	{
	}

	public AnimalSpecie Specie {
		get {
			return this.specie;
		}
		set {
			specie = value;
		}
	}

	public AnimalType Type {
		get {
			return this.type;
		}
		set {
			type = value;
		}
	}
}

