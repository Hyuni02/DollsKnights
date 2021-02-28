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

    [SerializeField]
    private GameObject Default_Node;
    public List<GameObject> Nodes;
    [SerializeField]
    public List<Route> routes;

    void Start()
    {
        //SetNodeIndex();
    }


    void Update()
    {
        
    }

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
