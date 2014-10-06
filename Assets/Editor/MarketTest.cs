using System.Collections.Generic;
using NUnit.Framework;

public class MarketTest 
{
	[Test]
	public void InitializeTest ()
	{
		Market market = new Market ();
		market.MarketStack = CreateTestMarketStack ();

		Assert.That (market.Animals == null);
		Assert.That (market.MarketStalls == null);

		market.Initialize ();

		Assert.AreEqual (market.Animals.Count, 50);
		Assert.That (market.MarketStalls.Length == 5);
	}

	[Test]
	public void GiveSingleAnimalTest()
	{
		Market market = CreateTestMarket ();

		Assert.AreEqual (market.Animals.Count, 50);

		market.GiveSingleAnimal ();

		Assert.AreEqual (market.Animals.Count, 49);

	}

	private Market CreateTestMarket()
	{
		Market market = new Market ();
		market.MarketStack = CreateTestMarketStack ();
		market.Initialize ();
		return market;
	}

	private Dictionary<AnimalSpecie, int> CreateTestMarketStack()
	{
		Dictionary<AnimalSpecie, int> marketStack = new Dictionary<AnimalSpecie, int> ();
		marketStack.Add (AnimalSpecie.DONKEY, 11);
		marketStack.Add (AnimalSpecie.COW, 6);
		marketStack.Add (AnimalSpecie.LAMA, 6);
		marketStack.Add (AnimalSpecie.HORSE, 6);
		marketStack.Add (AnimalSpecie.CHICKEN, 10);
		marketStack.Add (AnimalSpecie.GOAT, 8);
		marketStack.Add (AnimalSpecie.PIG, 8);
		return marketStack;
	}
}
