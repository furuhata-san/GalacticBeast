using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Crashing : MonoBehaviour
{
    [Header("Player_MoveController���Q��"), SerializeField]
    private Player_MoveController PMC;

    [Header("�ՓˑΏ�"), SerializeField]
    private string[] targetTagName = new string[1];

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
        //�^�O�ƈ�v���Ȃ��ꍇ��return
        bool flag = false;
        for (int i = 0; i < targetTagName.Length; ++i){
            if (collision.transform.tag == targetTagName[i])
            {
                flag = true;
                break;
            }
        }
        if (!flag) return;

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
