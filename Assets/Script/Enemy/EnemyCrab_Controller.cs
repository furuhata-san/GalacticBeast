using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab_Controller : MonoBehaviour
{
    [Header("�J�j�^�̓G�̃}�l�[�W���[�Q��"), SerializeField]
    private Animator animator;

    [Header("�G�̗͂��Q��"), SerializeField]
    private EnemyHitPoint enemyHP;

    [Header("�o���A�w�莞�Ԃ͏����Ȃ�"), SerializeField]
    private float waitTime;

    [Header("�U���������J�n����܂ł̎��ԂƁA������ɍU�����n�߂�܂ł̎���")]
    [SerializeField] private float attackReadyTime;
    private bool attackReadyFlag;
    [SerializeField] private float attackPlayWait;
    private float nowCount;

    [System.Serializable]
    public class EnemyAttackData
    {
        [Header("�U����"), SerializeField]
        public float power;
        [Header("�U���g���K�[��")]
        public string triggerName;
        [Header("�U�����s������A�w��b����ɍU������𔭐�������"), SerializeField]
        public float attackStartTime;
        [Header("�U������̎�������"), SerializeField]
        public float attackStayTime;
        [Header("�U���̏I������"), SerializeField]
        public float attackEndTime;
    }
    [Header("�U�����"), SerializeField]
    private EnemyAttackData[] attackData = new EnemyAttackData[1];

    private bool attackFlag; //�U�������ǂ����𔻒�
    private bool attackAreaEnable; //�U�����肪�L�����ǂ���
    public bool GetAttackAreaFlag() { return attackAreaEnable; }
    private bool endFlag; //�����I��

    [Header("���S��A�폜����܂ł̏��")]
    [Tooltip("�폜�܂ł̎���"), SerializeField] private float deleteTime;

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
            //���S���Ă��Ȃ��ꍇ
            if (!enemyHP.JudgeDie())
            {
                //�U�����ł͂Ȃ��ꍇ
                if (!attackFlag)
                {
                    //���ԑ���
                    nowCount += Time.deltaTime;

                    //�w�莞�Ԍo�߂�����U���i�����j
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
        //�U�����������ꍇ�͖�����
        if (attackFlag)
        {
            yield break;
        }

        //�U���J�n
        attackFlag = true;

        //�U���������_���I��
        int attackNum = Random.Range(0, attackData.Length);

        //�A�j���[�V����
        animator.SetTrigger(attackData[attackNum].triggerName);

        //�U������J�n�܂őҋ@
        yield return new WaitForSeconds(attackData[attackNum].attackStartTime);

        //�U������L����
        attackAreaEnable = true;

        //�U������I���܂őҋ@
        yield return new WaitForSeconds(attackData[attackNum].attackStartTime);

        //�U�����薳����
        attackAreaEnable = false;

        //�U���I���܂őҋ@
        yield return new WaitForSeconds(attackData[attackNum].attackEndTime);

        //�U���I��
        attackFlag = false;
    }

    IEnumerator Dead()
    {
        //�A�j���[�V����
        animator.SetTrigger("Dead");
        
        //�폜�܂őҋ@
        yield return new WaitForSeconds(deleteTime);

        //�폜
        Destroy(gameObject);
    }
}
