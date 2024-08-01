using System.Collections.Generic;
using UnityEngine;

public class EnemySegment : MonoBehaviour
{
    public List<GameObject> segment = new List<GameObject>();

    public void Initialize(List<GameObject> segment)
    {
        this.segment = segment;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            foreach (GameObject enemy in segment)
            {
                Debug.Log("an di 1 khuc");
                enemy.SetActive(false);
            }
        }
    }
}