using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{/*
	[Header("Levels")]
	[Tooltip("The size of the buffer for Tilemaps")]
	public int nbTilemaps = 3;
	[Tooltip("The Tilemap container Grid")]
	public Grid tilemapContainer;
	[Tooltip("The Theme of the game")]
	public LevelTheme theme;
	[Tooltip("The number of cells in a tilemap")]
	public Vector3Int tilemapSize = new Vector3Int(24, 12);
	[Tooltip("All prefabs of maps")]
	public GameObject[] tilemapPrefab;
	[Tooltip("The space between")]
	public float space = 3.84f;

	[Header("Game Controls")]
	[Tooltip("The speed at which the game slides")]
	public float scrollSpeed = 1f;
	[Tooltip("How many Tilemaps needed between each theme swap")]
	public int themeChangeCooldown = 10;


	// index of the current theme
	private Color currentTheme = Color.white;
	// the 
	private Queue<Tilemap> tilemaps;
	// time since last theme swap
	private float timeSinceLastThemeSwap;
	private Tilemap lastTilemap;
	// used when holes overflow on the next tilemap
	private int unfinishedHole;
	private (int y, int size) unfinishedPlateform = (0, 0);

	private void Start() {
		InitialGeneration();
	}
	void InitialGeneration()
	{
		
	}

	private Tilemap GenerateTilemap()
	{
		GameObject prefab = tilemapPrefab[Random.Range(0, tilemapPrefab.Length)];
		Vector3 position = lastTilemap.transform.position;
		position.x += space; 
		GameObject go = Instantiate(prefab, position, new Quaternion());
		return go.GetComponent<Tilemap>();
	}

	private void ChangeTheme()
	{
		timeSinceLastThemeSwap += 1;
		if (timeSinceLastThemeSwap > themeChangeCooldown)
		{
			timeSinceLastThemeSwap = 0;
			currentTheme = new Color(Random.Range(50, 255), Random.Range(50, 255), Random.Range(50, 255));
		}
	}*/
}
