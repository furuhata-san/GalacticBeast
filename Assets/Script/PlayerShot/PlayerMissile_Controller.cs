using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile_Controller : MonoBehaviour
{
    float nowCount;

    [Header("�~�T�C�����ˌ�A�����̊Ԃ��̏�Œ�~"), SerializeField]
    private float idleTime;
    private float nowIdleCount;
    [Tooltip("�Î~���A�w����W���A�ʂ�`���Ĉړ�"), SerializeField]
    private Vector3 idleMoveValue;
    private Vector3 idleStartPos;
    private Vector3 idleStopPos;
    private bool idleFirst;//Start�Ƃ��Ĉ������߂Ɏg�p����

    [Header("�ڕW�Ɍ������ۂ̉����x�ƍő呬�x")]
    [SerializeField] float maxSpeed;
    [SerializeField] float addSpeed;
    private float nowSpeed;

    [Header("�~�T�C�����񑬓x"), SerializeField]
    private float lookSpeed;
    
    //�~�T�C���̖ڕW�n�_
    private GameObject targetObject;
    private Vector3 targetPos;
    private bool targetSetting;

    [Header("��������"), SerializeField]
    private float liveTime;

    [Header("�����G�t�F�N�g"), SerializeField]
    private GameObject effectPrefab;

    public void SetTarget(GameObject target)
    {
        targetObject = target;
        targetSetting = true;
    }

    public void SetTarget(Vector3 target)
    {
        targetPos = target;
        targetObject = null;
        targetSetting = true;
    }

    private Vector3 GetTargetPos()
    {
        Vector3 pos;
        if (targetObject)
        {
            pos = targetObject.transform.position;
        }
        else
        {
            pos = targetPos;
        }

        return pos;
    }

    // Start is called before the first frame update
    void Awake()
    {
        targetSetting = false;
        idleFirst = true;
        nowCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�^�[�Q�b�g���֐����o�R���ăZ�b�g���ꂽ�ꍇ�A�����J�n
        if (targetSetting)
        {
            //�J�E���g����
            nowCount += Time.deltaTime;

            //���Ԃɂ���ď����ύX
            if (nowCount <= idleTime)
            {
                Idle_Slerp();
            }
            else if(nowCount < liveTime)
            {
                MissileMove();
            }
            else
            {
                BoomAndDestroy();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�G���ǂł͂Ȃ������ꍇ�Areturn
        if (other.transform.tag != "Obstacle" &&
            other.transform.tag != "Enemy")
        {
            return;
        }

        //�G�t�F�N�g�������j��
        nowCount += liveTime;
    }



    private void Idle_Slerp()
    {
        //�ŏ��ɓ������ꍇ�̂݁A�J�n�ʒu�ƏI���ʒu���擾
        if (idleFirst)
        {
            idleStartPos = transform.position;
            idleStopPos = idleStartPos + idleMoveValue;
            nowIdleCount = 0;
            idleFirst = false;
        }

        //�ʂ�`���ĉ^��
        nowIdleCount += Time.deltaTime;
        transform.position = Vector3.Slerp(idleStartPos, idleStopPos, nowIdleCount / idleTime);
    }

    private void MissileMove()
    {
        //�^�[�Q�b�g���W���擾
        Vector3 markPos = GetTargetPos();//�ڕW���W
        Vector3 distance = markPos - transform.position;//�c��̋���

        //�^�[�Q�b�g����������
        Quaternion look = Quaternion.LookRotation(distance);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, look, lookSpeed * Time.deltaTime);

        //����
        if(nowSpeed < maxSpeed)
        {
            nowSpeed += addSpeed * Time.deltaTime;
        }
        else
        {
            nowSpeed = maxSpeed;
        }

        //�O�i
        transform.position += (transform.forward * nowSpeed) * Time.deltaTime;
    }

    private void BoomAndDestroy()
    {
        //�G�t�F�N�g����
        GameObject eff = Instantiate(effectPrefab);
        eff.transform.position = this.transform.position;

        //�폜
        Destroy(gameObject);
    }

}
