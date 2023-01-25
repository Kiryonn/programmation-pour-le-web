using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    public bool asPlayer;

    void Start()
    {
        //if (name == "downZone") { asPlayer = true; } else { asPlayer = false; }
        asPlayer = (name == "downZone");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Player") { asPlayer = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { asPlayer = false; }
    }

}
