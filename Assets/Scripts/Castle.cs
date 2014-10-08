using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;

[DataContract]
public class Castle : MonoBehaviour{
	
	private const int TOKEN_STACKS_TO_BE_DEPLETED = 3;

	private Dictionary<TokenType, int[]> tokenStack;
	private int depletedTokenStacks;
	[DataMember]
	public Dictionary<TokenType, int[]> TokenStack {
		get {
			return this.tokenStack;
		}
		set {
			tokenStack = value;
		}
	}
	[DataMember]
	public int DepletedTokenStacks {
		get {
			return this.depletedTokenStacks;
		}
		set {
			depletedTokenStacks = value;
		}
	}
}
