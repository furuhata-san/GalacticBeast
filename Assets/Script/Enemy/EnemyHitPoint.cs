using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPoint : MonoBehaviour
{
    [Header("�����̗�"), SerializeField]
    private float maxHp;
    [Header("���݂̗̑�"), SerializeField]
    private float nowHp;

    void Start()
    {
        nowHp = maxHp;
    }

    //�_���[�W
    public void Damage(float value)
    {
        nowHp -= value;
    }

    //��
    public void Heal(float value)
    {
        nowHp += value;
    }

    //�ő�l�␳
    public void HP_MaxLimiter()
    {
        if(nowHp >= maxHp)
        {
            nowHp = maxHp;
        }
    }

    //�ŏ��l�␳
    public void HP_MinLimiter()
    {
        if (nowHp > 0)
        {
            nowHp = 0;
        }
    }

    //���S����i�̗͂��O�ȉ��j
    public bool JudgeDie()
    {
        return nowHp <= 0;
    }

    //�̗͂��w�肵�����l���傫��(������)���𔻒肷��
    public bool HPJudge(float value, bool judge_Many)
    {
        bool ans = false;
        if (judge_Many)
        {
            ans = (nowHp >= value);
        }
        else
        {
            ans = (nowHp <= value);
        }

        return ans;
    }

}
