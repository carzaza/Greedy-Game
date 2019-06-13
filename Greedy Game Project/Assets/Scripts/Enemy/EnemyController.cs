using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject[] enemyObjects;
    private List<Vector2> possiblePositions = new List<Vector2>();
    private int enemyIndex;

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        InitializePositions();

        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
    }

    /// <summary> Initializes all the possible positions for the enemies. </summary>
    private void InitializePositions()
    {
        possiblePositions.Add(new Vector2(-6.0f, 2.0f));
        possiblePositions.Add(new Vector2(2.5f, -6.5f));
        possiblePositions.Add(new Vector2(6.0f, -2.5f));
        possiblePositions.Add(new Vector2(-2.0f, -5.0f));
        possiblePositions.Add(new Vector2(-1.5f, 5.5f));
        possiblePositions.Add(new Vector2(4.0f, 2.5f));
        possiblePositions.Add(new Vector2(4.5f, -1.0f));
    }

    /// <summary> Makes the enemy respawn in the scene. </summary>
    public void EnemyRespawn()
    {
        enemyIndex = 0;

        foreach (GameObject enemy in enemyObjects)
        {
            enemy.transform.position = possiblePositions[enemyIndex];
            enemyIndex++;
        }
    }
}