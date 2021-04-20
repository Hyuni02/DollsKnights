using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowSkill : SkillBase
{
    public int[] hit_time = { 3, 3, 3, 3, 4, 4, 4, 5, 5, 5 };
    public bool skilling = false;

    public override void SkillActive() {
        throw new System.NotImplementedException();
    }
}
