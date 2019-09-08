using UnityEngine;

/// <summary>
/// Settings for the Select Gizmo component
/// </summary>
[CreateAssetMenu(menuName = "Gizmo Settings/Select Gizmo Setting")]
public class SelectGizmoSetting : ScriptableObject
{
    public Material hightIntensityMat, mediumIntensityMat, lowIntensityMat;
}