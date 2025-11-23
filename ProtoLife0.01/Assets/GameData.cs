using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] public Dictionary<Vector3Int, Cell> cellDatabase = new();
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Cell
{
    public Cell(Vector3Int coordinates, int height, int humidity, int? mycelium, int nutrients, int? lifeform, bool corrupted)
    {
        Coordinates = coordinates;
        this.height = height;
        this.humidity = humidity;
        this.mycelium = mycelium;
        this.nutrients = nutrients;
        this.lifeform = lifeform;
        this.corrupted = corrupted;
    }

    public Vector3Int Coordinates { get; set; }
    public int? height { get; set; }
    public int? humidity { get; set; }
    public int? mycelium { get; set; }
    public int nutrients { get; set; }
    //Maybe a ID for each lifeform?
    public int? lifeform { get; set; }
    bool corrupted { get; set; }
}