using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile_Controller : MonoBehaviour
{
    float nowCount;

    [Header("ミサイル発射後、少しの間その場で停止"), SerializeField]
    private float idleTime;
    private float nowIdleCount;
    [Tooltip("静止時、指定座標分、弧を描いて移動"), SerializeField]
    private Vector3 idleMoveValue;
    private Vector3 idleStartPos;
    private Vector3 idleStopPos;
    private bool idleFirst;//Startとして扱うために使用する

    [Header("目標に向かう際の加速度と最大速度")]
    [SerializeField] float maxSpeed;
    [SerializeField] float addSpeed;
    private float nowSpeed;

    [Header("ミサイル旋回速度"), SerializeField]
    private float lookSpeed;
    
    //ミサイルの目標地点
    private GameObject targetObject;
    private Vector3 targetPos;
    private bool targetSetting;

    [Header("生存時間"), SerializeField]
    private float liveTime;

    [Header("爆発エフェクト"), SerializeField]
    private GameObject effectPrefab;

    public void SetTarget(GameObject target)
    {
        targetObject = target;
        targetSetting = true;
    }

    public void SetTarget(Vector3 target)
    {
        targetPos = target;
        targetObject = null;
        targetSetting = true;
    }

    private Vector3 GetTargetPos()
    {
        Vector3 pos;
        if (targetObject)
        {
            pos = targetObject.transform.position;
        }
        else
        {
            pos = targetPos;
        }

        return pos;
    }

    // Start is called before the first frame update
    void Awake()
    {
        targetSetting = false;
        idleFirst = true;
        nowCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //ターゲットが関数を経由してセットされた場合、処理開始
        if (targetSetting)
        {
            //カウント増加
            nowCount += Time.deltaTime;

            //時間によって処理変更
            if (nowCount <= idleTime)
            {
                Idle_Slerp();
            }
            else if(nowCount < liveTime)
            {
                MissileMove();
            }
            else
            {
                BoomAndDestroy();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //敵か壁ではなかった場合、return
        if (other.transform.tag != "Obstacle" &&
            other.transform.tag != "Enemy")
        {
            return;
        }

        //エフェクト生成＆破壊
        nowCount += liveTime;
    }



    private void Idle_Slerp()
    {
        //最初に入った場合のみ、開始位置と終了位置を取得
        if (idleFirst)
        {
            idleStartPos = transform.position;
            idleStopPos = idleStartPos + idleMoveValue;
            nowIdleCount = 0;
            idleFirst = false;
        }

        //弧を描いて運動
        nowIdleCount += Time.deltaTime;
        transform.position = Vector3.Slerp(idleStartPos, idleStopPos, nowIdleCount / idleTime);
    }

    private void MissileMove()
    {
        //ターゲット座標を取得
        Vector3 markPos = GetTargetPos();//目標座標
        Vector3 distance = markPos - transform.position;//残りの距離

        //ターゲット方向を向く
        Quaternion look = Quaternion.LookRotation(distance);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, look, lookSpeed * Time.deltaTime);

        //加速
        if(nowSpeed < maxSpeed)
        {
            nowSpeed += addSpeed * Time.deltaTime;
        }
        else
        {
            nowSpeed = maxSpeed;
        }

        //前進
        transform.position += (transform.forward * nowSpeed) * Time.deltaTime;
    }

    private void BoomAndDestroy()
    {
        //エフェクト生成
        GameObject eff = Instantiate(effectPrefab);
        eff.transform.position = this.transform.position;

        //削除
        Destroy(gameObject);
    }

}
