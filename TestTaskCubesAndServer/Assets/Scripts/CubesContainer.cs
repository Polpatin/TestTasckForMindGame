using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesContainer : MonoBehaviour
{ 
    public void FillCubesContainer()
    {
        var exampleList = GameObject.Find("ExampleGrid").GetComponent<ExampleGrid>().RandomMaterialsNumList;     

        GetComponent<GridSpawner>().SpawnCubes(ShuffleList(exampleList), transform.position);


    }


    public  List<int> ShuffleList(List<int> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }
}
