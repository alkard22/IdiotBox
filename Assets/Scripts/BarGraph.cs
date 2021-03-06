﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarGraph : MonoBehaviour {

    public class Data : Dictionary<Timeslot, Dictionary<Demographic, int>>
    {
        public void AddData(Timeslot t, Demographic d, int viewership)
        {
            Dictionary<Demographic, int> value;
            if (!ContainsKey(t))
            {
                Add(t, new Dictionary<Demographic, int>());
            }

            if (!this.TryGetValue(t, out value))
            {
                value.Add(d, viewership);
            }
            else
            {
                this[t][d] = viewership;
            }
        }
    }



    public Material kids;
    public Material armchairAthletes;
	public Material hopelessRomantics;
	public Material conspiracyTheorists;

    public Image graphImagePrefab;
    public ShowAvatar avatarPrefab;
    public ShowAvatar adAvatarPrefab;
    public Text timeslotPrefab;
    public GameObject spacer;

    private int barFixedWidth = 30;
    private int barUnitSize = 2;

    private Data data = new Data();
    private Dictionary<String, Image> bars = new Dictionary<String, Image>();
    private Dictionary<Timeslot, ShowAvatar> avatars = new Dictionary<Timeslot, ShowAvatar>();
    private Dictionary<Timeslot, ShowAvatar> adAvatars = new Dictionary<Timeslot, ShowAvatar>();
    private Dictionary<Timeslot, Text> revenueTags = new Dictionary<Timeslot, Text>();
    
    void Awake()
    {
        Dictionary<String, Material> colors = new Dictionary<String, Material>();
        colors.Add("Kids", kids);
		colors.Add("Armchair Athletes", armchairAthletes);
		colors.Add("Hopeless Romantics", hopelessRomantics);
        colors.Add("Conspiracy Theorists", conspiracyTheorists);

        foreach (Timeslot t in Enum.GetValues(typeof(Timeslot)))
        {
            Text label = Instantiate(timeslotPrefab);
            label.text = t.ToString();
            label.GetComponent<RectTransform>().SetParent(this.transform);

            GameObject horizontal = buildHorizontalLayoutGroup(t.ToString() + "Group");

            Instantiate(spacer).GetComponent<RectTransform>().SetParent(horizontal.transform);

            ShowAvatar avatar = Instantiate(avatarPrefab);
            avatar.GetComponent<RectTransform>().SetParent(horizontal.transform);
            avatars.Add(t, avatar);

            Instantiate(spacer).GetComponent<RectTransform>().SetParent(horizontal.transform);

            foreach (Demographic d in GameState.current.population)
            {
                data.AddData(t, d, 0);

                String key = t.ToString() + "-" + d.name;
                Image bar = Instantiate(graphImagePrefab);
                bar.material = colors[d.name];
                bar.name = d.name;
                
                bars.Add(key, bar);
                bar.GetComponent<RectTransform>().sizeDelta = new Vector2(data[t][d], barFixedWidth);
                bar.GetComponent<RectTransform>().SetParent(horizontal.transform);
            }

            Instantiate(spacer).GetComponent<RectTransform>().SetParent(horizontal.transform);

            ShowAvatar adAvatar = Instantiate(adAvatarPrefab);
            adAvatar.GetComponent<RectTransform>().SetParent(horizontal.transform);
            adAvatars.Add(t, adAvatar);

            Text revenue = Instantiate(timeslotPrefab);
            revenue.GetComponent<RectTransform>().SetParent(horizontal.transform);
            revenueTags.Add(t, revenue);
        }
    }

    GameObject buildHorizontalLayoutGroup(String name)
    {
        GameObject horizontal = new GameObject();
        HorizontalLayoutGroup hlg = horizontal.AddComponent<HorizontalLayoutGroup>();
        hlg.name = name;
        horizontal.GetComponent<RectTransform>().SetParent(this.transform);
        hlg.childControlHeight = false;
        hlg.childControlWidth = false;
        hlg.childForceExpandHeight = false;
        hlg.childForceExpandWidth = false;
        horizontal.GetComponent<RectTransform>().sizeDelta = new Vector2(200, barFixedWidth);
        return horizontal;
    }

    void Redraw()
    {
        Array timeslots = Enum.GetValues(typeof(Timeslot));
        Array.Reverse(timeslots);
        foreach (Timeslot t in timeslots)
        {
            int totalAudienceOffset = 0;
            
			if (GameState.current.adProgram [t] != null) {
				adAvatars [t].title = GameState.current.adProgram [t].name;
			} else
				adAvatars [t].title = "";
					
           	adAvatars[t].GenerateAvatar();


            Dictionary<Demographic, int> viewers = GameState.current.Viewers(t);
            avatars[t].title = GameState.current.showProgram[t].concept.name;
            avatars[t].GenerateAvatar();
            foreach (Demographic d in viewers.Keys)
            {
                String key = t.ToString() + "-" + d.name;
                RectTransform rect = bars[key].GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(viewers[d] * barUnitSize, barFixedWidth);
                totalAudienceOffset += viewers[d];
            }

            revenueTags[t].text = "$" + GameState.current.Revenue()[t]/100;
        }
    }

    void Update()
    {
        Redraw();
    }
}
