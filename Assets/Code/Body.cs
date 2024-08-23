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
    public int plus;
    public int minHeal;
    public int maxHeal;
    private void OnEnable()
    {
        minHeal = 5;
        maxHeal = 8;
        plus = PlayerPrefs.GetInt("plus",0);
        Debug.Log("plus : "+plus);
        MaxHealth = Random.Range(minHeal+plus, maxHeal+ plus);
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
        //DOTween.KillAll();
        transform.DOScale(Vector3.one * 0.8f, 0.08f).SetLoops(2, LoopType.Yoyo);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameObject eff= Instantiate(effectDie,transform.position,Quaternion.identity);
        Destroy(eff,0.7f);
        GameSystem.Instance.listBody.Remove(this);
    }
}
