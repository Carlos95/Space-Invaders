using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.Events;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "4da4217571108f6e4f81b99faeae1fb7c64162fa2c9cb2d13e527db217fe61a1";

    // Start is called before the first frame update
    void Start()
    {
        GetLeaderboard();
    }

    public UnityEvent<bool> successfullSubmitEvent;

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey,false, (msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, (msg) =>
        {
            if (msg)
            {
                SaveManager.SaveInt("HighScore", score);
            }
            successfullSubmitEvent.Invoke(msg);
            GetLeaderboard();
        });
    }
}
