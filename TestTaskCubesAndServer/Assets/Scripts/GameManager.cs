using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject exampleGrid;
    [SerializeField] GameObject cubesContainer;
    [SerializeField] GameObject FillableGrid;
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] Vector3 PlayerStartPosition;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject RestartButton;
    [SerializeField] TextMeshProUGUI MainText;
    void Start()
    {
        StartCoroutine(BuildScene());
    }
 
    private IEnumerator BuildScene()
    {
        if (PlayerPrefs.GetString("IsServerPlayer") == "false")
        {
            PlayerStartPosition = new Vector3(PlayerStartPosition.x * -1, PlayerStartPosition.y, PlayerStartPosition.z);
            PhotonNetwork.Instantiate(PlayerPrefab.name, PlayerStartPosition, Quaternion.identity);
            FillableGrid.GetComponent<FillableGrid>().ShowGrid();
            yield break;
        }

        exampleGrid.GetComponent<ExampleGrid>().FillExampleGrid();

       

        yield return new WaitForSeconds(2f);

        cubesContainer.GetComponent<CubesContainer>().FillCubesContainer();


        yield return new WaitForSeconds(2f);

        FillableGrid.GetComponent<FillableGrid>().ShowGrid();
       
        PhotonNetwork.Instantiate(PlayerPrefab.name, PlayerStartPosition, Quaternion.identity);
    }

   
}
