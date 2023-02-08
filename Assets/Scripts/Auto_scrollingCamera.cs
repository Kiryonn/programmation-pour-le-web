using Koboct.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_scrollingCamera : MonoBehaviour
{
    public float vitesseHorizontale = 0;
    //public float vitesseVericale = 0;

    public GameObject suivre;
    public Camera cam;

    public CharacterController characterController;
    public QuestionManager QuestionManager;
    public GameObject question;

    public GameObject[] cible;

    

    private Vector3 movCam;

    //public bool regardeH = false;
    public bool regardeB = false;

 


    private void Start()
    {
        movCam=transform.position;
    }


    // Update is called once per frame


    



        public float smoothSpeed = 0.125f; // The speed at which the movCam follows
  

    void LateUpdate()
    {
        //Vector3 desiredPosition = player.position + movCam;
        movCam.x += vitesseHorizontale * Time.deltaTime;
       // if (suivre != null) { regarde(); }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, movCam, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void go() { Reset(); vitesseHorizontale = 6; }

    public void death() 
    {
    
    vitesseHorizontale = 0;

        question.SetActive(true);
        QuestionManager.gameObject.SetActive(true);
        QuestionManager.information();

    }

    public void Reset()
    {
        characterController.restart = true;
        transform.position = new Vector3(0, 0, -10);
        characterController.gameObject.transform.position = new Vector3(-1, -6, 0);
        

                //Vector3 desiredPosition = player.position + movCam;
        movCam.x = vitesseHorizontale * Time.deltaTime;
       // if (suivre != null) { regarde(); }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, movCam, smoothSpeed);
        transform.position = smoothedPosition;

        foreach( GameObject x in cible)
        {
            x.SetActive(true);
        }

    }

}
