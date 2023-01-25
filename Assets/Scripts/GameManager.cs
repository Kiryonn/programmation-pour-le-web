using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
	[Header("Levels")]
	[Tooltip("The size of the buffer for Tilemaps")]
	public int nbTilemaps = 3;
	[Tooltip("The Tilemap container Grid")]
	public Grid tilemapContainer;
	[Tooltip("The Theme of the game")]
	public LevelTheme theme;
	[Tooltip("The number of cells in a tilemap")]
	public Vector3Int tilemapSize = new Vector3Int(24, 12);

	[Header("Game Controls")]
	[Tooltip("The speed at which the game slides")]
	public float scrollSpeed;
	[Tooltip("The time needed between each theme swap")]
	public float themeChangeCooldown;
	[Tooltip("The min size of holes (tile size)")]
	public int holeMinSize = 3;
	[Tooltip("The max size of holes (tiles size)")]
	public int holeMaxSize = 6;
	[Tooltip("The chance of hole generation")]
	public float holeChance = 0.3f;

	// index of the current theme
	private Color currentTheme = Color.white;
	// the 
	private Queue<Tilemap> tilemaps;
	// time since last theme swap
	private float timeSinceLastThemeSwap;
	private Tilemap lastTilemap;
	private Vector3 _viewPort;

	// used when holes overflow on the next tilemap
	private int unfinishedHole;

	void Start()
	{
		_viewPort = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
		InitialGeneration();
	}

	void Update()
	{
		DestroyOffScreenTiles();
		GeneratePlateform();
	}

	private void DestroyOffScreenTiles()
	{

	}
	void InitialGeneration()
	{
		tilemaps = new Queue<Tilemap>();
		for (int _ = 0; _ < nbTilemaps; _++)
		{
			Tilemap tm = new GameObject().AddComponent<Tilemap>();
			tm.gameObject.AddComponent<TilemapRenderer>();
			tm.size = tilemapSize;
			tm.transform.position = lastTilemap != null ? lastTilemap.transform.right : new Vector3(0, 0, 0);
			tm.SetTiles(new Vector3Int[] {Vector3Int.zero, new Vector3Int(tilemapSize.x, 0, 0) }, theme.groundTileNormal);
			int startHole = 1;
			if (unfinishedHole != 0)
			{
				startHole += unfinishedHole + 3;
				CreateHole(tm, 0, unfinishedHole);
				unfinishedHole = 0;
			}
			bool hasHole = startHole < tilemapSize.x && holeChance < Random.Range(0f, 1f);
			if (hasHole)
			{
				int holePos = Random.Range(startHole, tilemapSize.x);
				int holeSize = Random.Range(holeMinSize, holeMaxSize + 1);
				CreateHole(tm, holePos, holeSize);
			}
			tilemaps.Enqueue(tm);
			lastTilemap = tm;
			tm.RefreshAllTiles();
			tm.gameObject.SetActive(true);
			tm.transform.parent = tilemapContainer.transform;
		}
	}

	private void GeneratePlateform()
	{

	}

	private void GenerateTilemap()
	{
		if (tilemaps.Count < 4)
		{
			tilemaps.Dequeue();
			Tilemap tm = new Tilemap();
			tm.size = new Vector3Int(24, 12);
			tm.transform.position = lastTilemap.transform.right;
			tilemaps.Enqueue(tm);
		}
	}

	private void CreateHole(Tilemap tm, int pos, int size)
	{
		int overflow = pos + size - tilemapSize.x;
		tm.SetTiles(new Vector3Int[] { new Vector3Int(pos, 0), new Vector3Int(Mathf.Min(pos + size, tilemapSize.x - 1), 0) }, null);
		if (overflow > 0) unfinishedHole = overflow;
	}


	private void ChangeTheme()
	{
		timeSinceLastThemeSwap += Time.deltaTime;
		if (timeSinceLastThemeSwap > themeChangeCooldown)
		{
			timeSinceLastThemeSwap = 0f;
			currentTheme = new Color(Random.Range(50, 255), Random.Range(50, 255), Random.Range(50, 255));
		}
	}
}