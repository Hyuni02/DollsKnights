using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    public List<GameObject> Spawned_Dolls;
    public List<GameObject> Spawned_Enemies;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        GameObject Map = Instantiate(LevelContainer.instance.Levels[GameManager.instance.Index_SelectedLevel]);
        
        //선택한 제대 생성

        //스포닝 풀 생성
    }

}
