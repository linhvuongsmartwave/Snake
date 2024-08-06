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

    private void Awake()
    {
        Instance = this;
        EnemyStart(0);
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
                //int newWavePointIndex = gameObjectsList[i].GetComponent<Enemy>().wavePointIndex;
                gameObjectsList.RemoveAt(i);

                for (int j = gameObjectsList.Count - 1; j > i; j--)
                {
                    gameObjectsList[j].transform.position = gameObjectsList[j - 1].transform.position;
                    //gameObjectsList[j].GetComponent<Enemy>().wavePointIndex = gameObjectsList[j - 1].GetComponent<Enemy>().wavePointIndex;
                }

                if (i < gameObjectsList.Count)
                {
                    gameObjectsList[i].transform.position = newPosition;
                    //gameObjectsList[i].GetComponent<Enemy>().wavePointIndex = newWavePointIndex;
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
