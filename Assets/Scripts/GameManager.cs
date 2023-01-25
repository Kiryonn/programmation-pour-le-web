using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
	[Header("Levels")]
    [Tooltip("The number of levels generated at the same time")]
	public int nb_levels = 2;

	[Tooltip("The variants of levels that can spawn")]
	public List<GameObject> levels_variants;

	[Tooltip("The levels container GameObject")]
	public GameObject levels_container;

	[Header("Death zone")]
	[Tooltip("The zone off screen that insta kills the player")]
	public GameObject death_zone;

	[Header("Plateformes")]
	[Tooltip("")]
	public List<GameObject> plateforme_variants;


    void Start()
    {
		for (int i = 0; i < nb_levels; i++)
		{
			GameObject level = Instantiate(
				levels_variants[Random.Range(0, 1)]
				);
			levels_variants.GetRange(0, 5);
			level.transform.parent = levels_container.transform;
			levels_variants.Add(level);
		}
    }

    void Update()
    {
        for (int i = 0; i < nb_levels; i++)
		{
			
		}
	}
}
