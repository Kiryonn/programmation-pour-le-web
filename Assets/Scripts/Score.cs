using Koboct.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerData MyPlayerData;
    void Start()
    {
        Reset();
    }

    public void addScore()
    { MyPlayerData.Score++; }

    public void Reset()
    {
        MyPlayerData.Score = 0;

    }

    public int getScore()
    {
        return MyPlayerData.Score;
    }

}
