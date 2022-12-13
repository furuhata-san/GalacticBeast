using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxCell : EnemyHitPoint
{
    [Header("親の体力を参照"), SerializeField]
    EnemyHitPoint baseHP;

    [Header("本体へのダメージ倍率"), SerializeField]
    private float baseDamageRate;

    [Header("部位破壊時の追加ダメージ"), SerializeField]
    private float breakDamage;
    [Tooltip("一度破壊されたらそのまま"), SerializeField]
    private bool Damage_Once;
    private bool cellBreak = false;

    private void Update()
    {
        if (this.JudgeDie())
        {
            if (!cellBreak)
            {
                //部位破壊＋ダメージ
                cellBreak = true;
                baseHP.Damage(breakDamage);

                //一回限りではない場合
                if (!Damage_Once)
                {
                    //体力満タン＋破壊フラグ無効化
                    this.Heal(this.GetMaxHP());
                    cellBreak = false;
                }
            }
        }
    }

    public override void Damage(float value)
    {
        baseHP.Damage(value * baseDamageRate);
        this.nowHp -= value;
    }
}
