﻿
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public enum Timeslot
{
	Morning,
	Afternoon,
	PrimeTime,
	Night
};

public class GameModel
{
	public int turn;

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

	static Demographic kidsDemographic = new Demographic ("Kids", 20000,
		new Dictionary<Timeslot, double>() {
			{ Timeslot.Morning, 0.3 },
			{ Timeslot.Afternoon, 0.5 },
			{ Timeslot.PrimeTime, 0.2 },
			{ Timeslot.Night, 0.1 }
		}
	);

	static Demographic armchairAthleteDemographic = new Demographic ("Armchair Athletes", 30000,
		new Dictionary<Timeslot, double> () {
			{ Timeslot.Morning, 0.15 },
			{ Timeslot.Afternoon, 0.3 },
			{ Timeslot.PrimeTime, 0.5 },
			{ Timeslot.Night, 0.4 }
		}
	);

	static Demographic hopelessRomanticDemographic = new Demographic ("Hopeless Romantics", 20000,
		new Dictionary<Timeslot, double> () {
			{ Timeslot.Morning, 0.15 },
			{ Timeslot.Afternoon, 0.2 },
			{ Timeslot.PrimeTime, 0.6 },
			{ Timeslot.Night, 0.3 }
		}
	);

	static Demographic conspiracyTheoristsDemographic = new Demographic("Conspiracy Theorists", 50000, 
		new Dictionary<Timeslot, double>() {
			{Timeslot.Morning, 0.2},
			{Timeslot.Afternoon, 0.4},
			{Timeslot.PrimeTime, 0.4},
			{Timeslot.Night, 0.7}
		}
	);

	//Show Costs
	//V Cheap      500
	//Cheap      1000
	//Medium     2000
	//Expensive  4000

	//Show time to develop
	//Short        3
	//Medium       6
	//Long         12

