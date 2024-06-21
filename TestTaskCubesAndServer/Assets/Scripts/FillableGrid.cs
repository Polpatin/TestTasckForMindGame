using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillableGrid : MonoBehaviour
{
    [SerializeField] Vector3 StartPos;
    [SerializeField] Vector3 IntermediatePos;
    [SerializeField] Vector3 EndPos;
    public void ShowGrid()
    {
        StartCoroutine(PopupGrid());
        IEnumerator PopupGrid()
        {
            for (float i = 0; i < 1.1f; i += 0.1f)
            {
                transform.localPosition=Vector3.Lerp(StartPos, IntermediatePos, i);
                yield return new WaitForFixedUpdate();
            }
            for (float i = 0; i < 1.1f; i += 0.1f)
            {
                transform.localPosition = Vector3.Lerp(IntermediatePos, EndPos, i);
                yield return new WaitForFixedUpdate();
            }
        }
    }

   
}
