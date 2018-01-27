using UnityEngine;

public class EventCreatorExample : MonoBehaviour
{
    public WeeklyEvent weeklyEvent;

    // Use this for initialization
    void Start()
    {
        EventData data = new EventData();
        data.title = "Welcome to the Game!";
        data.message = "Today is the day you become the owner of multi-million dollar TV Studio. Your goal is to grow your viewer audience and dominate the markert.";
        data.optionOne = "Okay";
        data.optionTwo = "Nooooooooooooooooo!";

        weeklyEvent.NewEvent(data);
    }
}
