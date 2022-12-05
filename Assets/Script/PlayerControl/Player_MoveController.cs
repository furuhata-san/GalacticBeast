using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 
/// �ړ��݂̂𓝊�����X�N���v�g
/// 
/// </summary>


public class Player_MoveController : MonoBehaviour
{
    [Header("�������I�u�W�F�N�g�i�Q�Ƃ��Ȃ��ꍇ�͎��g�������ݒ�j")]
    [SerializeField]
    private GameObject player;//�����������v���C���[

    //���x�f�[�^
    [System.Serializable]
    public class MoveSpeed
    {
        //�ō���
        [Header("�ō����x"), SerializeField]
        private float max;
        public ref float Ref_Max(){ return ref max; }
        public float GetMax() { return max; }

        //�����x
        [Header("�����x"), SerializeField]
        private float add;
        public ref float Ref_Add() { return ref add; }
        public float GetAdd() { return add; }

        //����
        [Header("����"), SerializeField]
        private float inertia;
        public ref float Ref_Inertia() { return ref inertia; }
        public float GetInertia() { return inertia; }
    }
    [Header("�O�i"), SerializeField] private MoveSpeed FrontSpeed;//�O�i���x�f�[�^
    [Header("���"), SerializeField] private MoveSpeed BackSpeed;//��ޑ��x�f�[�^
    [Header("���E"), SerializeField] private MoveSpeed LRSpeed;//��ޑ��x�f�[�^

    [Header("�ړ����i�O��j"), SerializeField] private Vector3 moveVecFB = Vector3.forward;//�ړ������i�����l��Z���O���Ƃ���j
    [Header("�ړ����i���E�j"), SerializeField] private Vector3 moveVecLR = Vector3.left;//�ړ������i�����l��X���O���Ƃ���j
    [Header("�f�b�h�]�[��"), SerializeField]   private float padDeadZone;//�Q�[���p�b�h���̖͂�����

    private float nowSpeed_FB;//���݂̑��x�i�O�i��ށj
    private float nowSpeed_LR;//���݂̑��x�i���E�ړ��j
    public void NowSpeedReset()
    {
        nowSpeed_FB = 0;
        nowSpeed_LR = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Player�Q�Ƃ��Ȃ��ꍇ�́A���g���v���C���[�Ƃ��Đݒ肷��
        if (player == null) player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���p�b�h���ڑ�����Ă��Ȃ��ꍇ�͏����Ȃ�
        if (Gamepad.current == null)
        {
            print("�R���g���[���[���K�v�ł�");
            return;
        }
        //�ړ�����
        MoveFB();
        MoveLR();
    }

    private void MoveFB()
    {
        float tdt = Time.deltaTime;

        //�O�i�i���X�e�B�b�N��j
        if (Gamepad.current.leftStick.ReadValue().y >= padDeadZone)
        {
            //�O�i
            Speed_FrontBack(FrontSpeed, true);
        }
        //��ށi���X�e�B�b�N���j
        else if (Gamepad.current.leftStick.ReadValue().y <= -padDeadZone)
        {
            //���
            Speed_FrontBack(BackSpeed, false);
        }
        //�Î~�i���͂Ȃ��j
        else
        {
            //�����i�u���[�L��������j
            //�O�i���i��ގ��j�Ɍ���
            if (nowSpeed_FB != 0)
            {
                nowSpeed_FB -= (nowSpeed_FB / FrontSpeed.GetInertia()) * tdt;
            }
        }

        //�ړ�
        this.transform.Translate(moveVecFB * nowSpeed_FB * tdt);
    
    }
  
    void MoveLR()
    {
        float tdt = Time.deltaTime;

        //�����s�ړ��i���X�e�B�b�N���j
        if (Gamepad.current.leftStick.ReadValue().x >= padDeadZone)
        {
            //��
            Speed_LeftRight(LRSpeed, true);
        }
        //�E���s�ړ��i���X�e�B�b�N�E�j
        else if (Gamepad.current.leftStick.ReadValue().x <= -padDeadZone)
        {
            //�E
            Speed_LeftRight(LRSpeed, false);
        }
        //�Î~�i���͂Ȃ��j
        else
        {
            //�����i�u���[�L��������j
            if (nowSpeed_LR != 0)
            {
                nowSpeed_LR -= (nowSpeed_LR / LRSpeed.GetInertia()) * tdt;
            }
        }

        //�ړ�
        this.transform.Translate(moveVecLR * nowSpeed_LR * tdt);
    } 


    void Speed_FrontBack(MoveSpeed data, bool toFront)
    {
        //���݂̓��͒l���擾(Y)
        float stickValue = Gamepad.current.leftStick.ReadValue().y;
        float tdt = Time.deltaTime;

        //�O�i���i��ގ��j�Ɍ���
        if (nowSpeed_FB < 0 && toFront)
        {
            nowSpeed_FB -= (nowSpeed_FB / data.GetInertia()) * tdt;
        }
        else if (nowSpeed_FB > 0 && !toFront)
        {
            nowSpeed_FB -= (nowSpeed_FB / data.GetInertia()) * tdt;
        }

        //�O���։������s��(�����~�X�e�B�b�N��|�����ʁ~DeltaTime)
        nowSpeed_FB += data.GetAdd() * stickValue * tdt;

        //�ő呬�x�␳
        if(nowSpeed_FB >= data.GetMax())
        {
            nowSpeed_FB = data.GetMax();
        }
        else if(nowSpeed_FB <= -data.GetMax())
        {
            nowSpeed_FB = -data.GetMax();
        }
    }

    void Speed_LeftRight(MoveSpeed data, bool toLeft)
    {
        //���݂̓��͒l���擾(X)
        float stickValue = Gamepad.current.leftStick.ReadValue().x;
        float tdt = Time.deltaTime;

        //�t�����ֈړ����Ɍ���
        if (nowSpeed_LR > 0 && toLeft)
        {
            nowSpeed_LR -= (nowSpeed_LR / data.GetInertia()) * tdt;
        }
        else if (nowSpeed_LR < 0 && !toLeft)
        {
            nowSpeed_LR -= (nowSpeed_LR / data.GetInertia()) * tdt;
        }

        //���։������s��(�����~�X�e�B�b�N��|�����ʁ~DeltaTime)
        nowSpeed_LR -= data.GetAdd() * stickValue * tdt;

        //�ő呬�x�␳
        if (nowSpeed_LR >= data.GetMax())
        {
            nowSpeed_LR = data.GetMax();
        }
        else if (nowSpeed_LR <= -data.GetMax())
        {
            nowSpeed_LR = -data.GetMax();
        }
    }


    //�ȉ�Editor�p�֐�
    //public ref GameObject Ref_Player() { return ref player; }

}
