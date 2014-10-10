using UnityEngine;
using System.Collections.Generic;

public class MultiStall : MonoBehaviour {

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
}
