using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> gameObjectsList;
    public EnemyData[] enemyDatas;
    public Transform pointSpawn;
    public float size;
    public PointData pointData;

    private void Awake()
    {
        Instance = this;
        EnemyAndPoint(0);
    }

    void Update()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        for (int i = 0; i < gameObjectsList.Count; i++)
        {
            if (!gameObjectsList[i].activeSelf)
            {
                Vector3 newPosition = gameObjectsList[i].transform.position;
                gameObjectsList.RemoveAt(i);

                for (int j = gameObjectsList.Count - 1; j > i; j--)
                {
                    gameObjectsList[j].transform.DOMove(gameObjectsList[j - 1].transform.position, 0.5f);
                }

                if (i < gameObjectsList.Count)
                {
                    gameObjectsList[i].transform.DOMove(newPosition, 0.5f);
                }

                i--;
            }
        }
    }

    public void EnemyAndPoint(int index)
    {
        Instantiate(pointData.point[index], new Vector2(0, 0), Quaternion.identity); 
        if (index < 0 || index >= enemyDatas.Length) return;
        EnemyData turn = enemyDatas[index];
        if (turn.enemies != null)
        {
            Vector3 spawnPosition = pointSpawn.position;

            for (int k = 0; k < turn.lengthSnake; k++)
            {
                for (int i = 0; i < turn.enemies.Count; i++)
                {
                    GameObject enemy = Instantiate(turn.enemies[i], spawnPosition, Quaternion.Euler(0, 0, 90));
                    gameObjectsList.Add(enemy);
                    spawnPosition.x += size;
                }
            }
            gameObjectsList.Reverse();
        }


    }
}
