using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_scrollingCamera : MonoBehaviour
{
    public float vitesseHorizontale = 0;
    //public float vitesseVericale = 0;

    public GameObject suivre;
    public Camera cam;

    public PlayerZone upZone;
    public PlayerZone downZone;

    

    private Vector3 camera;

    //public bool regardeH = false;
    public bool regardeB = false;


    private void Start()
    {
        camera=transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        camera.x += vitesseHorizontale * Time.deltaTime;

        if (suivre != null) { regarde(); }
        
        transform.position = camera;

    }


    private void regarde()
    {
        //if (regardeB) { camera.y = suivre.transform.position.y - cam.orthographicSize*2/4 ; }
  
        if (upZone.asPlayer) { camera.y += 10*5/8f *Time.deltaTime; }
        else if (downZone.asPlayer) { camera.y =suivre.transform.position.y; }

        Debug.Log(""+upZone.asPlayer +"/" +downZone.asPlayer);
        

    }


}
