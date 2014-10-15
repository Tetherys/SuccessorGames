using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StallManager : MonoBehaviour {

	public List<SingleStall> stalls;

	public List<SingleStall> GetEmptyStalls()
	{
		return stalls.Where (item => item.Animal == null).ToList();
	}

	public int GetNumberOfEmptyStalls()
	{
		return GetEmptyStalls ().Count;	
	}

	public SingleStall GetFirstEmptyStall ()
	{
		return this.GetEmptyStalls ().First ();
	}

	public List<SingleStall> GetSelectedStalls()
	{
		return stalls.Where (item => item.Selected).ToList();
	}

	public List<Animal> GetAnimalsInSelectedStalls()
	{
		List<Animal> animals = (from item in GetSelectedStalls () 
								select item.Animal).ToList();
		GetSelectedStalls ().ForEach (item => item.Animal = null);
		return animals;
	}

	public List<AnimalSpecie> GetAnimalSpecieFromAnimalsInStalls()
	{
		return (from stall in stalls 
		        where stall.Animal != null
		        select stall.Animal.specie).ToList ();
	}

	public int GetNumberOfSelectedStalls()
	{
		return GetSelectedStalls ().Count;
	}

	public List<Animal> GetAnimalsInStallsBySpecie(AnimalSpecie specie)
	{
		List<Animal> animals = (from item in GetStallsBySpecie (specie) select item.Animal).ToList ();
		GetStallsBySpecie (specie).ForEach (item => item.Animal = null);
		return animals;
	}

	public int GetNumberOfSelectedStallBySpecie(AnimalSpecie specie)
	{
		return GetStallsBySpecie (specie).Where(item => item.Selected).ToList().Count;
	}

	private List<SingleStall> GetStallsBySpecie(AnimalSpecie specie)
	{
		return (from stall in stalls 
		        where stall.Animal.specie == specie
		        select stall).ToList();
	}

	public bool CheckIfSelectedAnimalsAreSameSpecie()
	{
		return (from stall in GetSelectedStalls ()
		        select stall.Animal.specie).Distinct ().Count() == 1;
	}

	public bool CheckIfSelectedAnimalsHaveMinimumNumber(AnimalType type, int min)
	{
		return (from stall in GetSelectedStalls ()
		        where stall.Animal.type == type
		        select stall).ToList ().Count () >= min;
	}

	public void AddAnimalToStalls(Animal animal)
	{
		this.GetFirstEmptyStall ().Animal = animal;
	}

	public void AddAnimalsToStalls(List<Animal> animals)
	{
		animals.ForEach (animal => this.AddAnimalToStalls(animal));
	}

	public List<SingleStall> Stalls {
		get {
			return this.stalls;
		}
		set {
			stalls = value;
		}
	}
}
