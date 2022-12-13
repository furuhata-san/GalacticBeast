using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab_Controller : MonoBehaviour
{
    [Header("カニ型の敵のマネージャー参照"), SerializeField]
    private Animator animator;

    [Header("敵体力を参照"), SerializeField]
    private EnemyHitPoint enemyHP;

    [Header("登場後、指定時間は処理なし"), SerializeField]
    private float waitTime;

    [Header("攻撃準備を開始するまでの時間と、準備後に攻撃を始めるまでの時間")]
    [SerializeField] private float attackReadyTime;
    private bool attackReadyFlag;
    [SerializeField] private float attackPlayWait;
    private float nowCount;

    [System.Serializable]
    public class EnemyAttackData
    {
        [Header("攻撃力"), SerializeField]
        public float power;
        [Header("攻撃トリガー名")]
        public string triggerName;
        [Header("攻撃を行った後、指定秒数後に攻撃判定を発生させる"), SerializeField]
        public float attackStartTime;
        [Header("攻撃判定の持続時間"), SerializeField]
        public float attackStayTime;
        [Header("攻撃の終了時間"), SerializeField]
        public float attackEndTime;
    }
    [Header("攻撃情報"), SerializeField]
    private EnemyAttackData[] attackData = new EnemyAttackData[1];

    private bool attackFlag; //攻撃中かどうかを判定
    private bool attackAreaEnable; //攻撃判定が有効かどうか
    public bool GetAttackAreaFlag() { return attackAreaEnable; }
    private bool endFlag; //処理終了

    [Header("死亡後、削除するまでの情報")]
    [Tooltip("削除までの時間"), SerializeField] private float deleteTime;

    // Start is called before the first frame update
    void Start()
    {
        nowCount = 0;
        attackFlag = false;
        attackAreaEnable = false;
        attackReadyFlag = false;
        endFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= 0 && !endFlag)
        {
            //死亡していない場合
            if (!enemyHP.JudgeDie())
            {
                //攻撃中ではない場合
                if (!attackFlag)
                {
                    //時間増加
                    nowCount += Time.deltaTime;

                    //指定時間経過したら攻撃（準備）
                    if (nowCount >= attackReadyTime + attackPlayWait)
                    {
                        nowCount = 0;
                        attackReadyFlag = false;
                        StartCoroutine("CrabAttack");
                    }
                    else if (nowCount >= attackReadyTime && !attackReadyFlag)
                    {
                        animator.SetTrigger("AttackReady");
                        attackReadyFlag = true;
                    }
                }
            }
            else
            {
                StopCoroutine("CrabAttack");
                StartCoroutine("Dead");
                endFlag = true;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    IEnumerator CrabAttack()
    {
        //攻撃中だった場合は無効化
        if (attackFlag)
        {
            yield break;
        }

        //攻撃開始
        attackFlag = true;

        //攻撃をランダム選択
        int attackNum = Random.Range(0, attackData.Length);

        //アニメーション
        animator.SetTrigger(attackData[attackNum].triggerName);

        //攻撃判定開始まで待機
        yield return new WaitForSeconds(attackData[attackNum].attackStartTime);

        //攻撃判定有効化
        attackAreaEnable = true;

        //攻撃判定終了まで待機
        yield return new WaitForSeconds(attackData[attackNum].attackStartTime);

        //攻撃判定無効化
        attackAreaEnable = false;

        //攻撃終了まで待機
        yield return new WaitForSeconds(attackData[attackNum].attackEndTime);

        //攻撃終了
        attackFlag = false;
    }

    IEnumerator Dead()
    {
        //アニメーション
        animator.SetTrigger("Dead");
        
        //削除まで待機
        yield return new WaitForSeconds(deleteTime);

        //削除
        Destroy(gameObject);
    }
}
