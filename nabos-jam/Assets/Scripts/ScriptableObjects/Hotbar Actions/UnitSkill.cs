using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Unitskill : BasicSkill
{
    protected Unit _unitInput;

    /// <summary>
    /// Exceute function for hotbar action
    /// </summary>
    override public void Execute(bool isQuickCast)
    {
        base.Execute(isQuickCast);

        if (isQuickCast)
        {

            // Get raycast point
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
                return;

            _unitInput = hit.collider.gameObject.GetComponentInParent<Unit>();

            if (!_unitInput)
                return;

            Cast();
        }
        else
        {
            _originUnit.Skills.GetSkillInput(this, VectorSkillType.Unit);      
        }
    }

    /// <summary>
    /// Cast the skill
    /// </summary>
    override public void Cast()
    {
        base.Cast();
        for (int i = 0; i < _actionObjects.Length; i++)
        {
            InitializeActionObjects(i, _unitInput.transform.position);
        }
    }
}