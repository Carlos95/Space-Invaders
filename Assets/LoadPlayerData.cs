using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerData : MonoBehaviour
{
    [SerializeField] private JSONSaving saveManager;
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = saveManager.LoadData();
    }

    
}
