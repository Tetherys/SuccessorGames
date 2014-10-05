using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Castle {

	[JsonIgnore]
	private const int TOKEN_STACKS_TO_BE_DEPLETED = 3;

	private Dictionary<TokenType, int[]> tokenStack;

	public Dictionary<TokenType, int[]> TokenStack {
		get {
			return this.tokenStack;
		}
		set {
			tokenStack = value;
		}
	}
}
