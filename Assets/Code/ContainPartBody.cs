using UnityEngine;

public class ContainPartBody : MonoBehaviour
{
    public bool hasBody;
    private Transform target;
    public float speed = 0.7f;
    public int wavePointIndex = 0;

    private void Awake()
    {
        target = Point.points[wavePointIndex];
    }

    private void Update()
    {
        if (GameSystem.Instance.canMove) Moving();
    }

    private void Moving()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        if (Vector2.Distance(transform.position, target.position) <= 0.01f)
            NextPoint();
    }

    public void NextPoint()
    {
        wavePointIndex++;
        if (wavePointIndex >= Point.points.Length) return;
        target = Point.points[wavePointIndex];
    }
}