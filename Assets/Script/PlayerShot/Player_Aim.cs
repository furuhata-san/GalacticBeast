using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Aim : MonoBehaviour
{
    [Header("プレイヤーカメラを参照"), SerializeField]
    private Camera playerCamera;

    [Header("サイト情報")]
    [SerializeField] private Image createRay;
    [SerializeField] private Image siteUI;
    [SerializeField] private Material greenMat;
    [SerializeField] private Material redMat;
    //サイト色変更
    public void SiteColorGreen(){ siteUI.material = greenMat; }
    public void SiteColorRed(){ siteUI.material = redMat; }

    [Header("ロックオン情報")]
    [SerializeField] private Image lockonUI;
    [SerializeField] private Image[] LockEnemys = new Image[1];

    //Raycast保存
    private RaycastHit site_raycast;
    public RaycastHit GetSiteRayCast() { return site_raycast; }

    //ヒットしたか＆ヒット位置
    public class HitData
    {
        public bool hitChack;
        public Vector3 hitPos;
    }
    private HitData hitData;

    public HitData GetSiteHitPoint()
    {
        //ヒットしたかどうかで返す数値を変更
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
        //UIの位置にRayを発射し、ヒット座標計算
        CreateSiteRay();
    }

    // Update is called once per frame
    void Update()
    {
        //Ray作成
        CreateSiteRay();
        //Rayがヒットしたかどうかでサイトのmaterial変更
        SiteColorChanger(SiteHitJudge());
    }

    private void CreateSiteRay()
    {
        //UIの位置にRayを発射し、ヒット座標計算
        Vector2 sitePos = createRay.transform.position;
        Ray siteRay = playerCamera.ScreenPointToRay(sitePos);
        Physics.Raycast(siteRay, out site_raycast);
    }

    private void SiteColorChanger(bool hitFlag)
    {
        //ヒットしたら緑、しなかったら赤
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
