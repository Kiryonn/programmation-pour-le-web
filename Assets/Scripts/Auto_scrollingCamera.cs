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

    

    private Vector3 movCam;

    //public bool regardeH = false;
    public bool regardeB = false;


    private void Start()
    {
        movCam=transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        

       // if (suivre != null) { regarde(); }
        
      //  transform.position = movCam;

    }


    private void regarde()
    {
        //if (regardeB) { movCam.y = suivre.transform.position.y - cam.orthographicSize*2/4 ; }
  
        if (upZone.asPlayer) { movCam.y += 10*5/8f *Time.deltaTime; }
        else if (downZone.asPlayer) { movCam.y =suivre.transform.position.y * Time.deltaTime; }

        Debug.Log(""+upZone.asPlayer +"/" +downZone.asPlayer);
        

    }


        public float smoothSpeed = 0.125f; // The speed at which the movCam follows
  

    void LateUpdate()
    {
        //Vector3 desiredPosition = player.position + movCam;
        movCam.x += vitesseHorizontale * Time.deltaTime;
       // if (suivre != null) { regarde(); }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, movCam, smoothSpeed);
        transform.position = smoothedPosition;
    }

}
