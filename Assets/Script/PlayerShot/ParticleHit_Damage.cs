using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit_Damage : MonoBehaviour
{
    [Header("攻撃力"), SerializeField]
    private float power;

    private void OnParticleCollision(GameObject other)
    {
        //ヒットしたオブジェクトにEnemyHitPointがアタッチされていたらダメージ
        EnemyHitPoint ehp;
        if(ehp = other.GetComponent<EnemyHitPoint>())
        {
            ehp.Damage(power);
        }
    }
}
