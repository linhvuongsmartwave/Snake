using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<GameObject> gameObjectsList;

    private void Awake()
    {
        gameObjectsList.Reverse();
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
}
