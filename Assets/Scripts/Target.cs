using Koboct.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private PlayerData Mydata;



    public GameObject sono;



    public PlayerData MyPlayerData;


    public void addScore()
    { MyPlayerData.Score++; }






    public void init(PlayerData data)
    {
        Mydata = data;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //target.Stop();
        GameObject go = Instantiate(sono,transform.position,transform.rotation);

        addScore();
        Debug.Log(collision.name);
        //Mydata.Score += 1;

        Destroy(collision.gameObject);

        Destroy(go, 2);
        gameObject.SetActive(false);
        //Destroy(gameObject);
        
    }

    

}
