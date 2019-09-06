using UnityEngine;
using System.Collections.Generic;

public class MovementPreview : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _unitGizmos;

    public void TurnOn()
    {
        for (int i = 0; i < GameManager.Instance.PlayerSquad.ActiveUnits.Count; i++)
        {
            _unitGizmos[i].SetActive(true);
        }
    }

    public void TurnOff()
    {
        foreach (GameObject gizmo in _unitGizmos)
        {
            gizmo.SetActive(false);
        }
    }

    public void SetValues(List<Vector3> positions, List<Quaternion> rotations)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            _unitGizmos[i].transform.position = positions[i];
            _unitGizmos[i].transform.rotation = rotations[i];
        }
    }

}