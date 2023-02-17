using System.Collections;
using System.Collections.Generic;
using Koboct.Data;
using Koboct.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    dreamloLeaderBoard dl;
    [SerializeField] private List<dreamloLeaderBoard.Score> scoreList=null;
    public PlayerData MyPlayerData;
    public QuestionManager affichage;
    public ScormScore scormScore;





    // Start is called before the first frame update
    void Start()
    {
      
        appel();

    }

    public void appel()
    {
        Debug.Log("bon mieux que rien");
        dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

        dl.AddScore(MyPlayerData.Name, MyPlayerData.Score);

        dl.GetScores();
        scormScore.transform.gameObject.SetActive(false);
        scormScore =new ScormScore();

        scormScore.transform.gameObject.SetActive(true);

        StartCoroutine(CheckScrore());
    }

    private IEnumerator CheckScrore()
    {
        while (dl.ToStringArray()==null)
        {
            Debug.Log("toujour la ");
            yield return new WaitForSeconds(2);
        }
        scoreList = dl.ToListHighToLow();

        Debug.Log("si je te dit que c'est bon ");
        DisplayScore();
    }

    private void DisplayScore()
    {
        //affichage.leaderBoard(scoreList);

        foreach (var score in scoreList)
        {
           Debug.Log(score.playerName);
        }

        affichage.leaderBoard(scoreList);

    }

    public List<dreamloLeaderBoard.Score> DisplayScoreList()
    {

        return scoreList;

    }

    
    private void Reset()
    {
        appel();
    }



}
