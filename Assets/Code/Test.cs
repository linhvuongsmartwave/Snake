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
                // Save the position of the GameObject that is being destroyed
                Vector3 newPosition = gameObjectsList[i].transform.position;

                // Remove the null entry
                gameObjectsList.RemoveAt(i);

                // Shift the next GameObject into the removed position
                if (i < gameObjectsList.Count)
                {
                    gameObjectsList[i].transform.position = newPosition;
                }

                // Since we removed an element, we decrement i to stay at the same index
                i--;
            }
        }
    }

    Vector3 GetNewPosition(int index)
    {
        // Example logic to determine new position based on index
        // This could be more complex depending on your game logic
        return new Vector3(index * 2, 0, 0); // Just an example, shifting on x-axis
    }
}
