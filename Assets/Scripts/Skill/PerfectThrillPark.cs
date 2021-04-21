using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectThrillPark : SkillBase
{
    public float[] c_dmg = { 1.2f, 1.2f, 1.3f, 1.47f, 1.56f, 1.64f, 1.73f, 1.82f, 1.91f, 2 };
    public float[] _range = { 2.5f, 2.67f, 2.83f, 3f, 3.17f, 3.33f, 3.5f, 3.67f, 3.83f, 4 };
    public bool skilling;
    public Sprite image;

    public override void SkillActive() {
        if (GetComponent<DollController>().Target == null)
            return;

        print("use skill : " + GetSkillName() + "    LV." + skilllevel);
        skill_cool_timer = GetCoolDown();
        skill_duration_timer = GetDuration();
        SoundManager.instance.PlaySound_Voice(GetComponent<SoundContainer>().SkillActive[Random.Range(0,3)]);
        SoundManager.instance.PlaySound_Sfx(GetComponent<SoundContainer>().SkillEffect);

        skilling = true;
    }

    public override void SkillDescribe() {
        base.SkillDescribe();

        skill_describe = skill_describe.Replace("_duration", skill_Duration[skilllevel].ToString());
        skill_describe = skill_describe.Replace("_c_dmg", c_dmg[skilllevel].ToString());
        skill_describe = skill_describe.Replace("_range", _range[skilllevel].ToString());
    }

    public override void Effect(GameObject _target) {
        GameObject buff = Instantiate(InGameManager.instance.buff);
        buff.GetComponent<Buff>().Initialized(GetSkillName(), GetDuration(), GetComponent<SkillBase>().skill_icon, gameObject, _target, 0, 0, 0, 0, 0, 0, 0, false, 0, true, GetDuration());
    }
}
