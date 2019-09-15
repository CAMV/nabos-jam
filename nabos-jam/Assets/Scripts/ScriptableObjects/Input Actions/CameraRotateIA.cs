using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that handles a zoom input
/// </summary>
[CreateAssetMenu(menuName = "Input Settings/Camera Rotate Input")]
public class CameraRotateIA : InputAction
{
    public override bool CheckInput()
    {
        float cameraScrollAmount = Input.GetAxis(_buttomName);
        return cameraScrollAmount != 0f;
    }
    public override IEnumerator ExecuteAction() 
    {
        if (CameraController.Instance != null)
        {
            //Debug.Log(Input.GetAxis(_buttomName));
            CameraController.Instance.UpdateRotation(-Input.GetAxis(_buttomName));
        }
        yield return null;
    }
}
