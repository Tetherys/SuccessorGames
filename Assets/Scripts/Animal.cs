using UnityEngine;


public class Animal : MonoBehaviour
{
	private AnimalSpecie specie;
	private AnimalType type;

	void OnStart() 
	{

	}

	void OnUpdate()
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

