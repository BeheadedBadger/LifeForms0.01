using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] List<Cell> neighbouringCells = new();
    [SerializeField] public GameObject prefab;
    [SerializeField] public MeshRenderer meshRenderer;
    [SerializeField] public Material mat;
    [SerializeField] public Material primary;
    [SerializeField] public Material secondary;
    [SerializeField] public Animator animator;
    public GenerateGrid grid;
    public int xCo;
    public int zCo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        prefab.transform.position = new Vector3(prefab.transform.position.x, 0.1f, prefab.transform.position.z);
        meshRenderer.material = primary;
        //animator.Play("Activate");
        foreach (var cell in neighbouringCells)
        {
            //cell.animator.Play("Secondary");
            cell.prefab.transform.position = new Vector3(cell.prefab.transform.position.x, 0.05f, cell.prefab.transform.position.z);
            cell.meshRenderer.material = secondary;
        }
    }
    private void OnMouseExit()
    {
        prefab.transform.position = new Vector3(prefab.transform.position.x, 0, prefab.transform.position.z);
        meshRenderer.material = mat;
        //animator.Play("Idle");
        foreach (Cell cell in neighbouringCells)
        {
            cell.prefab.transform.position = new Vector3(cell.prefab.transform.position.x, 0, cell.prefab.transform.position.z);
            //cell.animator.Play("Idle");
            cell.meshRenderer.material = mat;
        }
    }

    public void GetNeighbours()
    {
        List<Tuple<int, int>> neighbourCos = new()
        {
            Tuple.Create(xCo - 1, zCo - 1),
            Tuple.Create(xCo - 1, zCo + 1),
            Tuple.Create(xCo - 1, zCo),
            Tuple.Create(xCo + 1, zCo - 1),
            Tuple.Create(xCo + 1, zCo + 1),
            Tuple.Create(xCo + 1, zCo),
            Tuple.Create(xCo, zCo - 1),
            Tuple.Create(xCo, zCo + 1)
        };

        foreach (Tuple<int, int> key in neighbourCos)
        {
            if (grid.grid.ContainsKey(key))
            {
                neighbouringCells.Add(grid.grid[key]);
            }
        }
    }
}