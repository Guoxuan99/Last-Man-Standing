using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    public GameObject key;
    public GameObject syringeGameObject;
    public GameObject[] spawnAreas;
    public GameObject vaccine_count_panel;
    public GameObject text_5sec;
    int totalSyringeCount = 6;
    int remainingSyringeCount;
    bool generatedKey = false;
    string message;

    // Start is called before the first frame update
    void Start()
    {

        // Generate syringe randomly in the Game Area
        var enumerable = Enumerable.Range(0, 9).OrderBy(x => Guid.NewGuid()).Take(9);
        var items = enumerable.ToArray();
        foreach (var item in items)
        {
            Debug.Log(item);
        }

        for (int i = totalSyringeCount, j = 0; i > 0; i--,  j++)
        {
            CreateObject(1, syringeGameObject, items[j]);
            String s = "Created obj " + totalSyringeCount.ToString() + " at " + items[j].ToString();
            Debug.Log(s);

            if (j == 8)
            {
                j = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        remainingSyringeCount = GameObject.FindGameObjectsWithTag("goal").Length;
        if (remainingSyringeCount == 0 && !generatedKey)
        {
            // Create Key at Random Spawn Area when all syring are found
            CreateKey(1, Random.Range(0, 9));
            Debug.Log("All Syringe are collected, Generate Key");
            generatedKey = true;
            message = "All vaccines collected. Find the key to escape!!";
            StartCoroutine("WaitForSec", message);
        }
        //Debug.Log("Syringe Count= "+ remainingSyringeCount.ToString());

        if(remainingSyringeCount == 0)
        {
            message =  (totalSyringeCount - remainingSyringeCount).ToString() + "/" + totalSyringeCount.ToString() + "(Done)";
        }
        else
        {
            message =  (totalSyringeCount - remainingSyringeCount).ToString() + "/" + totalSyringeCount.ToString();
        }
        vaccine_count_panel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = message;
    }

    public int GetRemainingSyringeCount()
    {
        return remainingSyringeCount;
    }

    public int GetTotalSyringeCount()
    {
        return totalSyringeCount;
    }

    // Create Key Randomly in Map
    public void CreateKey(int numObjects, int spawnAreaIndex)
    {
        CreateObject(numObjects, key, spawnAreaIndex);
    }

    void CreateObject(int numObjects, GameObject desiredObject, int spawnAreaIndex)
    {
        for (var i = 0; i < numObjects; i++)
        {
            var newObject = Instantiate(desiredObject, Vector3.zero,
                Quaternion.Euler(0f, 0f, 0f), transform);
            PlaceObject(newObject, spawnAreaIndex);
            Debug.Log("CreateObject");
        }
    }

    public void PlaceObject(GameObject objectToPlace, int spawnAreaIndex)
    {
        var spawnTransform = spawnAreas[spawnAreaIndex].transform;
        var xRange = spawnTransform.localScale.x / 2.1f;
        var zRange = spawnTransform.localScale.z / 2.1f;

        objectToPlace.transform.position = new Vector3(Random.Range(-xRange, xRange), 2f, Random.Range(-zRange, zRange))
            + spawnTransform.position;

        Debug.Log("PlaceObject");
    }

    IEnumerator WaitForSec(string message)
    {
        text_5sec.SetActive(true);
        text_5sec.GetComponent<TMPro.TextMeshProUGUI>().text = message;
        Debug.Log("waiting");
        yield return new WaitForSeconds(3);
        text_5sec.SetActive(false);
    }
}
