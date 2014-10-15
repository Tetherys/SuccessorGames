using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Stall : MonoBehaviour {
	
	public delegate void Select();
	public event Select OnSelect;
	private bool canBeSelected;
	private bool selected;
	private SpriteRenderer spriteRenderer;
	
	// Use this for initialization
	void Start () {
		Selected = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnMouseDown()
	{
		if(canBeSelected)
		{
			Selected = !selected;
			if(spriteRenderer == null)
			{
				spriteRenderer = transform.Find("StallGround").GetComponent<SpriteRenderer>();
			}
			
			spriteRenderer.color = !Selected ? Color.white : Color.black;
			if(OnSelect != null)
			{
				OnSelect();
			}
		}
	}
	
	public bool Selected {
		get {
			return this.selected;
		}
		set {
			if(spriteRenderer != null)
			{
				spriteRenderer.color = Color.white;
			}
			selected = value;	
		}
	}

	public bool CanBeSelected {
		get {
			return this.canBeSelected;
		}
		set {
			canBeSelected = value;
		}
	}
}
