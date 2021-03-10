using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;

public abstract class CharacterBase : MonoBehaviour {
    public enum State { wait, move, attack, die, victory, victoryloop}
    public State state;
    //[HideInInspector]
    public string Name, now_animation;
    public GameObject Node_StandOn;
    [HideInInspector]
    public bool attacking = false;
    public bool placed = false;

    public List<UnityEngine.Transform> RouteToMove;

    UnityArmatureComponent uac;
    FinalState fs;

    public virtual void Start() {
        uac = GetComponentInChildren<UnityArmatureComponent>();
        fs = GetComponent<FinalState>();
    }

    public virtual void Update() {
        SetState();
        UpdateState();
    }

    public void SetRoute(List<UnityEngine.Transform> route) {
        RouteToMove = new List<UnityEngine.Transform>();
        for(int i = route.Count-1; i > 0; i--) {
            RouteToMove.Add(route[i]);
        }
    }

    public virtual void SetState() {
        switch (state) {
            case State.attack:

                PlayAnimation(state.ToString(), 1 / fs.rateoffire, 1);
                break;
            case State.die:

                PlayAnimation(state.ToString(), 1, 1);
                break;
            case State.move:
                move();
                PlayAnimation(state.ToString());
                break;
            case State.wait:

                PlayAnimation(state.ToString());
                break;
            case State.victory:
                PlayAnimation(state.ToString(), 1, 1);
                break;
            case State.victoryloop:
                PlayAnimation(state.ToString());
                break;
        }
    }

    void PlayAnimation(string anim, float timescale = 1, int playtime = -1) {
        if (state.ToString().Equals(now_animation))
            return;
        
        uac.animation.timeScale = timescale;
        now_animation = anim;
        uac.animation.Play(anim, playtime);
    }

    public virtual void move() {
        if (RouteToMove.Count > 0) {
            int i = RouteToMove.Count - 1;
            float dif_x = RouteToMove[i].transform.position.x - transform.position.x;
            float dif_z = RouteToMove[i].transform.position.z - transform.position.z;
            SetFaceDir(RouteToMove[i].transform.position.x);

            Vector3 dir = new Vector3(dif_x, 0, dif_z);
            if (dir.magnitude <= 0.02f) {
                transform.position = new Vector3(RouteToMove[i].transform.position.x, transform.position.y, RouteToMove[i].transform.position.z);
                RouteToMove.RemoveAt(i);
                //Check_Node_Stand();
            }
            else {
                transform.Translate(dir.normalized * fs.speed * 0.03f * Time.deltaTime, Space.World);
            }
        }
    }

    void Check_Node_Stand() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down * 0.2f, out hit)) {
            if (hit.collider.GetComponent<NodeInfo>()) {
                Node_StandOn = hit.collider.gameObject;
            }
        }
    }

    void SetFaceDir(float Target_PosX) {
        if (Target_PosX > transform.position.x)
            uac.armature.flipX = false;
        else if (Target_PosX == transform.position.x)
            return;
        else
            uac.armature.flipX = true;
    }

    public abstract void UpdateState();
}