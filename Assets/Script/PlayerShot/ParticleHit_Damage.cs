using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit_Damage : MonoBehaviour
{
    [Header("�U����"), SerializeField]
    private float power;

    private void OnParticleCollision(GameObject other)
    {
        //�q�b�g�����I�u�W�F�N�g��EnemyHitPoint���A�^�b�`����Ă�����_���[�W
        EnemyHitPoint ehp;
        if(ehp = other.GetComponent<EnemyHitPoint>())
        {
            ehp.Damage(power);
        }
    }
}
