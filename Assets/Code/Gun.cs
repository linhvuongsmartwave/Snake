using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public DynamicJoystick joystick;
    public Transform pointShoot;
    public Transform pointShoot1;
    public Transform pointShoot2;
    public float timeBetween;
    public float bulletSpeed = 50f;
    public float offset;
    public UiPanelDotween lose;
    public bool hasLose=false;
    private float shootTimer = 0f; // Thêm biến đếm thời gian
    public float speedShoot;
    private void OnEnable()
    {
        joystick = GameObject.FindObjectOfType<DynamicJoystick>();
        lose = GameObject.Find("boardlose").GetComponent<UiPanelDotween>();
    }

    public PickGun pickgun;
    public enum PickGun
    {
        gun1,
        gun2,
        gun3
    }


    void Start()
    {
        if (!joystick) print("Null Joystick");else print("co Joystick");
    }

    private void Update()
    {
        RotationGun();
        shootTimer += Time.deltaTime;
        if (shootTimer >= speedShoot)
        {
            Shoot();
            shootTimer = 0f; 
        }
    }

    void RotationGun()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (direction.sqrMagnitude < 0.01f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            float rotationSpeed = 1.0f;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (direction.sqrMagnitude < 0.01f)
            return;
        direction = direction.normalized;
        List<Transform> shootPoints = new List<Transform>();

        if (pickgun == PickGun.gun1)
            shootPoints.Add(pointShoot);
        else if (pickgun == PickGun.gun2)
        {
            shootPoints.Add(pointShoot1);
            shootPoints.Add(pointShoot2);
        }
        else if (pickgun == PickGun.gun3)
        {
            shootPoints.Add(pointShoot);
            shootPoints.Add(pointShoot1);
            shootPoints.Add(pointShoot2);
        }
        foreach (Transform shootPoint in shootPoints)
        {
            FireBullet(shootPoint, direction);
        }
    }

    void FireBullet(Transform shootPoint, Vector2 direction)
    {
        GameObject bullet = ObjectPooling.Instance.GetPooledObject("bullet");
        if (bullet != null)
        {
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation= this.transform.rotation;
            bullet.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        AudioManager.Instance.AudioShoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!hasLose)
            {
                Lose();
                GameSystem.Instance.canMove = false;
                hasLose = true;
            }
        }
    }
    public void Lose()
    {
        lose.PanelFadeIn();
    }
}
