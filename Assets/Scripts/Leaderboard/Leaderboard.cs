using UnityEngine;
using Dan.Main;
using Dan.Models;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    private const string GAME_SCENE_NAME = "Game";
    private const string SCOREBOARD_SCENE_NAME = "Scoreboard";

    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private JSONSaving saveManager;
    [SerializeField] private GameObject prefab;
    protected PlayerData playerData;
    public RectTransform playerEntryRect;
    private int playerIndex = -1;
    private string sceneName;

    private string publicLeaderboardKey = "4da4217571108f6e4f81b99faeae1fb7c64162fa2c9cb2d13e527db217fe61a1";
    public LeaderboardSearchQuery query = new LeaderboardSearchQuery();
    
    public UnityEvent<bool> successfullSubmitEvent;
    public UnityEvent<string> errorSubmitEvent;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        playerData = saveManager.LoadData();
        if (sceneName == SCOREBOARD_SCENE_NAME)
        {
          GetLeaderboard();  
        }
    }

    public void GetLeaderboard(string name)
    {
        query.Username = name;
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, query, (msg) =>
        {
            if (msg.Length > 0)
            {
                playerIndex = msg[0].Rank;
                GetLeaderboard();
            }
        });
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey,false, (msg) =>
        {
            // In the Game scene we want just a focused leaderboard in contrast to the scoreboard scene
            int startingIndex = (sceneName == GAME_SCENE_NAME) ? playerIndex-5 : 0;
            if (startingIndex < 0)
            {
                startingIndex = 0;
            }
            int loopLength = (sceneName == GAME_SCENE_NAME) ? startingIndex + 10 : msg.Length;
            if (loopLength > msg.Length)
            {
                startingIndex -= (loopLength - msg.Length);
                loopLength = msg.Length; 
            }
            
            for (int i = startingIndex; i < loopLength; i++)
            {
                CreateEntryOnScoreBoard(msg, i);
            }
        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, (msg) =>
        {
            if (msg)
            {
                playerData.score = score;
                saveManager.SaveData(playerData);
            }
            successfullSubmitEvent.Invoke(msg);

            if (sceneName == GAME_SCENE_NAME) {
                GetLeaderboard(playerData.name);
            } else {
                GetLeaderboard();
            }
        }, (err) => {

            errorSubmitEvent.Invoke(err);
        });
    }

    private void CreateEntryOnScoreBoard(Entry[] msg, int i)
    {
        GameObject newEntry = Instantiate(prefab, scrollViewContent);
        if (newEntry.TryGetComponent(out LeaderboardEntry item))
        {
            item.rank = msg[i].Rank.ToString();
            item.playerName = msg[i].Username;
            item.score = msg[i].Score.ToString();
            if (msg[i].Username == playerData.name)
            {
                playerEntryRect = item.GetComponent<RectTransform>();
                HighlightPlayerEntry(item);
            }

            if (sceneName == GAME_SCENE_NAME)
            {
                SetMeterColours(i,item);
            }
        }
    }

    private void SetMeterColours(int i, LeaderboardEntry item)
    {
        if (i < playerIndex-1)
        {
            PaintEntryGreen(item);
        }
        else if (i > playerIndex-1)
        {
            PaintEntryRed(item);
        }
    }

    private void HighlightPlayerEntry(LeaderboardEntry entry)
    {
        entry.nameText.color = new Color(255, 165, 0);
        entry.scoreText.color = new Color(255, 165, 0);
        entry.rankText.color = new Color(255, 165, 0);
    }

    private void PaintEntryGreen(LeaderboardEntry entry)
    {
        entry.nameText.color = new Color(0, 255, 0);
        entry.scoreText.color = new Color(0, 255, 0);
        entry.rankText.color = new Color(0, 255, 0);
    }

    private void PaintEntryRed(LeaderboardEntry entry)
    {
        entry.nameText.color = new Color(255, 0, 0);
        entry.scoreText.color = new Color(255, 0, 0);
        entry.rankText.color = new Color(255, 0, 0);
    }
}
