using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.7f;
    private Transform target;
    private int wavePointIndex = 0;

    private void Start()
    {
        target = Point.points[0];
    }

    private void Update()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            NextPoint();
        }
    }
    public void NextPoint()
    {
        wavePointIndex++;
        target=Point.points[wavePointIndex];
    }

}
