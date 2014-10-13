using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Linq;

[DataContract]
public class Castle : MonoBehaviour{
	
	private const int TOKEN_STACKS_TO_BE_DEPLETED = 3;

	private Dictionary<TokenType, int[]> tokenStack;
	private int depletedTokenStacks;

	public List<Token> TradeAnimalsForTokens(List<Animal> animals)
	{
		List<Token> tokens = new List<Token>();
		foreach(Animal animal in animals)
		{
			tokens.Add(CreateToken(AnimalSpecieToTokenType(animal.specie)));
			Destroy(animal.gameObject);
		}
		return tokens;
	}

	private Token CreateToken(TokenType type)
	{
		Token token = new Token ();
		token.Type = type;
		int [] values = tokenStack [type];
		token.Value = values.First();
		tokenStack [type] = values.Where ((val,index) => index != 0).ToArray();
		Debug.Log ("token type:" + type);
		Debug.Log ("token value:" + token.Value);
		return token;
	}

	private TokenType AnimalSpecieToTokenType(AnimalSpecie specie)
	{
		switch(specie)
		{
		case AnimalSpecie.CHICKEN:
			return TokenType.CHICKEN;
			break;
		case AnimalSpecie.GOAT:
			return TokenType.GOAT;
			break;
		case AnimalSpecie.COW:
			return TokenType.COW;
			break;
		case AnimalSpecie.LAMA:
			return TokenType.LAMA;
			break;
		case AnimalSpecie.HORSE:
			return TokenType.HORSE;
			break;
		case AnimalSpecie.PIG:
				return TokenType.PIG;
			break;
		}
		return TokenType.BONUS_3;
	}

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
