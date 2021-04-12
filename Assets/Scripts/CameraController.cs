using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject CameraPosTarget;

    Vector3 MapCenter;
    void Start() {
        MapCenter = new Vector3(
            InGameManager.instance.Map.transform.GetChild(InGameManager.instance.Map.transform.childCount - 1).transform.position.x * 0.5f, 0, InGameManager.instance.Map.transform.GetChild(InGameManager.instance.Map.transform.childCount - 1).transform.position.z * 0.5f);
        CameraPosTarget.transform.position = MapCenter;
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosSet();
    }
    void CameraPosSet() {
        if (InGameManager.instance.SelectedDoll == null && InGameManager.instance.SelectedNode == null) {
            moveCameraSmoothly(MapCenter);
        }
        else {
            if (InGameManager.instance.SelectedDoll != null) {
                moveCameraSmoothly(InGameManager.instance.SelectedDoll.transform.position);
            }
            else {
                moveCameraSmoothly(MapCenter);
            }
        }
    }
    float velocityX;
    float velocityZ;
    void moveCameraSmoothly(Vector3 end) {
        float posX = Mathf.SmoothDamp(CameraPosTarget.transform.position.x, end.x, ref velocityX, 0.08f);
        float posZ = Mathf.SmoothDamp(CameraPosTarget.transform.position.z, end.z, ref velocityZ, 0.08f);
        CameraPosTarget.transform.position = new Vector3(posX, 0, posZ);
    }

}
