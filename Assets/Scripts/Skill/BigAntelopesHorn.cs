using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAntelopesHorn : SkillBase
{
    public float[] increase_dmg1 = { 0.8f, 0.9f, 1, 1.1f, 1.2f, 1.3f, 1.4f, 1.4f, 1.5f, 1.5f };
    public float[] increase_dmg2 = { 1.2f, 1.3f, 1.5f, 1.6f, 1.8f, 1.9f, 2.1f, 2.2f, 2.4f, 2.5f };
    public float[] increase_dmg3 = { 1.6f, 1.7f, 2, 2.1f, 2.4f, 2.5f, 2f, 8.3f, 3.3f, 3.5f };

    public override void SkillActive() {
        if (GetComponent<SaigaController>().skilling || GetComponent<SaigaController>().List_temp_target.Count == 0)
            return;

        print("use skill : " + GetSkillName() + "    LV." + skilllevel);
        //배치된 모든 인형들에게 버프 부여
        skill_cool_timer = GetCoolDown();
        skill_duration_timer = GetDuration();

        GetComponent<SaigaController>().skilling = true;
        GetComponent<SaigaController>().skillcount = 0;
    }

    public override void SkillDescribe() {
        base.SkillDescribe();

        skill_describe = skill_describe.Replace("_dmg1", increase_dmg1[skilllevel].ToString());
        skill_describe = skill_describe.Replace("_dmg2", increase_dmg2[skilllevel].ToString());
        skill_describe = skill_describe.Replace("_dmg3", increase_dmg3[skilllevel].ToString());
    }
}
