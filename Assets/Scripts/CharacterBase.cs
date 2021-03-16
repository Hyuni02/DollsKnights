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
    public bool attackable;

    public  float Timer_attack;
    public GameObject Target;

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
        InvokeRepeating("SearchTarget", 0, 0.1f);
    }

    public virtual void Update() {
        SetState();
        UpdateState();

        if (Timer_attack > 0)
            Timer_attack -= Time.deltaTime;
    }

    public void SetRoute(List<UnityEngine.Transform> route) {
        RouteToMove = new List<UnityEngine.Transform>();
        for(int i = 0; i <route.Count; i++) {
            RouteToMove.Add(route[i]);
        }
        Node_StandOn = RouteToMove[RouteToMove.Count - 1].gameObject;
    }

    public virtual void SetState() {
        if (fs.hp <= 0)
            state = State.die;

        if (uac.animation.isCompleted) {
            now_animation = null;
            attacking = false;
        }

        if (attacking)
            return;

        switch (state) {
            case State.attack:
                if (Timer_attack <= 0 && Target != null) {
                    attacking = true;
                    attack();
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
                PlayAnimation(state.ToString(), 1, 1);
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

    public void PlayAnimation(string anim, float timescale = 1, int playtime = 0) {
       if (state.ToString().Equals(now_animation))
            return;

        uac.animation.timeScale = Mathf.Max(timescale, 1);
        now_animation = anim;
        uac.animation.Play(anim, playtime);
    }

    public virtual void attack() {
        //print(gameObject.name + " attacked " + Target.name);
        Target.GetComponent<CharacterBase>().GetAttacked(fs.damage, fs.accuracy, fs.critrate, fs.armorpen);
    }
    public virtual void move() {
        if (RouteToMove.Count > 0) {
            float dif_x = RouteToMove[0].transform.position.x - transform.position.x;
            float dif_z = RouteToMove[0].transform.position.z - transform.position.z;
            SetFaceDir(RouteToMove[0].transform.position.x);

            Vector3 dir = new Vector3(dif_x, 0, dif_z);
            if (dir.magnitude <= 0.02f) {
                transform.position = new Vector3(RouteToMove[0].transform.position.x, transform.position.y, RouteToMove[0].transform.position.z);
                RouteToMove.RemoveAt(0);
                //Check_Node_Stand();
            }
            else {
                transform.Translate(dir.normalized * fs.speed * 0.06f * Time.deltaTime, Space.World);
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

    public void SearchTarget() {
        if (!gameObject.activeSelf)
            return;

        attackable = false;
        //print("Check Target" + gameObject.name);
        if(GetComponent<DollController>() != null) {
            for(int i = 0; i < InGameManager.instance.Spawned_Enemies.Count; i++) {
                if (GetDistance(InGameManager.instance.Spawned_Enemies[i]) <= fs.range
                    && InGameManager.instance.Spawned_Enemies[i].activeSelf) {
                    attackable = true;
                    SetTarget(InGameManager.instance.Spawned_Enemies[i]);
                    return;
                }
                else {
                    SetTarget();
                    attackable = false;
                }
            }
        }
        else {
            for (int i = 0; i < InGameManager.instance.Spawned_Dolls.Count; i++) {
                if (GetDistance(InGameManager.instance.Spawned_Dolls[i]) <= fs.range
                     && InGameManager.instance.Spawned_Dolls[i].activeSelf) {
                    attackable = true;
                    SetTarget(InGameManager.instance.Spawned_Dolls[i]);
                    return;
                }
                else {
                    SetTarget();
                    attackable = false;
                }
            }
        }
    }

    public abstract void UpdateState();

    public float GetDistance(GameObject target) {
        float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        return distance;
    }
    public void SetTarget(GameObject target = null) {
        Target = target;
    }
    public void GetAttacked(int dmg, int acc, float critrate = 0, int armorpen = 0) {
        //회피-명중 계산
        //-명중 시
        if (acc > Random.Range(0, acc + fs.evasion)) {
            //장갑 판정
            if (armorpen < fs.armor) {
                fs.hp -= 1;
                ViewDmgIndicator(1, false, true);
                return;
            }

            //치명타 판정
            if(critrate > Random.Range(0, 1)) {
                int critdmg = (int)(dmg * 1.5f);
                fs.hp -= critdmg;
                ViewDmgIndicator(critdmg, true);
                return;
            }

            fs.hp -= dmg;
            ViewDmgIndicator(dmg);
        }
        //-회피 시
        else {
            ViewDmgIndicator(0, false, false, true);
        }
    }
    void ViewDmgIndicator(int dmg, bool crit = false, bool blocked = false, bool eva = false) {

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fs.range);
    }
}