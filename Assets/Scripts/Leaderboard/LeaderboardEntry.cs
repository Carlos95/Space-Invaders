using UnityEngine;
using TMPro;

public class LeaderboardEntry : MonoBehaviour
{
    public  TMP_Text nameText;
    public TMP_Text scoreText;
    public TMP_Text rankText;

    private string m_playerName;

    public string playerName
    {
        get { return m_playerName;}
        set { 
            nameText.SetText(value);
            m_playerName = value;
        }
    }

    public string score
    {
        set { scoreText.SetText(value); }
    }

    public string rank
    {
        set { rankText.SetText(value); }
    }
}
