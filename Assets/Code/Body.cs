using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Body : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;
    public TextMeshProUGUI txtHeal;
    public GameObject effectDie;
    private void OnEnable()
    {
        MaxHealth = Random.Range(10, 15);
    }
    private void Start()
    {
        currentHealth = MaxHealth;
        txtHeal.text = currentHealth.ToString();
    }
    private void Update()
    {
        this.transform.position = this.transform.GetChild(5).position;
    }
    public void TakedDamage(int damage)
    {
        currentHealth -= damage;
        txtHeal.text = currentHealth.ToString();

        transform.DOScale(Vector3.one * 0.8f, 0.1f).SetLoops(2, LoopType.Yoyo);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameObject eff= Instantiate(effectDie,transform.position,Quaternion.identity);
        Destroy(eff,0.7f);
    }
}
