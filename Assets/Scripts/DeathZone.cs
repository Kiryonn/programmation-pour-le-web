using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam=gameObject.GetComponentInParent<Camera>();

        transform.position=(cam.transform.position+new Vector3(-(cam.orthographicSize*2.5f+1),0,0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("t'est dans la zone");
    }
}
