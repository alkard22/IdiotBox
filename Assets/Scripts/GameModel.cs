using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Timeslot
{
	Morning,
	Afternoon,
	PrimeTime,
	Night
};

public class GameModel
{
	int turn = 1;

	List<ShowConcept> allConcepts;
	List<ShowConcept> availConcepts;
	Dictionary<ShowConcept, int> developingConcepts = new Dictionary<ShowConcept, int>();

	public List<Ad> allAds;
	public List<Ad> availAds;

	public List<Demographic> population;

	public List<Show> availShows;

	public Dictionary<Timeslot, Show> showProgram;

	public Dictionary<Timeslot, Ad> adProgram;

	public int balance;

	static Demographic kidsDemographic = new Demographic ("Kids", 10000,
		new Dictionary<Timeslot, double>() {
			{ Timeslot.Morning, 0.3 },
			{ Timeslot.Afternoon, 0.5 },
			{ Timeslot.PrimeTime, 0.2 },
			{ Timeslot.Night, 0.1 }
		}
	);

	static Demographic grownupsDemographic = new Demographic("Grownups", 20000, 
		new Dictionary<Timeslot, double>() {
			{Timeslot.Morning, 0.3},
			{Timeslot.Afternoon, 0.2},
			{Timeslot.PrimeTime, 0.6},
			{Timeslot.Night, 0.4}
		}
	);

	static ShowConcept morningShowConcept = new ShowConcept(
		"morning show", 
		"talking heads", 
		100, 
		2, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic,  1},
			{ grownupsDemographic, 5 }
		}
	);
	static Show morningShow = morningShowConcept.toShow(false);

	static ShowConcept cartoonConcept = new ShowConcept(
		"roadrunner", 
		"The cyote must always lose", 
		150, 
		3, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic,  5},
			{ grownupsDemographic, 1 }
		}
	);
	static Show cartoonShow = cartoonConcept.toShow(false);

	static ShowConcept newsConcept = new ShowConcept(
		"news", 
		"Sometimes it's even balanced", 
		250, 
		5, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic,  2},
			{ grownupsDemographic, 5 }
		}
	);
	static Show newsShow = newsConcept.toShow(false);

	static ShowConcept fishtankConcept = new ShowConcept(
		"fishtank", 
		"Think Finding Nemo!", 
		20, 
		1, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic,  2},
			{ grownupsDemographic, 2 }
		}
	);
	static Show fishtankShow = fishtankConcept.toShow(false);

	static ShowConcept blackMirrorConcept = new ShowConcept ("BlackMirror", "Scary neo-noir scifi", 100000, 5, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic, 0 },
			{ grownupsDemographic, 5 }
		}
	);
	static Show blackMirrorShow = blackMirrorConcept.toShow(false);

	Ad nationalTiles = new Ad ("National Tiles", "<silly voice>Frank Walker from national tiles....", 
		new DemographicTarget (grownupsDemographic, 10), 2);

	Ad francoCozzo = new Ad("Franco Cozzo", "Megalo, Megalo, Megalo! Grand Sale, Grand Sale, Grand Sale!", 
		new DemographicTarget(grownupsDemographic, 10), 3);

	Ad transformers = new Ad ("Transformers", "Robots in disguise", 
		new DemographicTarget (kidsDemographic, 10), 3);		

	Ad myLittlePony = new Ad ("My Little Pony \ud83d\udc34", "♩ I love my little pony ♫", 
		new DemographicTarget (kidsDemographic, 10), 3);	

	Ad vegemite = new Ad ("Vegemite", "Aussie kids... are vegemite kids", 
		new DemographicTarget (grownupsDemographic, 8), 4);

	public GameModel (List<Demographic> population = null,
		List<Show> availShows = null,
		List<Ad> availAds = null,
		List<ShowConcept> availConcepts = null,
		Dictionary<ShowConcept, int> developingConcepts = null,
		Dictionary<Timeslot, Show> showProgram = null,
		Dictionary<Timeslot, Ad> adProgram = null)
	{
		this.allConcepts = initConcepts();
		this.allAds = initAds();

		this.population = (population == null) ? population : initPopulation();

		this.availShows = (availShows == null) ? availShows : initAvailShows();
		this.availAds = (availAds == null) ? availAds : initAvailAds();
		this.availConcepts = (availConcepts == null) ? availConcepts : initAvailConcepts();
		this.developingConcepts = (developingConcepts == null) ? developingConcepts : new Dictionary<ShowConcept, int>();

		this.showProgram = (showProgram == null) ? showProgram : initProgram();
		this.adProgram = (adProgram == null) ? adProgram: initAdProgram();

		balance = 10000000;
	}

	List<Demographic> initPopulation()
	{
		return new List<Demographic> { 
			kidsDemographic,
			grownupsDemographic,
		};
	}

	List<ShowConcept> initConcepts()
	{
		return new List<ShowConcept> { 
			blackMirrorConcept
		};
	}


	List<ShowConcept> initAvailConcepts()
	{
		return new List<ShowConcept>(allConcepts);
	}


	Dictionary<Timeslot, Show> initProgram()
	{
		return new Dictionary<Timeslot, Show>() {
			{Timeslot.Morning, morningShow},
			{Timeslot.Afternoon, cartoonShow},
			{Timeslot.PrimeTime, newsShow},
			{Timeslot.Night, fishtankShow},
		};
	}

	List<Show> initAvailShows()
	{
		return new List<Show> { blackMirrorShow };
	}


	Dictionary<Timeslot, Ad> initAdProgram()
	{
		return new Dictionary<Timeslot, Ad>() {
			{Timeslot.Morning, myLittlePony},
			{Timeslot.Afternoon, transformers},
			{Timeslot.PrimeTime, nationalTiles},
			{Timeslot.Night, francoCozzo},
		};
	}


	List<Ad> initAds()
	{
		return new List<Ad>() {vegemite};
	}

	List<Ad> initAvailAds()
	{
		return new List<Ad>(allAds);
	}
		
	public Dictionary<Demographic, int> Viewers(Timeslot slot)
	{
		var viewerData = new Dictionary<Demographic, int>();

		foreach(Demographic demographic in population)
		{
			var show = showProgram [slot];
			viewerData[demographic] = (int) (show.Appeal(demographic) * demographic.timeslotPrefs[slot]);
		}

		return viewerData;
	}

	public Dictionary<Timeslot, int> Revenue()
	{
		var revenueData = new Dictionary<Timeslot, int>();

		foreach (Timeslot slot in Enum.GetValues(typeof(Timeslot))) {
			var ad = adProgram [slot];
			var revenue = 0;

			if (ad != null)
			{
				var viewers = Viewers(slot);
				var otherViewers = viewers
										.Where (pair => pair.Key != ad.primary.target)
										.Aggregate (0, (sum, pair) => sum += pair.Value);
				revenue = ad.primary.revenue * viewers [ad.primary.target] + ad.revenueOther * otherViewers;
			}

			revenueData [slot] = revenue;
		}
		return revenueData;
	}

	public GameModel Clone() {

		var newPop = population.Select(
				demographic =>
					new Demographic(
							demographic.name,
							demographic.size,
							new Dictionary<Timeslot, double>(demographic.timeslotPrefs))).ToList();

		var newAvailShows = availShows.Select (
				show =>
					new Show (
						show.concept,
						show.peak,
						show.longevity)).ToList();

		var newShowProgram = showProgram.Select (
				pair =>
					new KeyValuePair<Timeslot, Show> (
							pair.Key,
							new Show (pair.Value.concept,
									pair.Value.peak,
									pair.Value.longevity))).ToDictionary(pair => pair.Key, pair => pair.Value);

		return new GameModel (newPop, newAvailShows, new List<Ad> (availAds), new List<ShowConcept> (availConcepts),
				new Dictionary<ShowConcept, int> (developingConcepts), newShowProgram, new Dictionary<Timeslot, Ad> (adProgram));
	}

	public void NextTurn() {

		turn++;
		balance += Revenue ().Aggregate (0, (sum, pair) => sum += pair.Value);

	}
}

