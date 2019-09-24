using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class <c> MoveTargetGUI </c> handles the feedback when a movement begins executing.
/// </summary>
public class MovementFeedbackGizmo : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _tgGizmos = new GameObject[0];

    private Animator[] _gizmoAnimators;

    void Awake()
    {
        _gizmoAnimators = new Animator[_tgGizmos.Length];

        for (int i = 0; i < _tgGizmos.Length; i++)
        {
            _gizmoAnimators[i] = _tgGizmos[i].GetComponent<Animator>();    
        }

        Hide();
    }

    /// <summary>
    /// Shows the feedback of the movement given the target positions and rotations of the units being moved.
    /// </summary>
    /// <param name="pTargets">Target positions of units being moved.</param>
    /// <param name="rTargets">Target rotations of units being moved.</param>
    public void Show(List<Vector3> pTargets, List<Quaternion> rTargets)
    {
        Hide();
        // Reset the animation of the gizmos first
        for (int i = 0; i < pTargets.Count; i++)
        {
            _tgGizmos[i].transform.position = pTargets[i];
            _tgGizmos[i].transform.rotation = rTargets[i];
        }

        // Execute the animation in the next frame
        StartCoroutine(ShowCO(pTargets.Count)); 
    }

    /// <summary>
    /// Corutine to handle the execution of the animations of the gizmos a frame after being called. 
    /// </summary>
    /// <param name="nTargets"></param>
    /// <returns></returns>
    private IEnumerator ShowCO(int nTargets)
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

    /// <summary>
    /// Hides the Gizmos
    /// </summary>
    public void Hide()
    {
        for (int i = 0; i < _gizmoAnimators.Length; i++)
            _gizmoAnimators[i].PlayInFixedTime("Hide", 0, 0);
    }
}