using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Aim : MonoBehaviour
{
    [Header("�v���C���[�J�������Q��"), SerializeField]
    private Camera playerCamera;

    [Header("�T�C�g���")]
    [SerializeField] private Image createRay;
    [SerializeField] private Image siteUI;
    [SerializeField] private Material greenMat;
    [SerializeField] private Material redMat;
    //�T�C�g�F�ύX
    public void SiteColorGreen(){ siteUI.material = greenMat; }
    public void SiteColorRed(){ siteUI.material = redMat; }

    [Header("���b�N�I�����")]
    [SerializeField] private Image lockonUI;
    [SerializeField] private Image[] LockEnemys = new Image[1];

    //Raycast�ۑ�
    private RaycastHit site_raycast;
    public RaycastHit GetSiteRayCast() { return site_raycast; }

    //�q�b�g���������q�b�g�ʒu
    public class HitData
    {
        public bool hitChack;
        public Vector3 hitPos;
    }
    private HitData hitData;

    public HitData GetSiteHitPoint()
    {
        //�q�b�g�������ǂ����ŕԂ����l��ύX
        if(site_raycast.transform == null)
        {
            hitData.hitPos = Vector3.zero;
        }
        else
        {
            hitData.hitPos = site_raycast.point;
        }

        return hitData;
    }

    public bool SiteHitJudge()
    {
        hitData.hitChack = (site_raycast.transform != null);
        return hitData.hitChack;
    }

    private void Start()
    {
        hitData = new HitData();
        //UI�̈ʒu��Ray�𔭎˂��A�q�b�g���W�v�Z
        CreateSiteRay();
    }

    // Update is called once per frame
    void Update()
    {
        //Ray�쐬
        CreateSiteRay();
        //Ray���q�b�g�������ǂ����ŃT�C�g��material�ύX
        SiteColorChanger(SiteHitJudge());
    }

    private void CreateSiteRay()
    {
        //UI�̈ʒu��Ray�𔭎˂��A�q�b�g���W�v�Z
        Vector2 sitePos = createRay.transform.position;
        Ray siteRay = playerCamera.ScreenPointToRay(sitePos);
        Physics.Raycast(siteRay, out site_raycast);
    }

    private void SiteColorChanger(bool hitFlag)
    {
        //�q�b�g������΁A���Ȃ��������
        if (hitFlag)
        {
            SiteColorGreen();
        }
        else
        {
            SiteColorRed();
        }
    }
}
