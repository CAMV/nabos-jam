using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTargetGUI : MonoBehaviour
{
    [SerializeField]
    GameObject[] _tgGizmos = new GameObject[0];

    Animator[] _gizmoAnimators;

    void Start()
    {

        _gizmoAnimators = new Animator[_tgGizmos.Length];

        for (int i = 0; i < _tgGizmos.Length; i++)
        {
            _gizmoAnimators[i] = _tgGizmos[i].GetComponent<Animator>();    
        }
        
    }

    public void Show(List<Vector3> pTargets, List<Quaternion> rTargets)
    {
        for (int i = 0; i < pTargets.Count; i++)
        {
            if (i < _gizmoAnimators.Length)
            {
                _tgGizmos[i].transform.position = pTargets[i];
                _tgGizmos[i].transform.rotation = rTargets[i];
                _gizmoAnimators[i].PlayInFixedTime("Hide", 0, 0);
            }
        }

        StartCoroutine(ShowCO(pTargets.Count)); 
    }

    IEnumerator ShowCO(int nTargets)
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < nTargets; i++)
        {
            if (i < _gizmoAnimators.Length)
            {
                _gizmoAnimators[i].Play("Move", 0);
            }
        } 
    }

}