using System;
using UnityEngine;

public class PartBody : MonoBehaviour {

    public int id;

    private void Update() {
        MovingForward();
    }

    private void MovingForward() {
        transform.position = GameSystem.Instance.listContainPartBody[id].position;
    }

    private void OnMouseDown() {
        //transform.parent.GetComponent<Segment>().Damage(1);
        Debug.Log("Onclick" + gameObject.name);
        Destroy(gameObject);
    }
}