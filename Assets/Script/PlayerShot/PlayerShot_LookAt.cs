using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot_LookAt : MonoBehaviour
{
    [Header("向きを変更したいオブジェクトそのものにアタッチすること")]
    [Header("ターゲット座標"), SerializeField]
    private Vector3 targetPos;

    [Header("回転軸"), SerializeField, Range(0,1)]
    private Vector3 transformUp;


    //ターゲットを変更した際に、有効化しその方向を向く
    private bool lookFlag;

    private void Start()
    {
        lookFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookFlag)
        {
            this.transform.LookAt(targetPos, transformUp);
            lookFlag = false;
        }
    }

    public void SetTargetAndLook(Vector3 pos, bool looking)
    {
        targetPos = pos;
        lookFlag = looking;
    }

}
