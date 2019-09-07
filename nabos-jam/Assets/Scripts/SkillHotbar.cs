using UnityEngine;

public class SkillHotbar : MonoBehaviour
{
    [SerializeField]
    private Skill[] _slots = new Skill[0];

    public Skill GetSkill(int index)
    {
        if (index < _slots.Length)
            return _slots[index];
        
        return null;
    }

    public void SetSkill(int index, Skill skill)
    {
        if (index < _slots.Length)
            _slots[index] = skill;
    }

}