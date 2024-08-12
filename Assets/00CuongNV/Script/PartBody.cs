using System;
using UnityEngine;

public class PartBody : MonoBehaviour {
    
    private void OnMouseDown() {
        //transform.parent.GetComponent<Segment>().Damage(1);
        Debug.Log("Onclick" + gameObject.name);
    }
}