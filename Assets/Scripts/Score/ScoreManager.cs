using System.Collections;
using System.Collections.Generic;
using Koboct.Data;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    dreamloLeaderBoard dl;
    [SerializeField] private List<dreamloLeaderBoard.Score> scoreList;
    public PlayerData MyPlayerData;
    
    
    // Start is called before the first frame update
    void Start()
    {
        dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
     
        dl.AddScore(MyPlayerData.Name, MyPlayerData.Score);
        
        dl.GetScores();
        StartCoroutine(CheckScrore());

    }

    private IEnumerator CheckScrore()
    {
        while (dl.ToStringArray()==null)
        {

            yield return new WaitForSeconds(2);
        }
        scoreList = dl.ToListHighToLow();

        DisplayScore();
    }

    private void DisplayScore()
    {
        foreach (var score in scoreList)
        {
           Debug.Log(score.playerName);
        }
    }
}
