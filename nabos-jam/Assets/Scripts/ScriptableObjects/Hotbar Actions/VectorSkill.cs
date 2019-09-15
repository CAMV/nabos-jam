using UnityEngine;

public enum VectorSkillType 
{
    Direction, Vector, Area
}

[CreateAssetMenu(menuName = "Skills/Vector Skill")]
[RequireComponent(typeof(MeshRenderer))]

public class VectorSkill : Skill
{


    [SerializeField]
    protected Projectile _projectile;
    
    [SerializeField]
    protected VectorSkillType _type = VectorSkillType.Vector;
    
    [Range(0, 10)]
    [SerializeField]
    protected int _range;

    protected Vector3 _targetPosition;
    
    public int Range {
        get {
            return _range;
        }
    }
    
    /// <summary>
    /// Checks if the conditions for the skill to be casted are met.
    /// </summary>
    override protected bool CheckPreCondition()
    {
        return true;
    }

    override public void Execute(bool isQuickCast)
    {
        string qcMsg = isQuickCast ? "quick-casted " : "casted";
        Debug.Log(_originUnit.name + " " + qcMsg  + _name + "!");

        if (isQuickCast)
        {

            // Get raycast point
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane newPlane = new Plane(Vector3.up, _originUnit.transform.position);
            float rayPoint;
            newPlane.Raycast(ray, out rayPoint);
            
            _targetPosition = ray.GetPoint(rayPoint);

            CastSkillObject();
        }
        else
        {
            _originUnit.Skills.ExecuteSkillInput(this, _type);
            
        }
    }

    private void CastSkillObject()
    {

        Ray throwRay = new Ray(_originUnit.transform.position, _targetPosition);    

        float distanceToThrow = Vector3.Distance(_originUnit.transform.position, _targetPosition);
        distanceToThrow = distanceToThrow > _range ? _range : distanceToThrow;

        

        Projectile currentP = GameObject.Instantiate(
                        _projectile, 
                        _originUnit.transform.position + Vector3.up*1.5f + (_targetPosition - _originUnit.transform.position).normalized, 
                        Quaternion.LookRotation(_targetPosition - _originUnit.transform.position, Vector3.up)
                    );

        currentP.Initialize(_originUnit, throwRay.GetPoint(Mathf.Max(1, distanceToThrow)));
    }



}