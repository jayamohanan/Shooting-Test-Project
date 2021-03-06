﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    #region Public Filelds
    public GameObject targetPrefab;
    public GameObject gunMuzzle;
    public GameObject crossHair;
    public GameObject gun;
    public Transform targetParent;
    public AudioClip gunShotSound;
    public TextMeshProUGUI crosshairTargetTxt;
    public TextMeshProUGUI redTxt, greenTxt, yellowTxt, blackTxt;
    public int panAngle = 120;
    public float ZSpawnDistance = 15;//Distance along z axis for target spawning
    public float camShakeStrength = 0.5f;//For gun shake while shooting
    #endregion
    #region Privat Fields
    private AudioSource audioSource;
    private float bulletImpact = 10000f;
    private Queue<GameObject> targets = new Queue<GameObject>();//Pool queue
    //Distance from camera for spawning
    private float zSpawnDistanceFromCamera;
    private float ySpawnDistanceFromCamera;
    private int targetPoolCount = 100; //Initialize pool with 100 target for reusing
    private float targetSpawnRange;
    private Color[] targetColors = new Color[] { Color.red, Color.green, Color.yellow, Color.black };
    private string[] targetTags = new string[] { "red", "green", "yellow", "black" };
    private int redCount, greenCount, yellowCount, blackCount;
    private float lastSpawnTime;
    private RaycastHit hit;
    private string targetAtCrossHairMessage;
    #endregion
    #region Unity Callbacks
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        float horizontalFOV = Camera.VerticalToHorizontalFieldOfView(Camera.main.fieldOfView, Camera.main.aspect);
        Debug.Log("Horizontal FOV is " + horizontalFOV + " degrees, targets are spawned in 60 degree FOV region");
        zSpawnDistanceFromCamera = ZSpawnDistance - Camera.main.transform.position.z;
        ySpawnDistanceFromCamera = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height * 1.05f, zSpawnDistanceFromCamera)).y;//worldPos.y, above screen
        targetSpawnRange = Mathf.Tan(panAngle * Mathf.Deg2Rad / 2) * zSpawnDistanceFromCamera;//spawning in this range, pan angle
    }
    void Start()
    {
        audioSource.clip = gunShotSound;
        InitializeTargetPool();
        lastSpawnTime = Time.time;
    }
    void Update()
    {
        if ((Time.time - lastSpawnTime) > 0.5f)
        {
            SpawnTargets();
            lastSpawnTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        crossHair.transform.position = gunMuzzle.transform.position + gunMuzzle.transform.forward * (zSpawnDistanceFromCamera - 1);
        crossHair.transform.LookAt(Camera.main.transform, Camera.main.transform.up);
        targetAtCrossHairMessage = crosshairTargetTxt.text;
        if (Physics.Raycast(gunMuzzle.transform.position, gunMuzzle.transform.forward, out hit, 100))
        {
            if (targetAtCrossHairMessage == "")
            {
                crosshairTargetTxt.text = "On GunPoint \n" + hit.collider.tag + " Cube";
            }
        }
        else
        {
            crosshairTargetTxt.text = "";
        }
    }
    #endregion
 
    private void InitializeTargetPool()
    {
        for (int i = 0; i < targetPoolCount; i++)
        {
            AddTargetToPool();
        }
    }
    private void AddTargetToPool()//Instantite a target and add to pool queue
    {
        GameObject target = Instantiate(targetPrefab, targetParent) as GameObject;
        target.SetActive(false);
        target.GetComponent<Target>().TargetInvisible += (targetObject)=> { targets.Enqueue(targetObject);};
        targets.Enqueue(target);
    }
    public void Fire()
    {
        RaycastHit hit;
        if(Physics.Raycast(gunMuzzle.transform.position, gunMuzzle.transform.forward, out hit, 100))
        {
            hit.rigidbody.AddForce(new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f), 1)*bulletImpact);
            switch (hit.collider.tag)
            {
                case "red":
                    redCount++;
                    break;
                case "green":
                    greenCount++;
                    break;
                case "yellow":
                    yellowCount++;
                    break;
                case "black":
                    blackCount++;
                    break;
                default:
                    break;
            }
            UpdateHitCount();
        }
        audioSource.Play();
        StartCoroutine(gun.GetComponent<CameraShake>().ShakeCamera(0.1f, camShakeStrength));
    }
    private void UpdateHitCount()
    {
        if (redTxt.text != redCount.ToString())
            redTxt.text = redCount.ToString();
        if (greenTxt.text != greenCount.ToString())
            greenTxt.text = greenCount.ToString();
        if (yellowTxt.text != yellowCount.ToString())
            yellowTxt.text = yellowCount.ToString();
        if (blackTxt.text != blackCount.ToString())
            blackTxt.text = blackCount.ToString();
    }
    private void SpawnTargets()//Activate targets from queue and spawn at required location 
    {
        if (targets.Count==0)
        {
            AddTargetToPool();
            return;
        }
        GameObject target = targets.Dequeue();
        Vector3 spawnPosition = new Vector3(Random.Range(-targetSpawnRange, targetSpawnRange), ySpawnDistanceFromCamera, ZSpawnDistance);
        Vector3 spawnRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        target.transform.position = spawnPosition;
        target.transform.rotation = Quaternion.Euler(spawnRotation);

        int colorIndex = Random.Range(0, 4);
        MeshRenderer targetMeshRenderer = target.GetComponent<MeshRenderer>();
        if (targetMeshRenderer)
        {
            targetMeshRenderer.material.color = targetColors[colorIndex];
        }
        else
        {
            Debug.LogError("No MeshRenderer");
        }
        target.tag = targetTags[colorIndex];
        target.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
