using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit_CreatePrefab : MonoBehaviour
{
    //パーティクル情報（ヒット位置を正確に判定するため）
    private ParticleSystem particle;
    private List<ParticleCollisionEvent> collisionEvents;   // パーティクル衝突に関する情報

    [Header("生成するEffect(Prefab)"), SerializeField]
    private GameObject effectPrefab;

    [Header("エフェクトを生成するタグ名"), SerializeField]
    private string[] tagName = new string[1];

    private void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void CreateEffect(Vector3 pos)
    {
        GameObject eff = Instantiate(effectPrefab);
        eff.transform.position = pos;
        //eff.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
    }

    private void OnParticleCollision(GameObject other)
    {
        //ヒットしたタグがターゲット意外だった場合はreturn
        bool targetHitting = false;
        for(int i = 0; i < tagName.Length; ++i)
        {
            if (other.transform.tag == tagName[i])
            {
                targetHitting = true;
                break;
            }
        }
        if (!targetHitting) return;

        //エフェクト生成
        int numCollisionEvents = particle.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            CreateEffect(collisionEvents[i].intersection);
        }
    }

}