	static ShowConcept morningShowConcept = new ShowConcept(
		"morning show", 
		"talking heads", 
		1000, 
		6, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic,  1},
			{ hopelessRomanticDemographic, 5 },
			{ armchairAthleteDemographic, 2 },
			{ conspiracyTheoristsDemographic, 2 }
		}
	);
	static Show morningShow = morningShowConcept.toShow(false);

	static ShowConcept cartoonConcept = new ShowConcept(
		"roadrunner", 
		"The cyote must always lose", 
		2000, 
		6, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic,  5},
			{ armchairAthleteDemographic, 1 },
			{ hopelessRomanticDemographic, 1 },
			{ conspiracyTheoristsDemographic, 0 }
		}
	);
	static Show cartoonShow = cartoonConcept.toShow(false);


	static ShowConcept newsConcept = new ShowConcept(
		"news", 
		"Sometimes it's even balanced", 
		2500, 
		12, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic,  2},
			{ armchairAthleteDemographic, 3 },
			{ hopelessRomanticDemographic, 3 },
			{ conspiracyTheoristsDemographic, 3 }
		}
	);
	static Show newsShow = newsConcept.toShow(false);

	static ShowConcept fishtankConcept = new ShowConcept(
		"fishtank", 
		"Think Finding Nemo!", 
		500, 
		3, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic,  2},
			{ armchairAthleteDemographic,  1},
			{ hopelessRomanticDemographic,  2},
			{ conspiracyTheoristsDemographic, 3 }
		}
	);
	static Show fishtankShow = fishtankConcept.toShow(false);

	static ShowConcept blackMirrorConcept = new ShowConcept ("BlackMirror", "Scary neo-noir scifi", 4000, 12, 
		new Dictionary<Demographic, int> {
			{ kidsDemographic, 0 },
			{ armchairAthleteDemographic,  1},
			{ hopelessRomanticDemographic,  2},
			{ conspiracyTheoristsDemographic, 5 }
		}
	);
	static Show blackMirrorShow = blackMirrorConcept.toShow(false);

    static ShowConcept dieHeartConcept = new ShowConcept("Die Heart", "Romanitc reality show about finding love", 1000, 6,
    new Dictionary<Demographic, int> {
            { kidsDemographic, 1 },
			{ armchairAthleteDemographic,  1},
			{ hopelessRomanticDemographic,  2},
            { conspiracyTheoristsDemographic, 3 }
    }
);
    static Show dieHeartShow = dieHeartConcept.toShow(false);

    static ShowConcept cowTippingUSAConcept = new ShowConcept("Cow Tipping USA", "Cowboys getting rough with cows", 1000, 4,
new Dictionary<Demographic, int> {
            { kidsDemographic, 2 },
			{ armchairAthleteDemographic,  1},
			{ hopelessRomanticDemographic,  2},
            { conspiracyTheoristsDemographic, 3 }
}
);
    static Show cowTippingUSAShow = cowTippingUSAConcept.toShow(false);

    static ShowConcept FrankDeepConcept = new ShowConcept("Frank and the Terrors of the Deep", "Kid friendly show...promise!", 500, 5,
new Dictionary<Demographic, int> {
            { kidsDemographic, 5 },
			{ armchairAthleteDemographic,  1},
			{ hopelessRomanticDemographic,  2},
            { conspiracyTheoristsDemographic, 2 }
}
);
    static Show FrankDeepShow = FrankDeepConcept.toShow(false);

    Ad nationalTiles = new Ad ("National Tiles", "<silly voice>Frank Walker from national tiles....", 
		new DemographicTarget (hopelessRomanticDemographic, 40), 4, 4);

	Ad francoCozzo = new Ad("Franco Cozzo", "Megalo, Megalo, Megalo! Grand Sale, Grand Sale, Grand Sale!", 
		new DemographicTarget(armchairAthleteDemographic, 30), 6, 4);

	Ad transformers = new Ad ("Transformers", "Robots in disguise", 
		new DemographicTarget (kidsDemographic, 20), 6, 6);		

	Ad myLittlePony = new Ad ("My Little Pony", "I love my little pony", 
		new DemographicTarget (kidsDemographic, 20), 8, 8);	

	Ad vegemite = new Ad ("Vegemite", "Aussie kids... are vegemite kids", 
		new DemographicTarget (conspiracyTheoristsDemographic, 16), 10, 10);

	public GameModel (List<Demographic> population = null,
		List<Show> availShows = null,
		List<Ad> availAds = null,
		List<ShowConcept> availConcepts = null,
		Dictionary<ShowConcept, int> developingConcepts = null,
		Dictionary<Timeslot, Show> showProgram = null,
		Dictionary<Timeslot, Ad> adProgram = null,
		int turn = 1,
		int balance = 10000000)
	{
		this.population = (population != null) ? population : initPopulation();
		this.turn = turn;

		this.allConcepts = initConcepts();
		this.allAds = initAds();

		this.availShows = (availShows != null) ? availShows : initAvailShows();
		this.availAds = (availAds != null) ? availAds : initAvailAds();
		this.availConcepts = (availConcepts != null) ? availConcepts : initAvailConcepts();
		this.developingConcepts = (developingConcepts != null) ? developingConcepts : new Dictionary<ShowConcept, int>();

		this.showProgram = (showProgram != null) ? showProgram : initProgram();
		this.adProgram = (adProgram != null) ? adProgram: initAdProgram();

		this.balance = balance;
	}

    public void scheduleShow(Timeslot timeslot, Show show)
    {
        showProgram[timeslot] = show;
    }

    public void scheduleAd(Timeslot timeslot, Ad ad)
    {
        adProgram[timeslot] = ad;
    }

    public void developConcept(ShowConcept concept)
    {
        developingConcepts.Add(concept, concept.developmentTime);
    }

    Dictionary<Demographic, int> ParseDemographicAppeal(string input)
	{
		var inputPhrases = input.Split (',');
		var output = new Dictionary<Demographic, int> ();

		foreach(var inputPhrase in inputPhrases) {
			var lexemes = inputPhrase.Split (':');
			var demo = population.Find (d => d.name == lexemes [0]);

			output [demo] = int.Parse(lexemes[1]);
		}

		return output;
	}

	ShowConcept ParseShowConcept(string[] fields)
	{
		return new ShowConcept(fields[0], fields[1],int.Parse(fields[2]), int.Parse(fields[3]), ParseDemographicAppeal(fields[4]));
	}

	public List<ShowConcept> LoadAllShowConceptsFromFile(string filename)
	{
		var showConcepts = new List<ShowConcept> ();
        TextAsset asset = Resources.Load(filename) as TextAsset;
        Stream s = new MemoryStream(asset.bytes);
        var reader = new StreamReader (s);
		string line;

		while ((line = reader.ReadLine ()) != null) {
			var fields = line.Split ('|');
			showConcepts.Add(ParseShowConcept(fields));
		}

		return showConcepts;
	}

	public List<Show> LoadShowsFromFile(string filename)
	{
		var shows = new List<Show> ();
        TextAsset asset = Resources.Load(filename) as TextAsset;
        Stream s = new MemoryStream(asset.bytes);
        var reader = new StreamReader (s);
		string line;

		while ((line = reader.ReadLine ()) != null) {
			var fields = line.Split ('|');
			var concept = ParseShowConcept (fields);
			var show = new Show (concept, float.Parse (fields [5]), float.Parse (fields [6]));

			shows.Add (show);	
		}

		return shows;
	}

	List<Demographic> initPopulation()
	{
		return new List<Demographic> { 
			kidsDemographic,
			armchairAthleteDemographic,
			hopelessRomanticDemographic,
			conspiracyTheoristsDemographic,
		};
	}

	List<ShowConcept> initConcepts()
	{
		return LoadAllShowConceptsFromFile ("all-show-concepts");
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
		return LoadShowsFromFile ("avail-shows");
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
		return new List<Ad>() { nationalTiles, transformers, myLittlePony, francoCozzo, vegemite };
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
									pair.Value.longevity,
					                pair.Value.weeksShowing
									))
			).ToDictionary(pair => pair.Key, pair => pair.Value);

		return new GameModel (new List<Demographic>(population), newAvailShows, new List<Ad> (availAds), new List<ShowConcept> (availConcepts),
			new Dictionary<ShowConcept, int> (developingConcepts), newShowProgram, new Dictionary<Timeslot, Ad> (adProgram), turn, balance);
	}

	public void NextTurn() {
		turn++;
		balance += Revenue ().Aggregate (0, (sum, pair) => sum += pair.Value);
		foreach(var item in showProgram) {
			item.Value.weeksShowing++;
		}

		var newDevelopingConcepts = new Dictionary<ShowConcept, int> ();

		foreach (var pair in developingConcepts) {
			if (pair.Value <= 1)
				availShows.Add (pair.Key.toShow());
			else
				newDevelopingConcepts [pair.Key] = pair.Value - 1;	
		}

		var newAds = new Dictionary<Timeslot, Ad> (adProgram);

		foreach (var pair in adProgram) {
			if (pair.Value != null && pair.Value.weeksRemaining > 1) {
				pair.Value.weeksRemaining = pair.Value.weeksRemaining - 1;
				newAds [pair.Key] = pair.Value;
			}
			else
				newAds [pair.Key] = null;
					
		}

		adProgram = newAds;
		developingConcepts = newDevelopingConcepts;
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
	public int developmentTime;


	public ShowConcept(string name, String flavor, int price, int developmentTime, Dictionary<Demographic, int> demographicAppeal)
	{
		this.name = name;
		this.flavor = flavor;
		this.demographicAppeal = demographicAppeal;
		this.price = price;
		this.developmentTime = developmentTime;
	}

	//peak varies between 8 - 52 (+/-7)
	//longevity varies between 4 - 18 (+/-3)

	public Show toShow(bool noise = true)
	{
		var random1 = noise ? UnityEngine.Random.Range(-7.0f, 7.0f) : 0.0f;
		var peak = 5.0 + 0.01 * this.price + this.developmentTime + random1;

		var random2 = noise ? UnityEngine.Random.Range(-3.0f, 3.0f) : 0.0f;
		var longevity =  1.0 + 0.003f * this.price + 0.6f * this.developmentTime + random2;

		return new Show(this, (int) peak, (int) longevity);
	}

	public int ExpectedRating()
	{
		var n = 0.001 * this.price + this.developmentTime * 0.2;
		if (n <= 1.0)
			return 1;
		else if (n <= 2.0)
			return 2;
		else if (n <= 3.0)
			return 3;
		else if (n <= 4.0)
			return 4;
		else 
			return 5;
	}

}

