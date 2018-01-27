using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RevenueView : MonoBehaviour
{
    public Text revenue;
    public GameObject trendIcon;

    public Color up;
    public Color down;

    private int currentRevenue = 0;

    public void UpdateRevenue(int value)
    {
        trendIcon.SetActive(true);
        if(currentRevenue < value) {
            revenue.text = string.Format("{0:C0}", value);
            trendIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 180f);
            trendIcon.GetComponent<Image>().color = down;
        } else if (currentRevenue > value) {
            revenue.text = string.Format("{0:C0}", value);
            trendIcon.GetComponent<RectTransform>().rotation = Quaternion.identity;
            trendIcon.GetComponent<Image>().color = up;
        } else {
            trendIcon.SetActive(false);
        }

        currentRevenue = value;
    }

    void Update()
    {
        Dictionary<Timeslot, int> revenueData = GameState.current.Revenue();
        int sum = 0;
        foreach (int r in revenueData.Values)
        {
            sum += r;
        }
        if (sum != currentRevenue)
        {
            UpdateRevenue(sum);
        }
    }

}
