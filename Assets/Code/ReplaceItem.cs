using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceItem : MonoBehaviour
{
    public GameObject effect;
    private void OnDestroy()
    {
        GameObject effect = Instantiate(this.effect, transform.position, Quaternion.identity);
        Destroy(effect, 0.7f);
    }
}
