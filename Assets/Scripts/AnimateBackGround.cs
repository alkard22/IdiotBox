using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateBackGround : MonoBehaviour
{
    public List<Sprite> backgroundAnimations;
    private int animationFrame = 0;

    void Start()
    {
        InvokeRepeating("ChangeBackGroundImage", 0f, 0.8f);  //1s delay, repeat every 1s
    }

    private void ChangeBackGroundImage()
    {
        if(animationFrame >= backgroundAnimations.Count) {
            animationFrame = 0;
        }
        this.GetComponent<Image>().sprite = backgroundAnimations[animationFrame++];
    }

}
