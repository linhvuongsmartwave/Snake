﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.7f;
    public Transform target;
    public int wavePointIndex = 0;


    private void Start()
    {
        target = Point.points[0];
    }

    private void Update()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        if (Vector2.Distance(transform.position, target.position) <= 0.01f)
        {
            NextPoint();
        }
    }

    public void NextPoint()
    {
        wavePointIndex++;
        if (wavePointIndex >= Point.points.Length)
        {
            return;
        }
        target = Point.points[wavePointIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            transform.parent.GetComponent<Segment>().Damage(1);
        }

    }
}
