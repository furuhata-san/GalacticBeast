using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHit_Damage : MonoBehaviour
{
    [Header("攻撃力"), SerializeField]
    private float power;

    [Header("攻撃の判定時間"), SerializeField]
    private float attackTime;
    private float nowCount;

    [SerializeField]
    private bool isTrigger;

    private void Start()
    {
        nowCount = 0;
    }

    private void Update()
    {
        nowCount += Time.deltaTime;
        if(nowCount >= attackTime)
        {
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTrigger) return;

        //ヒットしたオブジェクトにEnemyHitPointがアタッチされていたらダメージ
        EnemyHitPoint ehp;
        if (ehp = collision.gameObject.GetComponent<EnemyHitPoint>())
        {
            ehp.Damage(power);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;

        //ヒットしたオブジェクトにEnemyHitPointがアタッチされていたらダメージ
        EnemyHitPoint ehp;
        if (ehp = other.GetComponent<EnemyHitPoint>())
        {
            ehp.Damage(power);
        }
    }
}
