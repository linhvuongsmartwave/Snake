using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> gameObjectsList;
    public EnemyData[] enemyDatas;
    public Transform pointSpawn;
    private void Awake()
    {
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
        if (turn.enemies !=null)
        {
            Vector3 spawnPosition = pointSpawn.position;
            for (int i = 0; i < turn.enemies.Count; i++)
            {
                GameObject enemy = Instantiate(turn.enemies[i], spawnPosition, Quaternion.identity);
                gameObjectsList.Add(enemy);
                spawnPosition.x += 0.5f;
            }
            gameObjectsList.Reverse();

        }
     
    }
  

}
