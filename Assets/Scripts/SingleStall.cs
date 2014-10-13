﻿using UnityEngine;
using System.Collections;


public class SingleStall : Stall {

	private Animal animal;

	public Animal Animal {
		get {
			return this.animal;
		}
		set {
			animal = value;
			if(animal != null)
			{
				Transform t = animal.transform;
				t.parent = this.transform;
				t.localPosition = Vector3.zero;
				t.localRotation = Quaternion.identity;
			}
		}
	}
}