using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie_EffectAndDestroy : MonoBehaviour
{
    [Header("�G�̗̑͏����Q��"), SerializeField]
    private EnemyHitPoint hpMgr;

    [Header("���S���Ă���w��b����ɐ�������Prefab")]
    [SerializeField] private float effectCreateTime;
    [SerializeField] private GameObject effectPrefab;

    [Header("�G�t�F�N�g������ɍ폜����܂ł̎��ԁi�O�b�ő��폜�j")]
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
            //�J�E���g����
            nowCount += Time.deltaTime;

            //�G�t�F�N�g�Q�Ƃ�����ꍇ�i��x���������null�ɏ㏑���j
            if (effectPrefab)
            {
                //�G�t�F�N�g�������Ԃ𒴂��Ă����ꍇ
                if (effectCreateTime <= nowCount)
                {
                    //�G�t�F�N�g�����A���W�����g�A�p�x��Y���̂݃����_��
                    GameObject eff = Instantiate(effectPrefab);
                    eff.transform.position = this.transform.position;
                    eff.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

                    //�Q�Ƃ��Ȃ����i���񂩂琶������Ȃ��Ȃ�j
                    effectPrefab = null;
                }
            }

            //�j��(else if�ɂ͂��Ȃ�����)
            if (effectCreateTime + destoroyTime <= nowCount)
            {
                //���g��j��
                Destroy(this.gameObject);
            }
        }
    }
}
