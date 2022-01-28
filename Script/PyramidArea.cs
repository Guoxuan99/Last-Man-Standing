using UnityEngine;
using Unity.MLAgentsExamples;

public class PyramidArea : Area
{
    public GameObject key;
    public GameObject stonePyramid;
    public GameObject[] spawnAreas;
    //public int numPyra;
    public float range;

    // Create Key Randomly in Map
    public void CreatePyramid(int numObjects, int spawnAreaIndex)
    {
        CreateObject(numObjects, key, spawnAreaIndex);
    }

    // Create object randomly in specific area
    void CreateObject(int numObjects, GameObject desiredObject, int spawnAreaIndex)
    {
        for (var i = 0; i < numObjects; i++)
        {
            var newObject = Instantiate(desiredObject, Vector3.zero,
                Quaternion.Euler(0f, 0f, 0f), transform);
            PlaceObject(newObject, spawnAreaIndex);
        }
    }

    // Place object randomly in Game Area
    public void PlaceObject(GameObject objectToPlace, int spawnAreaIndex)
    {
        var spawnTransform = spawnAreas[spawnAreaIndex].transform;
        var xRange = spawnTransform.localScale.x / 2.1f;
        var zRange = spawnTransform.localScale.z / 2.1f;

        objectToPlace.transform.position = new Vector3(Random.Range(-xRange, xRange), 2f, Random.Range(-zRange, zRange))
            + spawnTransform.position;
    }

    public override void ResetArea()
    {
    }
}