public class Show {
	static double PEAK_AUDIENCE_TURNS = 3;
	static double PEAK_DECAY_TURNS = 5;

	public ShowConcept concept;
	public float peak;
	public float longevity;
	public int weeksShowing = 0;

	public Show(ShowConcept concept, float peak, float longevity, int weeksShowing = 0)
	{
		this.concept = concept;
		this.peak = peak;
		this.longevity = longevity;
		this.weeksShowing = weeksShowing;
	}

	public float Appeal(Demographic demographic) 
	{
		//Audience appeal rises from 0.4 to 1.0 of `peak` over PEAK_AUDIENCE_TURNS turns.
		//Then sites at peak until PEAK_DECAY_TURNS 
		//After the PEAK_DECAY_TURNS peak, it decays exponentially with a half-life of `longevity`
		var fractionToPeak = weeksShowing/PEAK_AUDIENCE_TURNS;
		var afterPeak = weeksShowing - PEAK_DECAY_TURNS;
		var scale = (weeksShowing <= PEAK_AUDIENCE_TURNS) ?
			0.4 * (1.0 - fractionToPeak) + fractionToPeak: 
			(weeksShowing > PEAK_DECAY_TURNS) ?
				1.0 / Math.Pow(2.0, afterPeak/longevity): 
				1.0;


		try {
			return (float)(concept.demographicAppeal[demographic] * peak * scale);
		} catch(Exception e) {
			Debug.Log (demographic.name);
			Debug.Log (concept.demographicAppeal.Count);
			foreach (var a in concept.demographicAppeal) {
				Debug.Log (a.Key.name);
			}
			throw e;
		}
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
	public string name;
	public string flavor;
	public DemographicTarget primary;
	public int revenueOther;
    public int weeksRemaining;

	public Ad(string name, string flavor, DemographicTarget primary, int revenueOther, int duration)
	{
		this.primary = primary;
		this.revenueOther = revenueOther;
		this.name = name;
		this.flavor = flavor;
        this.weeksRemaining = duration;
	}
}