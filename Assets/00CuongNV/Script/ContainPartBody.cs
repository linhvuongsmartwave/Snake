using UnityEngine;

public class ContainPartBody : MonoBehaviour {

    public float speed = 0.7f;
    private Transform target;
    public int wavePointIndex = 0;

    public bool hasBody;

    private void Awake() {
        target = Point.points[wavePointIndex];
    }

    private void Update() {
        Moving();
    }

    private void Moving() {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= 0.01f) {
            NextPoint();
        }
    }

    public void NextPoint() {
        wavePointIndex++;
        if (wavePointIndex >= Point.points.Length) {
            return;
        }
        target = Point.points[wavePointIndex];
    }
}