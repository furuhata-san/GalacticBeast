using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 
/// 移動のみを統括するスクリプト
/// 
/// </summary>


public class Player_MoveController : MonoBehaviour
{
    [Header("動かすオブジェクト（参照がない場合は自身を自動設定）")]
    [SerializeField]
    private GameObject player;//動かしたいプレイヤー

    //速度データ
    [System.Serializable]
    public class MoveSpeed
    {
        //最高速
        [Header("最高速度"), SerializeField]
        private float max;
        public ref float Ref_Max(){ return ref max; }
        public float GetMax() { return max; }

        //加速度
        [Header("加速度"), SerializeField]
        private float add;
        public ref float Ref_Add() { return ref add; }
        public float GetAdd() { return add; }

        //慣性
        [Header("慣性"), SerializeField]
        private float inertia;
        public ref float Ref_Inertia() { return ref inertia; }
        public float GetInertia() { return inertia; }
    }
    [Header("前進"), SerializeField] private MoveSpeed FrontSpeed;//前進速度データ
    [Header("後退"), SerializeField] private MoveSpeed BackSpeed;//後退速度データ
    [Header("左右"), SerializeField] private MoveSpeed LRSpeed;//後退速度データ

    [Header("移動軸（前後）"), SerializeField] private Vector3 moveVecFB = Vector3.forward;//移動方向（初期値はZ軸前方とする）
    [Header("移動軸（左右）"), SerializeField] private Vector3 moveVecLR = Vector3.left;//移動方向（初期値はX軸前方とする）
    [Header("デッドゾーン"), SerializeField]   private float padDeadZone;//ゲームパッド入力の無視量

    private float nowSpeed_FB;//現在の速度（前進後退）
    private float nowSpeed_LR;//現在の速度（左右移動）
    public void NowSpeedReset()
    {
        nowSpeed_FB = 0;
        nowSpeed_LR = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Player参照がない場合は、自身をプレイヤーとして設定する
        if (player == null) player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームパッドが接続されていない場合は処理なし
        if (Gamepad.current == null)
        {
            print("コントローラーが必要です");
            return;
        }
        //移動処理
        MoveFB();
        MoveLR();
    }

    private void MoveFB()
    {
        float tdt = Time.deltaTime;

        //前進（左スティック上）
        if (Gamepad.current.leftStick.ReadValue().y >= padDeadZone)
        {
            //前進
            Speed_FrontBack(FrontSpeed, true);
        }
        //後退（左スティック下）
        else if (Gamepad.current.leftStick.ReadValue().y <= -padDeadZone)
        {
            //後退
            Speed_FrontBack(BackSpeed, false);
        }
        //静止（入力なし）
        else
        {
            //減速（ブレーキをかける）
            //前進時（後退時）に減速
            if (nowSpeed_FB != 0)
            {
                nowSpeed_FB -= (nowSpeed_FB / FrontSpeed.GetInertia()) * tdt;
            }
        }

        //移動
        this.transform.Translate(moveVecFB * nowSpeed_FB * tdt);
    
    }
  
    void MoveLR()
    {
        float tdt = Time.deltaTime;

        //左平行移動（左スティック左）
        if (Gamepad.current.leftStick.ReadValue().x >= padDeadZone)
        {
            //左
            Speed_LeftRight(LRSpeed, true);
        }
        //右平行移動（左スティック右）
        else if (Gamepad.current.leftStick.ReadValue().x <= -padDeadZone)
        {
            //右
            Speed_LeftRight(LRSpeed, false);
        }
        //静止（入力なし）
        else
        {
            //減速（ブレーキをかける）
            if (nowSpeed_LR != 0)
            {
                nowSpeed_LR -= (nowSpeed_LR / LRSpeed.GetInertia()) * tdt;
            }
        }

        //移動
        this.transform.Translate(moveVecLR * nowSpeed_LR * tdt);
    } 


    void Speed_FrontBack(MoveSpeed data, bool toFront)
    {
        //現在の入力値を取得(Y)
        float stickValue = Gamepad.current.leftStick.ReadValue().y;
        float tdt = Time.deltaTime;

        //前進時（後退時）に減速
        if (nowSpeed_FB < 0 && toFront)
        {
            nowSpeed_FB -= (nowSpeed_FB / data.GetInertia()) * tdt;
        }
        else if (nowSpeed_FB > 0 && !toFront)
        {
            nowSpeed_FB -= (nowSpeed_FB / data.GetInertia()) * tdt;
        }

        //前方へ加速を行う(加速×スティックを倒した量×DeltaTime)
        nowSpeed_FB += data.GetAdd() * stickValue * tdt;

        //最大速度補正
        if(nowSpeed_FB >= data.GetMax())
        {
            nowSpeed_FB = data.GetMax();
        }
        else if(nowSpeed_FB <= -data.GetMax())
        {
            nowSpeed_FB = -data.GetMax();
        }
    }

    void Speed_LeftRight(MoveSpeed data, bool toLeft)
    {
        //現在の入力値を取得(X)
        float stickValue = Gamepad.current.leftStick.ReadValue().x;
        float tdt = Time.deltaTime;

        //逆方向へ移動時に減速
        if (nowSpeed_LR > 0 && toLeft)
        {
            nowSpeed_LR -= (nowSpeed_LR / data.GetInertia()) * tdt;
        }
        else if (nowSpeed_LR < 0 && !toLeft)
        {
            nowSpeed_LR -= (nowSpeed_LR / data.GetInertia()) * tdt;
        }

        //左へ加速を行う(加速×スティックを倒した量×DeltaTime)
        nowSpeed_LR -= data.GetAdd() * stickValue * tdt;

        //最大速度補正
        if (nowSpeed_LR >= data.GetMax())
        {
            nowSpeed_LR = data.GetMax();
        }
        else if (nowSpeed_LR <= -data.GetMax())
        {
            nowSpeed_LR = -data.GetMax();
        }
    }


    //以下Editor用関数
    //public ref GameObject Ref_Player() { return ref player; }

}
