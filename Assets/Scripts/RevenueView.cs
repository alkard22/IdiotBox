using UnityEngine;
using UnityEngine.UI;

public class RevenueView : MonoBehaviour
{
    public Text revenue;
    public GameObject trendIcon;

    private int currentRevenue = 0;

    public void UpdateRevenue(int value)
    {
        trendIcon.SetActive(true);
        if(currentRevenue < value) {
            revenue.text = string.Format("{0:C0}", value);
            trendIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 180f);
        } else if (currentRevenue > value) {
            revenue.text = string.Format("{0:C0}", value);
            trendIcon.GetComponent<RectTransform>().rotation = Quaternion.identity;
        } else {
            trendIcon.SetActive(false);
        }

        currentRevenue = value;
    }

}
