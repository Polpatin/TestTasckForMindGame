using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGrid : MonoBehaviour
{
    private List<int> randomMaterialsNumList=new List<int>();
    public List<int> RandomMaterialsNumList { get { return randomMaterialsNumList; } }
    public void FillExampleGrid()
    {
       // List<int> randomMaterialsNumList = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            randomMaterialsNumList.Add(Random.Range(0, 2));
        }
        GetComponent<GridSpawner>().SpawnCubes(randomMaterialsNumList, transform.position);
    }
}
