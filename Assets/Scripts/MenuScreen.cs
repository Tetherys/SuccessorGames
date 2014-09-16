using UnityEngine;
using System.Collections;

public abstract class MenuScreen : MonoBehaviour {

	private bool isActive = false;

	public void setActive(bool isActive)
	{
		this.isActive = isActive;
	}
}
