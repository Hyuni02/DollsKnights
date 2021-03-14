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

    [SerializeField]
    float Timer_attack;

    public List<UnityEngine.Transform> RouteToMove;

    [HideInInspector]
    public UnityArmatureComponent uac;
    [HideInInspector]
    public FinalState fs;

    public virtual void Start() {
        uac = GetComponentInChildren<UnityArmatureComponent>();
        fs = GetComponent<FinalState>();
        Check_Node_Stand();

        Timer_attack = 1;
    }

    public virtual void Update() {
        SetState();
        UpdateState();

        if (Timer_attack > 0)
            Timer_attack -= Time.deltaTime;
    }

    public void SetRoute(List<UnityEngine.Transform> route) {
        RouteToMove = new List<UnityEngine.Transform>();
        for(int i = route.Count-1; i > 0; i--) {
            RouteToMove.Add(route[i]);
        }
    }

    public virtual void SetState() {
        if (fs.hp <= 0)
            state = State.die;

        if (uac.animation.isCompleted)
            now_animation = null;

        if (Timer_attack <= 0)
            attacking = false;
        
        if (attacking)
            return;

        switch (state) {
            case State.attack:
                if (Timer_attack <= 0) {
                    attacking = true;
                    float t = 1 / (fs.rateoffire * 0.02f);
                    PlayAnimation(state.ToString(), t, 1);
                    Timer_attack = t;
                }
                break;
            case State.die:
                Invoke("die", 0.7f);
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

    void PlayAnimation(string anim, float timescale = 1, int playtime = 0) {
       if (state.ToString().Equals(now_animation))
            return;

        uac.animation.timeScale = Mathf.Max(timescale, 1);
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
    public virtual void die() {
        gameObject.SetActive(false);
    }

    void Check_Node_Stand() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0,0.15f,0), Vector3.down * 0.2f, out hit)) {
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

    public float GetDistance(GameObject target) {
        float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        return distance;
    }
}