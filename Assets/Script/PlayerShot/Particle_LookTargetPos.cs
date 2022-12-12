using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_LookTargetPos : MonoBehaviour
{
    [Header("エイム情報の参照"), SerializeField]
    private Player_Aim aimPos;

    [Header("回転軸"), SerializeField]
    private Vector3Int transformUp;

    // Update is called once per frame
    void Update()
    {
        //エイムがヒットしたかどうかで向く方向を変更
        if (aimPos.SiteHitJudge())
        {
            Vector3 targetPos = aimPos.GetSiteHitPoint().hitPos;
            this.transform.LookAt(targetPos);
        }
        else
        {
            this.transform.rotation = aimPos.gameObject.transform.localRotation;
        }
    }
}
