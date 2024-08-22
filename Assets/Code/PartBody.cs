using DG.Tweening;
using TMPro;
using UnityEngine;

public class PartBody : MonoBehaviour
{

    public Body body;
    public int id;
    public ContainPartBody containPartBody;

    private float speedBack = 1f;

    public bool onMoveFoward;
    private int dame1Turn = 0;



    private void Update()
    {
        containPartBody = GameSystem.Instance.listContainPartBody[id].GetComponent<ContainPartBody>();
        containPartBody.hasBody = true;
        CheckContainPackBodyBack();
        if (onMoveFoward)
        {
            MovingForward();
            return;
        }
        MovingBack();


  
    }

    private void CheckContainPackBodyBack()
    {
        int pos = id + 1;
        if (pos == GameSystem.Instance.listContainPartBody.Count)
        {
            onMoveFoward = true;
            return;
        }

        ContainPartBody container = GameSystem.Instance.listContainPartBody[pos].GetComponent<ContainPartBody>();
        if (container.hasBody)
        {
            onMoveFoward = true;
            return;
        }

        onMoveFoward = false;
        containPartBody.hasBody = false;
        id = pos;
    }

    private void MovingForward()
    {
        transform.position = GameSystem.Instance.listContainPartBody[id].position;
        transform.rotation = GameSystem.Instance.listContainPartBody[id].rotation;
    }

    private void MovingBack()
    {
        ContainPartBody container = GameSystem.Instance.listContainPartBody[id].GetComponent<ContainPartBody>();
        Vector2 targetPosition = container.transform.position;
        transform.DOMove(targetPosition, 0.3f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            body.TakedDamage(1);

        }

    }

    private void OnDestroy()
    {
        containPartBody.hasBody = false;
    }
}