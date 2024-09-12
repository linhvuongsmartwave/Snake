using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Body : MonoBehaviour
{
    [HideInInspector] public int plus;
    [HideInInspector] public int minHeal;
    [HideInInspector] public int maxHeal;
    [HideInInspector] public int MaxHealth;
    [HideInInspector] public int currentHealth;
    public GameObject effectDie;
    public TextMeshProUGUI txtHeal;
    int randomGun;

    public GameObject[] gunPrefab;
    private GameObject attachedGun;

    private void OnEnable()
    {
        minHeal = 5;
        maxHeal = 8;
        plus = PlayerPrefs.GetInt("plus", 0);
        MaxHealth = Random.Range(minHeal + plus, maxHeal + plus);

        if (Random.value > 0.7f)
        {
            randomGun = Random.Range(GameSystem.Instance.characterIndex, gunPrefab.Length);
            attachedGun = Instantiate(gunPrefab[randomGun], transform);
            attachedGun.transform.localPosition = Vector3.zero;
        }
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
        AudioManager.Instance.AudioHit();
        currentHealth -= damage;
        txtHeal.text = currentHealth.ToString();
        transform.DOScale(Vector3.one * 0.8f, 0.08f).SetLoops(2, LoopType.Yoyo);
        if (currentHealth <= 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (attachedGun != null)
        {
            attachedGun.transform.parent = null;
            GameObject gun = GameObject.FindObjectOfType<Gun>().gameObject;
            Vector2 dir = gun.transform.position - this.transform.position;
            attachedGun.transform.DOMove(gun.transform.position, 1f).OnComplete(() =>
            {
                Destroy(attachedGun);

                if (gun.transform.parent != null)
                {
                    Destroy(gun.transform.parent.gameObject);
                }
                GameSystem.Instance.ReplayGun(randomGun);
            });
        }

        GameObject eff = Instantiate(effectDie, transform.position, Quaternion.identity);
        Destroy(eff, 0.7f);
        GameSystem.Instance.listBody.Remove(this);
    }
}
