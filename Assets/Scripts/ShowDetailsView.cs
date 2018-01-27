using UnityEngine;
using UnityEngine.UI;

public class ShowDetailsView : MonoBehaviour
{
    public Text ShowTitle;
    public ShowAvatar avatar;
    public Text price;
    public StarRatingController potentialRating;
    public Text productionTime;
    public Text appeal;

    private bool initialPass = true; // HACK to handle script race condition

    public void UpdateShowDetailsPanel(Show show)
    {
        ShowTitle.text = show.concept.name;
        avatar.title = show.concept.name;
        price.text = string.Format("{0:C0}", show.concept.price);
        potentialRating.SetStarRating(4); //TO FINISH
        productionTime.text = "1 week???";
        appeal.text = "what is to go here??";

        if(!initialPass)
            avatar.GenerateAvatar();
        else
            initialPass = false;
    }
}
