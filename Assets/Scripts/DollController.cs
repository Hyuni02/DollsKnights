using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffContainer))]
public class DollController : CharacterBase
{
    public Sprite Sprite_Doll;
    public Sprite Sprite_Doll_face;

    private void Awake() {
        if(Sprite_Doll == null || Sprite_Doll_face == null) {
            Debug.LogError(Name + "'s Sprite is null");
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void UpdateState() {

    }
}
