using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Crashing : MonoBehaviour
{
    [Header("Player_MoveControllerを参照"), SerializeField]
    private Player_MoveController PMC;

    [Header("衝突対象"), SerializeField]
    private string[] targetTagName = new string[1];

    [Header("床との衝突時に跳ね返る速度"), SerializeField]
    private float knockbackPower;

    // Start is called before the first frame update
    void Start()
    {
        if(PMC == null)
        {
            if(!(PMC = GetComponent<Player_MoveController>()))
            {
                print("プレイヤーコントローラーがアタッチされていません");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //タグと一致しない場合はreturn
        bool flag = false;
        for (int i = 0; i < targetTagName.Length; ++i){
            if (collision.transform.tag == targetTagName[i])
            {
                flag = true;
                break;
            }
        }
        if (!flag) return;

        //Rigidbody静止
        Rigidbody myrb;
        if (myrb = this.GetComponent<Rigidbody>())
        {
            myrb.velocity = Vector3.zero;
        }

        //接触地点を取得し、逆方法へノックバック
        Vector3 crachPos = collision.contacts[0].point;
        Vector3 knockbackVec = this.transform.position - crachPos;//接触位置が自身とどれほど離れているか

        // 逆方向にはねる
        PMC.NowSpeedReset();
        Vector3 knockbackForce = knockbackPower * knockbackVec.normalized;
        myrb.AddForce(knockbackForce, ForceMode.Impulse);
    }

}
