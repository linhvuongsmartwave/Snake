using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Body : MonoBehaviour
{
    public int MaxHealth;
    private int currentHealth;
    public TextMeshProUGUI txtHeal;
    private void OnEnable()
    {
        MaxHealth = Random.Range(3, 5);
    }
    private void Start()
    {
        currentHealth = MaxHealth;
    }
    private void Update()
    {
        txtHeal.text = currentHealth.ToString();
        this.transform.position = this.transform.GetChild(5).position;
    }
    public void TakedDamage(int damage)
    {
        currentHealth -= damage;
        transform.DOScale(Vector3.one * 0.8f, 0.1f).SetLoops(2, LoopType.Yoyo);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
