using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile_Creater : MonoBehaviour
{
    [Header("複製するミサイルのPrefab"), SerializeField]
    private PlayerMissile_Controller missilePrefab;

    [Header("エイム情報"), SerializeField]
    private Player_Aim aimMgr;

    public void Shoot()
    {
        GameObject go = Instantiate(missilePrefab.gameObject);
        go.transform.position = this.transform.position;
        go.transform.rotation = this.transform.rotation;

        if (aimMgr.SiteHitJudge()) {
            if (aimMgr.GetSiteRayCast().transform.tag == "Enemy")
            {
                go.GetComponent<PlayerMissile_Controller>().
                    SetTarget(aimMgr.GetSiteRayCast().transform.gameObject);
            }
            else
            {
                go.GetComponent<PlayerMissile_Controller>().
                    SetTarget(aimMgr.GetSiteHitPoint().hitPos);
            }
        }
        else
        {
            go.GetComponent<PlayerMissile_Controller>().
                   SetTarget(10000 * go.transform.forward);
        }
    }
}
