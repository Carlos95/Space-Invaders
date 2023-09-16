using Dan.Main;
using Dan.Models;
using UnityEngine;
using UnityEngine.Events;

public enum InternetStatus
{
    NoAnswer,
    On,
    Off
}

public class CheckInternetConnection : MonoBehaviour
{
    private string publicLeaderboardKey = "4da4217571108f6e4f81b99faeae1fb7c64162fa2c9cb2d13e527db217fe61a1";
    public LeaderboardSearchQuery query = new LeaderboardSearchQuery();

    public UnityEvent<bool> isInternetAvailableEvent;

    public void TryLbConnection()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, false, (msg) =>
        {
            if (msg != null && msg.Length > 0)
            {
                IsInternetAvailable(InternetStatus.On);
            } else
            {
                IsInternetAvailable(InternetStatus.Off);
            } 
        });
    }

    public void IsInternetAvailable(InternetStatus status)
    {
        if (status == InternetStatus.Off)
        {
            isInternetAvailableEvent.Invoke(false);
        }
        else
        {
            isInternetAvailableEvent.Invoke(true);
        }
    }
}
