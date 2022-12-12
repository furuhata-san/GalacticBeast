using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHit_Damage : MonoBehaviour
{
    [Header("�U����"), SerializeField]
    private float power;

    [Header("�U���̔��莞��"), SerializeField]
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

        //�q�b�g�����I�u�W�F�N�g��EnemyHitPoint���A�^�b�`����Ă�����_���[�W
        EnemyHitPoint ehp;
        if (ehp = collision.gameObject.GetComponent<EnemyHitPoint>())
        {
            ehp.Damage(power);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;

        //�q�b�g�����I�u�W�F�N�g��EnemyHitPoint���A�^�b�`����Ă�����_���[�W
        EnemyHitPoint ehp;
        if (ehp = other.GetComponent<EnemyHitPoint>())
        {
            ehp.Damage(power);
        }
    }
}
