using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] int worldSize;
    [SerializeField] GameObject basePlane;
    [SerializeField] GameObject water;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] List<GameObject> tiles = new();

    [SerializeField] float borderSize = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Generate();
        }
    }

    void Generate()
    {   
        ClearAll();
        basePlane.transform.localScale = new Vector3(worldSize / 8, 1, worldSize / 8);
        basePlane.transform.position = new Vector3(worldSize / 2, 0, worldSize / 2);
        water.transform.localScale = new Vector3(worldSize + 30, water.transform.localScale.y, worldSize + 30);
        water.transform.position = new Vector3(worldSize / 2, water.transform.position.y, worldSize / 2);

        List<float> values = new();

        float randomOffset = Random.Range(0, 10000);
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                float noiseCoordinates = Mathf.PerlinNoise((float)x / 50 + randomOffset, (float)y / 50 + randomOffset);
                //Lower towards edge and heighten towards center, in a circle pattern.
                borderSize = worldSize / 4;
                float center = worldSize / 2;
                float radius = center - borderSize;
                var dx = center - x;
                var dy = center - y;

                float distance_squared = dx * dx + dy * dy;

                //Convert to a value between 0 and 1
                float edgeDetectionValue = distance_squared / (radius * radius); 
                //Lower towards edge, heighten towards center
                noiseCoordinates = noiseCoordinates * 2 - (edgeDetectionValue/2) + 0.5f / 2;

                //noiseCoordinates are now a number between +- 0 and 1. Generate tiles.
                if (noiseCoordinates >= 0)
                {
                    GameObject tile = Instantiate(tilePrefab, this.transform);
                    tile.transform.position = new Vector3(x, 0, y);
                    tiles.Add(tile);
                }
                if (noiseCoordinates >= 0.3)
                {
                    GameObject tile = Instantiate(tilePrefab, this.transform);
                    tile.transform.position = new Vector3(x, 1, y);
                    tiles.Add(tile);
                }
                if (noiseCoordinates >= 0.6)
                {
                    GameObject tile = Instantiate(tilePrefab, this.transform);
                    tile.transform.position = new Vector3(x, 2, y);
                    tiles.Add(tile);
                }
                if (noiseCoordinates >= 1)
                {
                    GameObject tile = Instantiate(tilePrefab, this.transform);
                    tile.transform.position = new Vector3(x, 3, y);
                    tiles.Add(tile);
                }
                values.Add(noiseCoordinates);
            }
        }
        Debug.Log($"{values.Min(x => x)} {values.Max(x => x)}");
    }

    private void ClearAll()
    {
        if (tiles != null && tiles.Count > 0)
        {
            foreach (var tile in tiles)
            {
                Destroy(tile);
            }
        }
    }
}