public class Demographic 
{
	public string name;
	public int size; 
	public Dictionary<Timeslot, double> timeslotPrefs;

	public Demographic(string name, int size, Dictionary<Timeslot, double> timeslotPrefs)
	{
		this.name = name;
		this.size = size;
		this.timeslotPrefs = timeslotPrefs;
	}

}

public class ShowConcept
{
	public string name;
	public string flavor;
	public Dictionary<Demographic, int> demographicAppeal;
	public int price;
	public int duration;


	public ShowConcept(string name, String flavor, int price, int duration, Dictionary<Demographic, int> demographicAppeal)
	{
		this.name = name;
		this.flavor = flavor;
		this.demographicAppeal = demographicAppeal;
		this.price = price;
		this.duration = duration;
	}

	public Show toShow(bool noise = true)
	{
		var random1 = noise ? UnityEngine.Random.Range(-10.0f, 10.0f) : 0.0f;
		var peak = 0.01 * this.price + this.duration + random1;

		var random2 = noise ? UnityEngine.Random.Range(-10.0f, 10.0f) : 0.0f;
		var longevity = 0.01f * this.price + this.duration + random2;

		return new Show(this, (int) peak, (int) longevity);
	}

}

public class Show {

	public ShowConcept concept;
	public float peak;
	public float longevity;

	public Show(ShowConcept concept, float peak, float longevity)
	{
		this.concept = concept;
		this.peak = peak;
		this.longevity = longevity;
	}

	public float Appeal(Demographic demographic) {
		return concept.demographicAppeal[demographic] * peak;
	}

	public string Name
	{
		get { return concept.name; }
	}

}

public class DemographicTarget 
{
	public Demographic target;
	public int revenue;

	public DemographicTarget(Demographic target, int revenue) 	
	{
		this.target = target;
		this.revenue = revenue;
	}
}

public class Ad
{
	string name;
	string flavor;
	public DemographicTarget primary;
	public int revenueOther;

	public Ad(string name, string flavor, DemographicTarget primary, int revenueOther)
	{
		this.primary = primary;
		this.revenueOther = revenueOther;
		this.name = name;
		this.flavor = flavor;
	}
}