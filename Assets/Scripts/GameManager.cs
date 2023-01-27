using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Vector3 _viewPort;

	void Start()
	{
		_viewPort = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
	}

	void Update()
	{
		DestroyOffScreenTiles();
	}

	private void DestroyOffScreenTiles()
	{

	}
	
}