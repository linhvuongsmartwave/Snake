using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> gameObjectsList;
    public EnemyData[] enemyDatas;
    public Transform pointSpawn;
    public float size;
    public List<GameObject> segment = new List<GameObject>();


    private void Awake()
    {
        Instance = this;
        EnemyStart(0);
    }

    void Update()
    {
        UpdateList();
    }

    void UpdateList()
    {
        for (int i = 0; i < gameObjectsList.Count; i++)
        {
            if (!gameObjectsList[i].activeSelf)
            {
                Vector3 newPosition = gameObjectsList[i].transform.position;
                gameObjectsList.RemoveAt(i);

                for (int j = gameObjectsList.Count - 1; j > i; j--)
                {
                    gameObjectsList[j].transform.position = gameObjectsList[j - 1].transform.position;
                }

                if (i < gameObjectsList.Count)
                {
                    gameObjectsList[i].transform.position = newPosition;
                }

                i--;
            }
        }
    }

    public void EnemyStart(int index)
    {
        if (index < 0 || index >= enemyDatas.Length) return;
        EnemyData turn = enemyDatas[index];
        if (turn.enemies != null)
        {
            Vector3 spawnPosition = pointSpawn.position;

            for (int k = 0; k < turn.length; k++)
            {
                for (int i = 0; i < turn.enemies.Count; i++)
                {
                    segment.Clear();

                    for (int j = 0; j < 6; j++)
                    {
                        GameObject enemy = Instantiate(turn.enemies[i], spawnPosition, Quaternion.identity);

                        gameObjectsList.Add(enemy);
                        segment.Add(enemy);
                        spawnPosition.x += size;
                    }
                }
            }

            gameObjectsList.Reverse();
        }
    }
}


