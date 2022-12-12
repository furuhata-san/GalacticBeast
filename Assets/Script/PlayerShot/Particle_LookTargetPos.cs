using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_LookTargetPos : MonoBehaviour
{
    [Header("�G�C�����̎Q��"), SerializeField]
    private Player_Aim aimPos;

    [Header("��]��"), SerializeField]
    private Vector3Int transformUp;
    private Quaternion defLookAngle;//�����l�̕ۑ�

    // Start is called before the first frame update
    void Start()
    {
        defLookAngle = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //�G�C�����q�b�g�������ǂ����Ō���������ύX
        if (aimPos.SiteHitJudge())
        {
            Vector3 targetPos = aimPos.GetSiteHitPoint().hitPos;
            this.transform.LookAt(targetPos);
        }
        else
        {
            this.transform.rotation = defLookAngle;
        }
    }
}
