using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Body : MonoBehaviour
{
    public float MaxHealth;
    private int currentHealth;
    public TextMeshProUGUI txtHeal;
    private void Update()
    {
        txtHeal.text= currentHealth.ToString();
    }
    public void TakedDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) { 
            Destroy(gameObject);
        }
    }
}
