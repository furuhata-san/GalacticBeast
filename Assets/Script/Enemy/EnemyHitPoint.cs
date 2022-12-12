using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPoint : MonoBehaviour
{
    [Header("初期体力"), SerializeField]
    private float maxHp;
    [Header("現在の体力"), SerializeField]
    private float nowHp;

    void Start()
    {
        nowHp = maxHp;
    }

    //ダメージ
    public void Damage(float value)
    {
        nowHp -= value;
    }

    //回復
    public void Heal(float value)
    {
        nowHp += value;
    }

    //最大値補正
    public void HP_MaxLimiter()
    {
        if(nowHp >= maxHp)
        {
            nowHp = maxHp;
        }
    }

    //最小値補正
    public void HP_MinLimiter()
    {
        if (nowHp > 0)
        {
            nowHp = 0;
        }
    }

    //死亡判定（体力が０以下）
    public bool JudgeDie()
    {
        return nowHp <= 0;
    }

    //体力が指定した数値より大きい(小さい)かを判定する
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
