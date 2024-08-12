using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
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
                    Transform parentTransform = gameObjectsList[j].transform;

                    // Lặp qua tất cả các thành phần con của gameObjectsList[j]
                    for (int k = 0; k < parentTransform.childCount; k++)
                    {
                        Transform childTransform = parentTransform.GetChild(k);

                        // Truy cập vào script Enemy của thành phần con
                        Enemy enemyScript = childTransform.GetComponent<Enemy>();

                        if (enemyScript != null)
                        {
                            // Giảm wavePointIndex để đi lùi lại 1 bước
                            if (enemyScript.wavePointIndex > 0)
                            {
                                enemyScript.wavePointIndex--;

                                // Cập nhật target để di chuyển lùi lại
                                enemyScript.target = Point.points[enemyScript.wavePointIndex];
                            }
                        }
                    }
                }

                if (i < gameObjectsList.Count)
                {
                    Transform parentTransform = gameObjectsList[i].transform;
                    for (int k = 0; k < parentTransform.childCount; k++)
                    {
                        Transform childTransform = parentTransform.GetChild(k);

                        // Truy cập vào script Enemy của thành phần con
                        Enemy enemyScript = childTransform.GetComponent<Enemy>();

                        if (enemyScript != null)
                        {
                            // Giảm wavePointIndex để đi lùi lại 1 bước
                            if (enemyScript.wavePointIndex > 0)
                            {
                                enemyScript.wavePointIndex--;

                                // Cập nhật target để di chuyển lùi lại
                                enemyScript.target = Point.points[enemyScript.wavePointIndex];
                            }
                        }
                    }
                    //gameObjectsList[i].transform.DOMove(newPosition, 0.5f);
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
                    GameObject segment = Instantiate(turn.enemies[i], spawnPosition, Quaternion.Euler(0, 0, 90));
                    gameObjectsList.Add(segment);
                    spawnPosition.x += size;
                }
            }
            gameObjectsList.Reverse();
        }


    }
}
