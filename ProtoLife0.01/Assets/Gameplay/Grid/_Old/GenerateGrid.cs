using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField] int mapSize = 50;
    [SerializeField] GameObject GridCell;
    public Dictionary<Tuple<int, int>, Cell> grid = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                GameObject prefab = Instantiate(GridCell, this.transform);
                Cell cell = prefab.GetComponent<Cell>();
                cell.transform.position = new Vector3(x, 0, z);
                grid.Add(Tuple.Create(x, z), cell);
                cell.grid = this;
                cell.xCo = x;
                cell.zCo = z;
            }
        }

        foreach (Cell cell in grid.Values)
        { 
            //Determine neighbours of each cell
            cell.GetNeighbours();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
