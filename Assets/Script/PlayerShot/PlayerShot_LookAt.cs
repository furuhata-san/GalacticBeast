using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot_LookAt : MonoBehaviour
{
    [Header("������ύX�������I�u�W�F�N�g���̂��̂ɃA�^�b�`���邱��")]
    [Header("�^�[�Q�b�g���W"), SerializeField]
    private Vector3 targetPos;

    [Header("��]��"), SerializeField, Range(0,1)]
    private Vector3 transformUp;


    //�^�[�Q�b�g��ύX�����ۂɁA�L���������̕���������
    private bool lookFlag;

    private void Start()
    {
        lookFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookFlag)
        {
            this.transform.LookAt(targetPos, transformUp);
            lookFlag = false;
        }
    }

    public void SetTargetAndLook(Vector3 pos, bool looking)
    {
        targetPos = pos;
        lookFlag = looking;
    }

}
