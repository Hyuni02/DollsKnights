using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Route {
    public List<Transform> routeNodes;

    public Route(List<Transform> routeNodes) {
        this.routeNodes = routeNodes;
    }
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

    public List<Transform> Nodes;
    public List<Transform> enemyspawnPoint;
    [SerializeField]
    public List<Route> routes;

    void Start()
    {
        SetNodeIndex();
    }
    void SetNodeIndex() {
        for(int i = 0; i < MapSize.x * MapSize.y; i++) {
            Nodes[i].GetComponent<NodeInfo>().index = new Vector2(i%MapSize.x, Mathf.Floor(i/MapSize.x));
        }
    }

    void Update()
    {
        
    }
}
