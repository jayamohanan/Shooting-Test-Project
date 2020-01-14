using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject gunMuzzle;
    public GameObject crossHair;
    public float ZSpawnDistance = 15;
    private float bulletImpact = 10000f;
    public TextMeshProUGUI redTxt, greenTxt, yellowTxt, blackTxt;
    private Queue<GameObject> targets = new Queue<GameObject>();
    private float zSpawnDistanceFromCamera;
    private float ySpawnDistanceFromCamera;
    private int targetPoolCount = 100; 
    public int panAngle = 120;
    private float targetSpawnRange;
    private Color[] targetColors = new Color[] { Color.red, Color.green, Color.yellow, Color.black };
    private string[] targetTags = new string[] { "red", "green", "yellow", "black" };
    private int redCount, greenCount, yellowCount, blackCount;
    private float lastSpawnTime;
    void Start()
    {
        zSpawnDistanceFromCamera = ZSpawnDistance - Camera.main.transform.position.z;
        ySpawnDistanceFromCamera = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.2f, zSpawnDistanceFromCamera)).y;
        targetSpawnRange = Mathf.Tan(panAngle/2)* zSpawnDistanceFromCamera;
        InitializeTargetPool();
        lastSpawnTime = Time.time;
    }
    private void InitializeTargetPool()
    {
        for (int i = 0; i < targetPoolCount; i++)
        {
            AddTargetToPool();
        }
    }
    private void AddTargetToPool()
    {
        GameObject target = Instantiate(targetPrefab) as GameObject;
        target.SetActive(false);
        target.GetComponent<Target>().TargetInvisible += (targetObject)=> { targets.Enqueue(targetObject);};
        targets.Enqueue(target);
    }
    void Update()
    {
        if ((Time.time - lastSpawnTime)>0.5f)
        {
            SpawnTargets();
            lastSpawnTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        crossHair.transform.position = gunMuzzle.transform.position+ gunMuzzle.transform.forward * (zSpawnDistanceFromCamera - 1);
        crossHair.transform.LookAt(Camera.main.transform, Camera.main.transform.up);

    }
    private void Fire()
    {
        Debug.Log("Fired");
        RaycastHit hit;
        if(Physics.Raycast(gunMuzzle.transform.position, gunMuzzle.transform.forward, out hit, 100))
        {
            Debug.Log("Hit");
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
    private void SpawnTargets()
    {
        if (targets.Count < 0)
        {
            AddTargetToPool();
            return;
        }
        GameObject target = targets.Dequeue();
        Vector3 spawnPosition = new Vector3(Random.Range(-targetSpawnRange, targetSpawnRange), ySpawnDistanceFromCamera, zSpawnDistanceFromCamera);
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
}
