using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInfo : MonoBehaviour
{
    public enum Type { low, high, enemyspawn, command}
    public Type type;
    public bool placeable;
    public bool heilport;
    public Vector2 index;

    public void Start() {
        if (heilport) {
            //Instantiate(GameManager.instance.Icon_Heilport);
        }
    }
}
