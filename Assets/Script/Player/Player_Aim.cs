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
    [SerializeField] private Image rockonUI;
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
            hitData.hitChack = false;
            hitData.hitPos = Vector3.zero;
        }
        else
        {
            hitData.hitChack = true;
            hitData.hitPos = site_raycast.point;
        }

        return hitData;
    }

    private void Start()
    {
        //UI�̈ʒu��Ray�𔭎˂��A�q�b�g���W�v�Z
        Vector2 sitePos = createRay.transform.position;
        Ray siteRay = playerCamera.ScreenPointToRay(sitePos);
        Physics.Raycast(siteRay, out site_raycast);
    }

    // Update is called once per frame
    void Update()
    {
        CreateSiteRay();
    }

    private void CreateSiteRay()
    {
        //UI�̈ʒu��Ray�𔭎˂��A�q�b�g���W�v�Z
        Vector2 sitePos = siteUI.transform.position;
        Ray siteRay = playerCamera.ScreenPointToRay(sitePos);
        Physics.Raycast(siteRay, out site_raycast);
    }
}
