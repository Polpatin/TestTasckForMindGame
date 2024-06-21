using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        Loaded,
        Emty
    }

    [SerializeField] State playerSytate = State.Emty;

    IEnumerator MoweToPositionCoor;

    PhotonView photonView;
    [SerializeField]  Vector3 targetPosition;
    [SerializeField] GameObject targetBox;
    Vector3 mooveDirection;
    [SerializeField] GameObject targetPlace;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {

        
            if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

               
                if (clickedObject.GetComponent<Box>() != null && playerSytate == State.Emty)
                {
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    MoweToPositionCoor = GoToPosition();
                    targetBox = clickedObject;
                    StopCoroutine(MoweToPositionCoor);
                    StartCoroutine(MoweToPositionCoor);
                    return;
                }

                if (clickedObject.tag=="Place" && playerSytate == State.Loaded)
                {
                    targetPlace = clickedObject;
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    MoweToPositionCoor = GoToThePlace();
                    StopCoroutine(MoweToPositionCoor);
                    StartCoroutine(MoweToPositionCoor);
                    return;
                }

                if (clickedObject.GetComponent<Box>() == null)
                {
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    MoweToPositionCoor = GoToPosition();
                    StopCoroutine(MoweToPositionCoor);
                    StartCoroutine(MoweToPositionCoor);
                    return;
                }

            }
        }
            MooveBox();
        }

     

    }
   
    void MooveBox()
    {
        if (targetBox != null && playerSytate == State.Loaded)
        {
            targetBox.transform.rotation = gameObject.transform.rotation;
            targetBox.transform.position = gameObject.transform.position + Vector3.up * 2.2f;

        }
        if (targetBox != null && playerSytate == State.Loaded && !PhotonNetwork.IsMasterClient)
        {   
                targetBox.GetComponent<PhotonView>().RPC("FollowThePlayer", RpcTarget.All, transform.position + Vector3.up * 2.2f);
        }



    }


    private void PickUpBox(GameObject box)
    {
        targetBox= box;
        playerSytate = State.Loaded;
    }
    private void PutBoxDown(GameObject box, GameObject place)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            targetBox.GetComponent<PhotonView>().RPC("FollowThePlayer", RpcTarget.All, place.transform.position + Vector3.up * 2.4f);
        }
        box.transform.position = place.transform.position + Vector3.up * 2.4f;
        box.transform.rotation = place.transform.rotation;
        targetBox = null;
        playerSytate = State.Emty;
    }

    private IEnumerator GoToPosition()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(targetPosition.x, transform.localPosition.y, targetPosition.z);
        mooveDirection = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(mooveDirection);

        for (float i = 0f; i < 1.1f; i += 0.05f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, i);
            yield return new WaitForFixedUpdate();
        }
        transform.position = endPos;
        transform.rotation = targetRotation;

       yield return null;
    }

    private IEnumerator GoToThePlace()
    {
        Vector3 startPos = transform.position;
        Vector3 closesPlacePoint= targetPlace.GetComponent<BoxCollider>().ClosestPoint(transform.position);
        Vector3 endPos = new Vector3(closesPlacePoint.x, transform.localPosition.y, closesPlacePoint.z);
        mooveDirection = endPos - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(mooveDirection);

        for (float i = 0f; i < 1.1f; i += 0.05f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, i);
            yield return new WaitForFixedUpdate();
        }
        transform.position = endPos;
        transform.rotation = targetRotation;

        yield return null;
    }


    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.gameObject == targetBox && playerSytate==State.Emty)
        {
            PickUpBox(collision.gameObject);
            return;
        }
       StopAllCoroutines();

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Place" && playerSytate == State.Loaded && targetPlace == other.gameObject)
        {
            StopAllCoroutines();
            PutBoxDown(targetBox, other.gameObject);
        }
    }
   
   
}
