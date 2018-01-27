using System;
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
    public Material grownups;

    public Image graphImagePrefab;
    public ShowAvatar avatarPrefab;
    public GameObject spacer;

    private int barFixedWidth = 30;

    private Data data = new Data();
    private Dictionary<String, Image> bars = new Dictionary<String, Image>();
    private Dictionary<Timeslot, ShowAvatar> avatars = new Dictionary<Timeslot, ShowAvatar>();

    void Awake()
    {
        Dictionary<String, Material> colors = new Dictionary<String, Material>();
        colors.Add("Kids", kids);
        colors.Add("Grownups", grownups);

        foreach (Timeslot t in Enum.GetValues(typeof(Timeslot)))
        {
            GameObject horizontal = buildHorizontalLayoutGroup(t.ToString() + "Group");
            ShowAvatar avatar = Instantiate(avatarPrefab);
            avatar.GetComponent<RectTransform>().SetParent(horizontal.transform);
            avatars.Add(t, avatar);

            GameObject space = Instantiate(spacer);
            space.GetComponent<RectTransform>().SetParent(horizontal.transform);

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
            Dictionary<Demographic, int> viewers = GameState.current.Viewers(t);
            avatars[t].title = GameState.current.showProgram[t].concept.name;
            avatars[t].GenerateAvatar();
            foreach (Demographic d in viewers.Keys)
            {
                String key = t.ToString() + "-" + d.name;
                RectTransform rect = bars[key].GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(viewers[d], barFixedWidth);
                totalAudienceOffset += viewers[d];
            }
        }
    }

    void Update()
    {
        Redraw();
    }
}
