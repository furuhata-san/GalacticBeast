using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxCell : EnemyHitPoint
{
    [Header("�e�̗̑͂��Q��"), SerializeField]
    EnemyHitPoint baseHP;

    [Header("�{�̂ւ̃_���[�W�{��"), SerializeField]
    private float baseDamageRate;

    [Header("���ʔj�󎞂̒ǉ��_���[�W"), SerializeField]
    private float breakDamage;
    [Tooltip("��x�j�󂳂ꂽ�炻�̂܂�"), SerializeField]
    private bool Damage_Once;
    private bool cellBreak = false;

    private void Update()
    {
        if (this.JudgeDie())
        {
            if (!cellBreak)
            {
                //���ʔj��{�_���[�W
                cellBreak = true;
                baseHP.Damage(breakDamage);

                //������ł͂Ȃ��ꍇ
                if (!Damage_Once)
                {
                    //�̗͖��^���{�j��t���O������
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
