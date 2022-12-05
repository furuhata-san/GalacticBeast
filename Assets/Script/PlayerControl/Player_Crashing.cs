using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Crashing : MonoBehaviour
{
    [Header("Player_MoveController���Q��"), SerializeField]
    private Player_MoveController PMC;

    [Header("���Ƃ̏Փˎ��ɒ��˕Ԃ鑬�x"), SerializeField]
    private float knockbackPower;

    // Start is called before the first frame update
    void Start()
    {
        if(PMC == null)
        {
            if(!(PMC = GetComponent<Player_MoveController>()))
            {
                print("�v���C���[�R���g���[���[���A�^�b�`����Ă��܂���");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //��Q���ł͂Ȃ��ꍇ�͏����Ȃ�
        if(collision.transform.tag != "Obstacle") { return; }

        //Rigidbody�Î~
        Rigidbody myrb;
        if (myrb = this.GetComponent<Rigidbody>())
        {
            myrb.velocity = Vector3.zero;
        }

        //�ڐG�n�_���擾���A�t���@�փm�b�N�o�b�N
        Vector3 crachPos = collision.contacts[0].point;
        Vector3 knockbackVec = this.transform.position - crachPos;//�ڐG�ʒu�����g�Ƃǂ�قǗ���Ă��邩

        // �t�����ɂ͂˂�
        PMC.NowSpeedReset();
        Vector3 knockbackForce = knockbackPower * knockbackVec.normalized;
        myrb.AddForce(knockbackForce, ForceMode.Impulse);
    }

}
