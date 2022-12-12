using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_LookTargetPos : MonoBehaviour
{
    [Header("エイム情報の参照"), SerializeField]
    private Player_Aim aimPos;

    [Header("回転軸"), SerializeField]
    private Vector3Int transformUp;
    private Quaternion defLookAngle;//初期値の保存

    // Start is called before the first frame update
    void Start()
    {
        defLookAngle = this.transform.rotation;
    }

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
            this.transform.rotation = defLookAngle;
        }
    }
}
