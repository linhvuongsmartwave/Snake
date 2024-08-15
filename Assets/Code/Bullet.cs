using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ObjectPooling.Instance.ReturnPooledObject(this.gameObject);
            GameObject effect = Instantiate(this.effect,transform.position,Quaternion.identity);
            Destroy(effect,0.7f);
        }
    }

}
