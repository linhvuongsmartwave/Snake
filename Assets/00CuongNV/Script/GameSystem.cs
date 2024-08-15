using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameSystem : MonoBehaviour {
    #region Singleton

    public static GameSystem Instance;

    private void Awake() {
        Instance = this;
    }

    #endregion

    [HideInInspector]
    public List<Transform> listContainPartBody;

    public Point Point;

    public GameObject Body;
    
    public GameObject PartBody1;
    public GameObject PartBody2;
    public GameObject head;
    public Transform ContainPartBody;
    public int numBody ;

    public int numPartBody ;
    public int numContainer;

    private int currenPartBody;
    private int currentBody;

    public Transform pointSpawn;
    public float size;
    public PointData pointData;

    public float timeSpaw;

    private List<Body> listBody = new List<Body>();

    private void Start() {
        currenPartBody = 0;
        currentBody = 0;
        for(int i  = 0; i < numBody; i++)
        {
            Body body = Instantiate(Body).GetComponent<Body>();
            listBody.Add(body);
        }

        StartCoroutine(SpawnContainPartBody());
    }

    IEnumerator SpawnContainPartBody() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(timeSpaw);
        for (int i = 0; i < numContainer; i++) {
            currenPartBody++;
            Transform container = Instantiate(ContainPartBody,pointSpawn);
            container.GetComponent<ContainPartBody>().hasBody = true;
            listContainPartBody.Add(container);

            if(currenPartBody == 10)
            {
                currenPartBody = 0;
                currentBody++;
            }
            GameObject partBodyPrefab = (currentBody % 2 == 0) ? PartBody1 : PartBody2;

            PartBody partBody = Instantiate(partBodyPrefab).GetComponent<PartBody>();
            partBody.body = listBody[currentBody];
            partBody.transform.SetParent(listBody[currentBody].transform);
            partBody.id = i;
            yield return waitForSeconds;
        }
    }
}