using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<GameObject> gameObjectsList;

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
                for (int j = 1; j < gameObjectsList.Count; j++)
                {
                    gameObjectsList[j].transform.position = newPosition;
                    Vector3 previousPosition = gameObjectsList[j - 1].transform.position;
                    gameObjectsList[j].transform.position = previousPosition;
                }
                //if (i < gameObjectsList.Count)
                //{
                //    gameObjectsList[i].transform.position = newPosition;
                //}
                i--;
            }
        }
    }
}
