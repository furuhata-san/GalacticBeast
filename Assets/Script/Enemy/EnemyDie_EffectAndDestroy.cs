using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie_EffectAndDestroy : MonoBehaviour
{
    [Header("敵の体力情報を参照"), SerializeField]
    private EnemyHitPoint hpMgr;

    [Header("死亡してから指定秒数後に生成するPrefab")]
    [SerializeField] private float effectCreateTime;
    [SerializeField] private GameObject effectPrefab;

    [Header("エフェクト生成後に削除するまでの時間（０秒で即削除）")]
    [SerializeField] private float destoroyTime;

    private float nowCount;

    // Start is called before the first frame update
    void Start()
    {
        nowCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpMgr.JudgeDie())
        {
            //カウント増加
            nowCount += Time.deltaTime;

            //エフェクト参照がある場合（一度複製するとnullに上書き）
            if (effectPrefab)
            {
                //エフェクト生成時間を超えていた場合
                if (effectCreateTime <= nowCount)
                {
                    //エフェクト生成、座標＝自身、角度＝Y軸のみランダム
                    GameObject eff = Instantiate(effectPrefab);
                    eff.transform.position = this.transform.position;
                    eff.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

                    //参照をなくす（次回から生成されなくなる）
                    effectPrefab = null;
                }
            }

            //破壊(else ifにはしないこと)
            if (effectCreateTime + destoroyTime <= nowCount)
            {
                //自身を破壊
                Destroy(this.gameObject);
            }
        }
    }
}
