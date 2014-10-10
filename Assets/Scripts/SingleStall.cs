using UnityEngine;
using System.Collections;


public class SingleStall : Stall {

	private Animal animal;

	public Animal Animal {
		get {
			return this.animal;
		}
		set {
			animal = value;
		}
	}

}
