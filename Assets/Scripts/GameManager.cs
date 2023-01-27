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
		// GenerateTilemap();
	}

	private void DestroyOffScreenTiles()
	{

	}
	void InitialGeneration()
	{
		tilemaps = new Queue<Tilemap>();
		Tilemap tm = CreateTilemap();

		// for (int _ = 0; _ < nbTilemaps; _++)
		// {
		// 	tm.transform.position = lastTilemap != null ? lastTilemap.transform.right : new Vector3(0, 0, 0);
		// 	tilemaps.Enqueue(tm);
		// }
	}

	private void GenerateTilemap()
	{
		if (tilemaps.Count >= 3)
		{
			tilemaps.Dequeue();
		}
		if (tilemaps.Count < 3)
		{
			Tilemap tm = CreateTilemap();
			tm.transform.position = lastTilemap.transform.localPosition;
			tilemaps.Enqueue(tm);
			lastTilemap = tm;
		}
	}

	private Tilemap CreateTilemap()
	{
		// create components
		GameObject go = new GameObject();
		Tilemap tm = go.AddComponent<Tilemap>();
		go.AddComponent<TilemapRenderer>();
		go.AddComponent<TilemapCollider2D>();
		go.layer = LayerMask.NameToLayer("Map");

		go.transform.parent = tilemapContainer.transform;
		go.transform.localScale = Vector3.one * 0.1f;

		// set tilemap cells
		tm.size = tilemapSize;

		// create terrain
		int tileindex = 0;
		for (int i = 0; i < tilemapSize.x; i++)
		{
			tm.SetTile(new Vector3Int(i, 0, 0), theme.groundTileNormal[tileindex]);
			tileindex++;
			if (tileindex == theme.groundTileNormal.Length) tileindex = 0;
		}
		// create hole
		int startHole = 1;
		if (unfinishedHole != 0)
		{
			startHole += unfinishedHole + 3;
			FinishHole(tm, unfinishedHole);
			unfinishedHole = 0;
		}
		bool hasHole = startHole < tilemapSize.x && holeChance > Random.Range(0f, 1f);
		if (hasHole)
		{
			int holePos = Random.Range(startHole, tilemapSize.x);
			int holeSize = Random.Range(holeMinSize, holeMaxSize + 1);
			CreateHole(tm, holePos, holeSize);
		}
		// add plateforms


		// visibility
		tm.RefreshAllTiles();
		go.SetActive(true);

		return tm;
	}

	private void CreatePlateform(Tilemap tm, Vector3Int pos, int size)
	{
		// tm.SetTiles();
	}

	private void CreateHole(Tilemap tm, int start, int size)
	{
		Debug.Log((start, size));
		int overflow = Mathf.Max(0, start + size - tilemapSize.x);
		Debug.Log(overflow);
		tm.SetTile(new Vector3Int(start-1, 0), theme.groundTileEnd);
		for (int i = start; i < start + size - overflow; i++)
		{
			Vector3Int position = new Vector3Int(i, 0);
			tm.SetTile(position, null);
			tm.SetTileFlags(position, TileFlags.None);
		}
		if (overflow > 0) unfinishedHole = overflow;
		else tm.SetTile(new Vector3Int(start + size, 0), theme.groundTileStart);
	}

	private void FinishHole(Tilemap tm, int size) {
		for (int i = 0; i < size; i++)
		{
			Vector3Int position = new Vector3Int(i, 0);
			tm.SetTile(position, null);
			tm.SetTileFlags(position, TileFlags.None);
		}
		tm.SetTile(new Vector3Int(size, 0), theme.groundTileStart);
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