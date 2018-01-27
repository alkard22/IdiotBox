using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class GameModel
	{
		int turn = 1;

		List<ShowConcept> allConcepts = initConcepts();
		List<ShowConcept> availConcepts = initAvailConcepts();
		Dictionary<ShowConcept, int> developingConcepts = new Dictionary<ShowConcept, int>();

		List<Ad> allAds = initAds();
		List<Ad> availAds = initAvailAds();

		List<Demographic> population = initPopulation();


		List<Timeslot> slots = initSlots();

		List<Show> availShows = initAvailShows();

		Dictionary<Timeslot, Show> showProgram = initProgram();

		Dictionary<Timeslot, Ad> adProgram = initAdProgram();

		public GameModel ()
		{

		}

		List<Timeslot> initSlots()
		{
			null;
		}

		List<Demographic> initPopulation()
		{
			null;
		}

		List<ShowConcept> initConcepts()
		{
			null;
		}


		List<ShowConcept> initAvailConcepts()
		{
			null;
		}


		Dictionary<Timeslot, Show> initProgram()
		{
			null;
		}

		List<Show> initAvailShows()
		{
			null;
		}


		Dictionary<Timeslot, Ad> initAdProgram()
		{
			null;
		}


		List<Ad> initAds()
		{
			null;
		}

		List<Ad> initAvailAds()
		{
			null;
		}



	}


	public class Timeslot 
	{
		public string name;

		public Timeslot(string name)
		{
			this.name = name;	
		}
			
	}

	public class Demographic 
	{
		public string name;
		public int size; 
		public Dictionary<Timeslot, float> timeslotPrefs;

		public Demographic(string name, int size, Dictionary<Timeslot, float> timeslotPrefs)
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


		public ShowConcept(string name, String flavor, Dictionary<Demographic, int> demographicAppeal, int price, int duration)
		{
			this.name = name;
			this.flavor = flavor;
			this.demographicAppeal = demographicAppeal;
			this.price = price;
			this.duration = duration;
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

	}

	public class DemographicTarget 
	{
		Demographic target;
		int revenue;

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
		public DemographicTarget other;

		public Ad(DemographicTarget primary, int revenueOther)
		{
			this.primary = primary;
			this.other = other;
		}
	}


}

