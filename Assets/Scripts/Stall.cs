using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Stall : MonoBehaviour {

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
}
