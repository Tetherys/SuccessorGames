using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MarketStall : MonoBehaviour {

	private GameObject animal;
	private bool selected;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {

		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		Selected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter()
	{
		animal = new AnimalFactory ().CreateAnimal (AnimalSpecie.CHICKEN);
		Transform t = animal.transform;
		t.parent = this.transform;
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
	}

	void OnMouseExit()
	{
		spriteRenderer.color = Color.white;
	}

	void OnMouseDown()
	{
		Selected = !selected;
		Debug.Log (gameObject);
	}

	public bool Selected {
		get {
			return this.selected;
		}
		set {
			selected = value;	
		}
	}

	public GameObject Animal {
		get {
			return this.animal;
		}
		set {
			animal = value;
		}
	}

}
