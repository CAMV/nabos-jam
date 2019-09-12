using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that handles a zoom input
/// </summary>
[CreateAssetMenu(menuName = "Input Settings/Camera Input")]
public class CameraZoomIA : InputAction
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
            Debug.Log(Input.GetAxis(_buttomName));
            CameraController.Instance.UpdateZoom(-Input.GetAxis(_buttomName));
        }
        yield return null;
    }
}
