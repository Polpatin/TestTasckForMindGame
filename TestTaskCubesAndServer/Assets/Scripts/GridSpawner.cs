using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] GameObject CubePrefab;
    [SerializeField] float cubeScale = 0.3f;
    [SerializeField] float spacingBetwenCubes = 0.1f;
    [SerializeField] float CubesYLevel = 0.1f;
    public void SpawnCubes(List<int> materialsNumbers, Vector3 gridPosition)
    {
      
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        Vector3[] cubesPositions = GridPositions(cubeScale, spacingBetwenCubes, CubesYLevel, gridPosition);
        Vector3 SpavnPos = gridPosition + Vector3.up * 20f;
        StartCoroutine(SpawnCubes());

        IEnumerator SpawnCubes()
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject cube = PhotonNetwork.Instantiate(CubePrefab.name, SpavnPos, Quaternion.identity);
                cube.transform.localScale = new Vector3(cubeScale, cubeScale, cubeScale);

                cube.GetComponent<Box>().ChangeMaterial(materialsNumbers[i]);

                for (float f = 0f; f < 1.1f; f += 0.1f)
                {
                   
                   
                    cube.transform.position = Vector3.Lerp(SpavnPos, cubesPositions[i], f);
                    yield return new WaitForFixedUpdate();
                }
                cube.transform.position = cubesPositions[i];

            }


            yield return null;
        }






        Vector3[] GridPositions(float cubeSize, float spacing, float yLevel, Vector3 gridPos)
        {
            List<Vector3> positions = new List<Vector3>();

            for (int z = 1; z > -2; z--)
            {
                for (int x = -1; x < 2; x++)
                {
                    positions.Add(new Vector3((cubeSize + spacing) * x, yLevel, (cubeSize + spacing) * z)+ gridPos);
                }
            }

            return positions.ToArray();
        }
    }
}
