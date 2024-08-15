using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public int health;
    private void Start()
    {
        
    }
    public void Damage(int health)
    {
        Debug.Log("Damage");
        this.health-=health;
        if (this.health<=0)
        {
            this .gameObject.SetActive(false);
        }
    }
}
