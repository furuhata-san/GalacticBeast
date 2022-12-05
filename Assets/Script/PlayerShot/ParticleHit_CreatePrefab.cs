using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit_CreatePrefab : MonoBehaviour
{
    //�p�[�e�B�N�����i�q�b�g�ʒu�𐳊m�ɔ��肷�邽�߁j
    private ParticleSystem particle;
    private List<ParticleCollisionEvent> collisionEvents;   // �p�[�e�B�N���Փ˂Ɋւ�����

    [Header("��������Effect(Prefab)"), SerializeField]
    private GameObject effectPrefab;

    [Header("�G�t�F�N�g�𐶐�����^�O��"), SerializeField]
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
        //�q�b�g�����^�O���^�[�Q�b�g�ӊO�������ꍇ��return
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

        //�G�t�F�N�g����
        int numCollisionEvents = particle.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            CreateEffect(collisionEvents[i].intersection);
        }
    }

}
