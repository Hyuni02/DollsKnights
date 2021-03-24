using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public enum Type { boost, action}

    public int level;
    public string skill_name;
    [TextArea]
    public string skill_describe_form;
    [TextArea]
    public string skill_describe;
    public Sprite skill_icon;
    public Type type;
    public float skill_InitCooltime;
    public float[] skill_Cooltime;
    public float[] skill_Duration;
    float timer = 0;
    public bool actoActive;

    public abstract void SkillActive();
    public virtual void SkillDescribe() {
        for (int i = 0; i < GetData.instance.List_DollData.Count; i++) {
            if (GetData.instance.List_DollData[i].name.Equals(transform.name.Replace("(Clone)", ""))) {
                level = GetData.instance.List_DollData[i].level;
                break;
            }
        }
    }
}
