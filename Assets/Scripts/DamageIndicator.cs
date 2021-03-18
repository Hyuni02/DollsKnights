using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Text text;
    public enum Type { miss, hit, crit, block}
    public Type type;

    public void SetIndicator(Type type, int dmg, Vector3 pos) {
        text = transform.GetChild(0).GetComponent<Text>();
        text.text = dmg.ToString();
        transform.position = pos;

        switch (type) {
            case Type.miss:
                //글씨 색 설정
                text.text = "miss";
                break;
            case Type.hit:
                //글씨 색 설정

                break;
            case Type.crit:
                //글씨 색 설정

                break;
            case Type.block:
                //글씨 색 설정

                break;
        }
        StartCoroutine(destroy());
    }

    IEnumerator destroy() {
        yield return new WaitForSeconds(0.5f);
        InGameManager.instance.ReturnIndicator(gameObject.GetComponent<DamageIndicator>());
    }
}
