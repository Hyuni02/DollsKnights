using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffContainer))]
public class DollController : CharacterBase
{
    public Sprite Sprite_Doll;
    public Sprite Sprite_Doll_face;

    public List<GameObject> Blocked_Enemies;

    private void Awake() {
        if(Sprite_Doll == null || Sprite_Doll_face == null) {
            Debug.LogError(Name + "'s Sprite is null");
        }
    }

    public override void UpdateState() {

        Blocking();
    }

    void Blocking() {
        if (fs.block > Blocked_Enemies.Count) {
            for (int i = 0; i < InGameManager.instance.Spawned_Enemies.Count; i++) {
                if (GetDistance(InGameManager.instance.Spawned_Enemies[i]) < 0.3f) {
                    InGameManager.instance.Spawned_Enemies[i].GetComponent<EnemyController>().Blocker = gameObject;
                    Blocked_Enemies.Add(InGameManager.instance.Spawned_Enemies[i]);
                }
            }
        }
    }
}
