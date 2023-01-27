using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
[CreateAssetMenu()]
public class LevelTheme : ScriptableObject
{
	[Tooltip("Name of the theme")]
	public string themeName = "";

	[Header("Ground")]
	[Tooltip("The start Tile for the ground")]
	public TileBase groundTileStart;
	[Tooltip("The regular Tile for the ground")]
	public TileBase[] groundTileNormal;
	[Tooltip("The end tile for the ground")]
	public TileBase groundTileEnd;

	[Header("Plateformes")]
	[Tooltip("The start Tile for plateforms")]
	public TileBase platerformStart;
	[Tooltip("The regular Tiles for plateforms")]
	public TileBase[] plateformNormal;
	[Tooltip("The end Tile for plateforms")]
	public TileBase plateformEnd;
	[Tooltip("The size of a tiny plateform (must be > 1)")]
	public int tinySize;
	[Tooltip("The size of a normal plateform (must be > 1)")]
	public int mediumSize;
	[Tooltip("The size of a large plateform (must be > 1)")]
	public int largeSize;
}