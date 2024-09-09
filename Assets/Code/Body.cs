using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Body : MonoBehaviour
{
    public int plus;
    public int minHeal;
    public int maxHeal;
    public int MaxHealth;
    public int currentHealth;
    public GameObject effectDie;
    public TextMeshProUGUI txtHeal;

    // Thuộc tính mới liên quan đến súng
    public GameObject gunPrefab; // Prefab của súng để gắn vào một số đoạn của thân
    private GameObject attachedGun; // Súng được gắn vào đoạn thân này

    private void OnEnable()
    {
        minHeal = 5;
        maxHeal = 8;
        plus = PlayerPrefs.GetInt("plus", 0);
        MaxHealth = Random.Range(minHeal + plus, maxHeal + plus);

        // Ngẫu nhiên quyết định liệu có gắn súng vào đoạn thân này không
        if (Random.value > 0.5f) // 50% cơ hội có súng
        {
            attachedGun = Instantiate(gunPrefab, transform); // Gắn súng vào thân
            attachedGun.transform.localPosition = Vector3.zero; // Điều chỉnh vị trí súng nếu cần
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
        // Nếu có súng được gắn vào, làm cho súng rơi xuống vị trí (0,0)
        if (attachedGun != null)
        {
            attachedGun.transform.parent = null; // Tách súng khỏi thân
            attachedGun.transform.DOMove(Vector3.zero, 1f); // Di chuyển súng rơi xuống (0,0)
        }

        GameObject eff = Instantiate(effectDie, transform.position, Quaternion.identity);
        Destroy(eff, 0.7f);
        GameSystem.Instance.listBody.Remove(this);
    }
}
