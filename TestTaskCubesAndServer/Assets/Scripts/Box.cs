using Photon.Pun;

using UnityEngine;

public class Box : MonoBehaviourPun, IPunObservable
{
    [SerializeField] Material[] boxMaterialArr;

    public int MaterialNumber { get; set; }

   
    public void ChangeMaterial(int materialNumber)
    {
        MaterialNumber = materialNumber;

        PhotonView targetView = PhotonView.Find(GetComponent<PhotonView>().ViewID);
        targetView.gameObject.GetComponent<Box>().MaterialNumber = materialNumber;
    }


    private void Start()
    {
        GetComponent<MeshRenderer>().material = boxMaterialArr[MaterialNumber];
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonStream PS = new PhotonStream(true, null);
            PhotonMessageInfo PI = new PhotonMessageInfo();
            OnPhotonSerializeView(PS, PI);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting && PlayerPrefs.GetString("IsServerPlayer") == "true")
        {

            stream.SendNext(MaterialNumber);


        }

        else if (stream.IsReading && PlayerPrefs.GetString("IsServerPlayer") == "false")
        {

            GetComponent<MeshRenderer>().material = boxMaterialArr[(int)stream.ReceiveNext()];

        }
    }
    [PunRPC]
   public void FollowThePlayer(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

}
