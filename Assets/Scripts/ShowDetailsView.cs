using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowDetailsView : MonoBehaviour
{
    public Text ShowTitle;
    public ShowAvatar avatar;
    public Text price;
    public StarRatingController potentialRating;
    public Text developmentTime;
    public Text showAppeal;

    private bool initialPass = true; // HACK to handle script race condition

    public void UpdateShowDetailsPanel(Show show)
    {
        showAppeal.text = "";

        ShowTitle.text = show.concept.name;
        avatar.title = show.concept.name;
        price.text = string.Format("{0:C0}", show.concept.price);
        potentialRating.SetStarRating(show.concept.ExpectedRating());
        developmentTime.text = show.concept.developmentTime + " week";

        Dictionary<Demographic, int> appeal = show.concept.demographicAppeal;

        foreach(KeyValuePair<Demographic, int> stats in appeal) {
            showAppeal.text += stats.Value + "/5     " + stats.Key.name + "\n";
        }

        if(!initialPass)
            avatar.GenerateAvatar();
        else
            initialPass = false;
    }
}
