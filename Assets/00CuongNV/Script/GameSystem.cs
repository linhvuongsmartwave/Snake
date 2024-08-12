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

    private List<GameObject> gameObjectsList;

    public Point Point;

    public GameObject PartBody;
    public Transform ContainPartBody;
    public int numPartBodyInContainer = 5;
    public int numContainer = 5;

    public Transform pointSpawn;
    public float size;
    public PointData pointData;

    public float timeSpaw;

    private void Start() {
        StartCoroutine(SpawnContainPartBody());
    }

    IEnumerator SpawnContainPartBody() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(timeSpaw);
        for (int i = 0; i < numContainer; i++) {
            Transform container = Instantiate(ContainPartBody,pointSpawn);
            Instantiate(PartBody, container);
            yield return waitForSeconds;
        }
    }
}