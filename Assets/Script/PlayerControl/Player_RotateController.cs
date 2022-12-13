using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_RotateController : MonoBehaviour
{
    [Header("������RigidBody�i�Q�Ƃ��Ȃ��ꍇ�͎��g�������ݒ�j")]
    [SerializeField]
    private Rigidbody playerRB;//�����������v���C���[

    //���x�f�[�^
    [System.Serializable]
    public class RotateSpeed
    {
        //�ō���
        [Header("�ō����x"), SerializeField]
        private float max;
        public ref float Ref_Max() { return ref max; }
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

        //��]��
        [Header("��]��"), SerializeField]
        private Vector3 rotateVec;
        public ref Vector3 Ref_RotateVec() { return ref rotateVec; }
        public Vector3 GetRotateVec() { return rotateVec; }

        //���݂̑��x
        //[System.NonSerialized]
        public float nowSpeed = 0;
    }

    [Header("�s�b�`�i�㉺�j"), SerializeField]//�㉺�Ɏ��_��U�鑬�x
    private RotateSpeed pitchSpeed;
    [Header("�s�b�`�t��]")] public bool pitchReverse = false;

    [Header("���[�i���E�j"), SerializeField]//���E�Ɏ��_��U�鑬�x
    private RotateSpeed yawSpeed;


    [Header("�f�b�h�]�[��"), SerializeField] private float padDeadZone;//�Q�[���p�b�h���̖͂�����


    // Start is called before the first frame update
    void Start()
    {
        //�Q�Ƃ��Ȃ��ꍇ�A���g���Q��
        if (playerRB == null)
        {
            //RigidBody���Ȃ��ꍇ�͒ʒm
            if(!(playerRB = this.GetComponent<Rigidbody>()))
            {
                print("RigidBody���A�^�b�`�E�Q�Ƃ���Ă��܂���");
            }
        }

        playerRB.interpolation = RigidbodyInterpolation.Extrapolate;
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

        //�f�[�^�𗘗p���Ĉړ����s��
        RotateUD();
        RotateLR();

        //Z�����O�x�ɋ߂Â���
        RotateZ_BringCloserZero(true, Time.deltaTime);
    }

    void RotateUD()
    {
        float tdt = Time.deltaTime;

        //������i�E�X�e�B�b�N��j
        float stickValue = -Gamepad.current.rightStick.ReadValue().y;
        if (pitchReverse)
        {
            //���o�[�X
            stickValue = -stickValue;
        }

        if (Gamepad.current.rightStick.ReadValue().y >= padDeadZone)
        {
            //�O�i
            Speed(pitchSpeed, stickValue, true);
        }
        //�������i�E�X�e�B�b�N���j
        else if (Gamepad.current.rightStick.ReadValue().y <= -padDeadZone)
        {
            //���
            Speed(pitchSpeed, stickValue, false);
        }
        //�Î~�i���͂Ȃ��j
        else
        {
            //�����i�u���[�L��������j
            //�O�i���i��ގ��j�Ɍ���
            if (pitchSpeed.nowSpeed != 0)
            {
                pitchSpeed.nowSpeed -= (pitchSpeed.nowSpeed / pitchSpeed.GetInertia()) * tdt;
            }
        }

        //��]
        Quaternion turn = Quaternion.Euler(pitchSpeed.GetRotateVec() * pitchSpeed.nowSpeed * tdt);
        playerRB.MoveRotation(playerRB.rotation * turn);
    }

    void RotateLR()
    {
        float tdt = Time.deltaTime;

        //�������i�E�X�e�B�b�N���j
        float stickValue = Gamepad.current.rightStick.ReadValue().x;
        if (Gamepad.current.rightStick.ReadValue().x >= padDeadZone)
        {
            //����]
            Speed(yawSpeed, stickValue, true);
        }
        //�E�����i�E�X�e�B�b�N�E�j
        else if (Gamepad.current.rightStick.ReadValue().x <= -padDeadZone)
        {
            //�E��]
            Speed(yawSpeed, stickValue, false);
        }
        //�Î~�i���͂Ȃ��j
        else
        {
            //�����i�u���[�L��������j
            //�O�i���i��ގ��j�Ɍ���
            if (yawSpeed.nowSpeed != 0)
            {
                yawSpeed.nowSpeed -= (yawSpeed.nowSpeed / yawSpeed.GetInertia()) * tdt;
            }
        }

        //��]
        Quaternion turn = Quaternion.Euler(yawSpeed.GetRotateVec() * yawSpeed.nowSpeed * tdt);
        playerRB.MoveRotation(playerRB.rotation * turn);
    }

    void Speed(RotateSpeed data, float stickValue, bool to_UP_or_LEFT)
    {
        //���݂̓��͒l���擾(Y)
        float tdt = Time.deltaTime;

        //�O�i���i��ގ��j�Ɍ���
        if (data.nowSpeed < 0 && to_UP_or_LEFT)
        {
            data.nowSpeed -= (data.nowSpeed / data.GetInertia()) * tdt;
        }
        else if (data.nowSpeed > 0 && !to_UP_or_LEFT)
        {
            data.nowSpeed -= (data.nowSpeed / data.GetInertia()) * tdt;
        }

        //�O���։������s��(�����~�X�e�B�b�N��|�����ʁ~DeltaTime)
        data.nowSpeed += data.GetAdd() * stickValue * tdt;

        //�ō��i�Œ�j���x�␳
        if (data.nowSpeed >= data.GetMax())
        {
            data.nowSpeed = data.GetMax();
        }
        else if (data.nowSpeed <= -data.GetMax())
        {
            data.nowSpeed = -data.GetMax();
        }
    }

    void RotateZ_BringCloserZero(bool use, float tdt)
    {
        //�g�p���Ȃ��ꍇ�͏������s��Ȃ�
        if (!use) return;

        //Z�����ǂ�قǌX���Ă��邩���v�Z����
        float zVal = transform.eulerAngles.z;
        //-180�`180�ɕ␳
        if (zVal > 180)
        {
            zVal -= 360;
        }

        //����̂P�t���[��������̉�]�ʂ����߂�
        float rotZ = zVal * tdt;

        //XY���̉�]�͂��̂܂�
        //float rotX = transform.localEulerAngles.x;
        //float rotY = transform.localEulerAngles.y;

        //��]���s��
        Quaternion turn = Quaternion.Euler(new Vector3(0, 0, -rotZ));
        playerRB.MoveRotation(playerRB.rotation * turn);
    }
}
