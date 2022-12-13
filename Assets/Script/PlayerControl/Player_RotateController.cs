using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_RotateController : MonoBehaviour
{
    [Header("動かすRigidBody（参照がない場合は自身を自動設定）")]
    [SerializeField]
    private Rigidbody playerRB;//動かしたいプレイヤー

    //速度データ
    [System.Serializable]
    public class RotateSpeed
    {
        //最高速
        [Header("最高速度"), SerializeField]
        private float max;
        public ref float Ref_Max() { return ref max; }
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

        //回転軸
        [Header("回転軸"), SerializeField]
        private Vector3 rotateVec;
        public ref Vector3 Ref_RotateVec() { return ref rotateVec; }
        public Vector3 GetRotateVec() { return rotateVec; }

        //現在の速度
        //[System.NonSerialized]
        public float nowSpeed = 0;
    }

    [Header("ピッチ（上下）"), SerializeField]//上下に視点を振る速度
    private RotateSpeed pitchSpeed;
    [Header("ピッチ逆回転")] public bool pitchReverse = false;

    [Header("ヨー（左右）"), SerializeField]//左右に視点を振る速度
    private RotateSpeed yawSpeed;


    [Header("デッドゾーン"), SerializeField] private float padDeadZone;//ゲームパッド入力の無視量


    // Start is called before the first frame update
    void Start()
    {
        //参照がない場合、自身を参照
        if (playerRB == null)
        {
            //RigidBodyがない場合は通知
            if(!(playerRB = this.GetComponent<Rigidbody>()))
            {
                print("RigidBodyがアタッチ・参照されていません");
            }
        }

        playerRB.interpolation = RigidbodyInterpolation.Extrapolate;
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

        //データを利用して移動を行う
        RotateUD();
        RotateLR();

        //Z軸を０度に近づける
        RotateZ_BringCloserZero(true, Time.deltaTime);
    }

    void RotateUD()
    {
        float tdt = Time.deltaTime;

        //上向き（右スティック上）
        float stickValue = -Gamepad.current.rightStick.ReadValue().y;
        if (pitchReverse)
        {
            //リバース
            stickValue = -stickValue;
        }

        if (Gamepad.current.rightStick.ReadValue().y >= padDeadZone)
        {
            //前進
            Speed(pitchSpeed, stickValue, true);
        }
        //下向き（右スティック下）
        else if (Gamepad.current.rightStick.ReadValue().y <= -padDeadZone)
        {
            //後退
            Speed(pitchSpeed, stickValue, false);
        }
        //静止（入力なし）
        else
        {
            //減速（ブレーキをかける）
            //前進時（後退時）に減速
            if (pitchSpeed.nowSpeed != 0)
            {
                pitchSpeed.nowSpeed -= (pitchSpeed.nowSpeed / pitchSpeed.GetInertia()) * tdt;
            }
        }

        //回転
        Quaternion turn = Quaternion.Euler(pitchSpeed.GetRotateVec() * pitchSpeed.nowSpeed * tdt);
        playerRB.MoveRotation(playerRB.rotation * turn);
    }

    void RotateLR()
    {
        float tdt = Time.deltaTime;

        //左向き（右スティック左）
        float stickValue = Gamepad.current.rightStick.ReadValue().x;
        if (Gamepad.current.rightStick.ReadValue().x >= padDeadZone)
        {
            //左回転
            Speed(yawSpeed, stickValue, true);
        }
        //右向き（右スティック右）
        else if (Gamepad.current.rightStick.ReadValue().x <= -padDeadZone)
        {
            //右回転
            Speed(yawSpeed, stickValue, false);
        }
        //静止（入力なし）
        else
        {
            //減速（ブレーキをかける）
            //前進時（後退時）に減速
            if (yawSpeed.nowSpeed != 0)
            {
                yawSpeed.nowSpeed -= (yawSpeed.nowSpeed / yawSpeed.GetInertia()) * tdt;
            }
        }

        //回転
        Quaternion turn = Quaternion.Euler(yawSpeed.GetRotateVec() * yawSpeed.nowSpeed * tdt);
        playerRB.MoveRotation(playerRB.rotation * turn);
    }

    void Speed(RotateSpeed data, float stickValue, bool to_UP_or_LEFT)
    {
        //現在の入力値を取得(Y)
        float tdt = Time.deltaTime;

        //前進時（後退時）に減速
        if (data.nowSpeed < 0 && to_UP_or_LEFT)
        {
            data.nowSpeed -= (data.nowSpeed / data.GetInertia()) * tdt;
        }
        else if (data.nowSpeed > 0 && !to_UP_or_LEFT)
        {
            data.nowSpeed -= (data.nowSpeed / data.GetInertia()) * tdt;
        }

        //前方へ加速を行う(加速×スティックを倒した量×DeltaTime)
        data.nowSpeed += data.GetAdd() * stickValue * tdt;

        //最高（最低）速度補正
        if (data.nowSpeed >= data.GetMax())
        {
            data.nowSpeed = data.GetMax();
        }
        else if (data.nowSpeed <= -data.GetMax())
        {
            data.nowSpeed = -data.GetMax();
        }
    }

    void RotateZ_BringCloserZero(bool use, float tdt)
    {
        //使用しない場合は処理を行わない
        if (!use) return;

        //Z軸がどれほど傾いているかを計算する
        float zVal = transform.eulerAngles.z;
        //-180～180に補正
        if (zVal > 180)
        {
            zVal -= 360;
        }

        //今回の１フレーム当たりの回転量を求める
        float rotZ = zVal * tdt;

        //XY軸の回転はそのまま
        //float rotX = transform.localEulerAngles.x;
        //float rotY = transform.localEulerAngles.y;

        //回転を行う
        Quaternion turn = Quaternion.Euler(new Vector3(0, 0, -rotZ));
        playerRB.MoveRotation(playerRB.rotation * turn);
    }
}
