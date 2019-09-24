using UnityEngine;

public enum VectorSkillType 
{
    Direction, Vector, Area, Unit
}

[CreateAssetMenu(menuName = "Skills/Vector Skill")]
/// <summary>
/// Represents all skills that requieres a vector3 to be executed.
/// </summary>
public class VectorSkill : Skill
{   
    [SerializeField]
    protected VectorSkillType _type = VectorSkillType.Vector;

    protected Vector3 _inputVector;

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
            Plane newPlane = new Plane(Vector3.up, _originUnit.transform.position);
            float rayPoint;
            newPlane.Raycast(ray, out rayPoint);
            
            _inputVector = ray.GetPoint(rayPoint);

            Cast(_inputVector);
        }
        else
        {
            _originUnit.Skills.GetSkillInput(this, _type);      
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
            InitializeActionObjects(i, _inputVector);
        }
    }

    /// <summary>
    /// Cast the skill with a given vector position as a reference.
    /// </summary>
    /// <param name="input"></param>
    public void Cast(Vector3 input)
    {
        _inputVector = input;
        Cast();
    }
}