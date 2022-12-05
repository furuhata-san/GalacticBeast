using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_StickAnim : MonoBehaviour
{
    [Header("�X�e�B�b�N��|�����ʂɉ����ăn���h�����X����")]
    [Header("�R�b�N�s�b�g���̃n���h�����Q�Ƃ���"), SerializeField]
    private GameObject cockpitStick;

    [Header("�I�u�W�F�N�g�́uX:�ŏ��p�x Y:�ő�p�x�v")]
    [SerializeField] private Vector2 angleLimitX;
    [SerializeField] private Vector2 angleLimitZ;

    private void Start()
    {
        if (!cockpitStick)
        {
            print("�R�b�N�s�b�g�̃n���h�����Q�Ƃ��Ă��������B");
        }
    }

    private void Update()
    {
        //�X�e�B�b�N���͗ʂ��擾
        Vector2 stickValue = Gamepad.current.rightStick.ReadValue();

        //���̒l�����͂���Ă����ꍇ�͕␳
        if (angleLimitX.x < 0) angleLimitX.x = -angleLimitX.x;
        if (angleLimitZ.x < 0) angleLimitZ.x = -angleLimitZ.x;

        //�n���h���c�i�O��ړ��j
        //X����]
        float rotateX = 0;
        //�����
        if (stickValue.y > 0)
        {
            rotateX = angleLimitX.y * stickValue.y;
        }
        //������
        else
        {
            rotateX = angleLimitX.x * stickValue.y;
        }

        //�n���h�����i���E�ړ��j
        //Z����]
        float rotateZ = 0;
        //�����
        if (stickValue.x < 0)
        {
            rotateZ = angleLimitZ.y * -stickValue.x;
        }
        //������
        else
        {
            rotateZ = angleLimitZ.x * -stickValue.x;
        }

        //��]
        Vector3 stickAngle = new Vector3(rotateX, 0, rotateZ);
        cockpitStick.transform.localRotation = Quaternion.Euler(stickAngle);
    }
}
