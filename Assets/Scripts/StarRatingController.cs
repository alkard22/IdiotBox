using UnityEngine;
using UnityEngine.UI;

public class StarRatingController : MonoBehaviour
{
    public Sprite fullStar;
    public Sprite emptyStar;

    public Image[] stars;

    public void SetStarRating(int rating)
    {
        for(int i = 0; i < stars.Length; i++) {
            if(i < rating) {
                stars[i].sprite = fullStar;
            } else {
                stars[i].sprite = emptyStar;
            }
        }
    }
}
