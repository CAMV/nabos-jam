using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class FormationVisualizer : MonoBehaviour
{
    public Formation formation;
    [Range(1, 8)]
    public int unitsToUse;
    public GameObject[] unitPlaceholders;

    

    void Update()
    {
        int limit = Mathf.Min(unitsToUse, formation.Size);

        for(int i = 0; i < unitPlaceholders.Length; i++)
        {
            if (i < limit)
            {
                unitPlaceholders[i].SetActive(true);
                unitPlaceholders[i].transform.position = transform.localToWorldMatrix.MultiplyPoint(formation.GetPosOffset(i));
                unitPlaceholders[i].transform.eulerAngles = transform.localToWorldMatrix.MultiplyVector(transform.eulerAngles);
                //unitPlaceholders[i].transform.Rotate(new Vector3(0, formation.GetEAOffset(i), 0), Space.Self);
            }
            else
            {
                unitPlaceholders[i].SetActive(false);
            }
            
        }


    }
}
