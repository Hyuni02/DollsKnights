using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Route {
    public List<Transform> routeNodes;
}
[System.Serializable]
public class Wave {
    public float Delay_start;
    public float Interval_spawn;
    public int Index_enemy;
    public int Count_spawn;
    public int Index_route;
}

public class LevelInfo : MonoBehaviour
{
    public int index_level;
    public string title;
    public Vector2 MapSize;
    public int enemyLevel;

    //waves
    //rewards
    public int max_echlon_count;
    public int life;

    [SerializeField]
    private GameObject Default_Node;
    public List<GameObject> Nodes;
    [SerializeField]
    public Route[] routes;

    [Header("Wave Info")]
    [SerializeField]
    public bool WaveEnd = false;
    bool playingWave = false;
    public Wave[] waves;
    int now_wave = -1;
    List<Transform> SelectedRoute;

    void Start()
    {
        Invoke("StartWave", 3f);
    }


    void Update()
    {
        //if (WaveEnd && CheckFinish()) {
        //    InGameManager.instance.Open_VictoryPanel();
        //}
    }

    void StartWave() {
        if (now_wave < waves.Length - 1) {
            print("Wave " + (now_wave + 1));
            now_wave++;
            StartCoroutine(MakeWave(waves[now_wave]));
        }
        else {
            WaveEnd = true;
        }
    }
    IEnumerator MakeWave(Wave _wave) {
        playingWave = true;

        SelectedRoute = new List<Transform>();
        for (int j = 0; j < routes[_wave.Index_route].routeNodes.Count; j++) {
            SelectedRoute.Add(routes[_wave.Index_route].routeNodes[j]);
        }

        //경로 표시기 출력
        print("Draw Route Visualizer");

        yield return new WaitForSeconds(_wave.Delay_start);
        for (int i = 0; i < _wave.Count_spawn; i++) {
            GameObject spawnedenemy = Instantiate(EnemyContainer.instance.Enemies[_wave.Index_enemy]);

            spawnedenemy.transform.position = new Vector3(SelectedRoute[0].position.x, 0.15f, SelectedRoute[0].position.z);

            //spawnedenemy.GetComponent<EnemyController>().SetRoute(SelectedRoute);
            //spawnedenemy.GetComponent<CharacterInfo>().level = enemyLevel;
            //InGameManager.instance.SpawnedEnemy.Add(spawnedenemy);
            yield return new WaitForSeconds(_wave.Interval_spawn);
        }
        playingWave = false;
        StartWave();
    }

    //--------------------------------------------------------

    [ContextMenu("Instantiate Level")]
    public void InstantiateLevel() {
        for (int i = 0; i < MapSize.y; i++) {
            for(int j = 0; j < MapSize.x; j++) {
                GameObject node = Instantiate(Default_Node);
                node.transform.position = new Vector3(j, 0, i);
                node.GetComponent<NodeInfo>().index = new Vector2(j, i);
                node.transform.SetParent(gameObject.transform);
                Nodes.Add(node);
            }
        }
    }
    [ContextMenu("Apply Node Setting")]
    public void ApplyNodeSetting() {
        for(int i = 0; i < Nodes.Count; i++) {
            Nodes[i].GetComponent<NodeInfo>().SetNodeType();
        }
    }
}
