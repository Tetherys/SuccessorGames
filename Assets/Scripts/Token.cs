using UnityEngine;
using System.Collections;

public class Token
{
	private TokenType type;
	private int value;

	public TokenType Type {
		get {
			return this.type;
		}
		set {
			type = value;
		}
	}

	public int Value {
		get {
			return this.value;
		}
		set {
			this.value = value;
		}
	}
}

