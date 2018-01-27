using UnityEngine;
using UnityEngine.UI;

public struct EventData
{
    public string title;
    public string message;
    public string optionOne;
    public string optionTwo;
}

public class WeeklyEvent : MonoBehaviour
{
    public Text titleBox;
    public Text messageBox;
    public Text optionOneText;
    public GameObject optionOneButton;
    public Text optionTwoText;
    public GameObject optionTwoButton;

    //TODO: handle a delegate to change some effect depending on user option selected

    public void NewEvent(EventData data)
    {
        this.gameObject.SetActive(true);
        bool secondOption = false;

        titleBox.text = data.title;
        messageBox.text = data.message;
        optionOneText.text = data.optionOne;

        if(data.optionOne != null) {
            optionTwoText.text = data.optionTwo;
            secondOption = true;
        }

        optionTwoButton.SetActive(secondOption);
    }
}
