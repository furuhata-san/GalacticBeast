using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_StickAnim : MonoBehaviour
{
    [Header("スティックを倒した量に応じてハンドルを傾ける")]
    [Header("コックピット内のハンドルを参照する"), SerializeField]
    private GameObject cockpitStick;

    [Header("オブジェクトの「X:最小角度 Y:最大角度」")]
    [SerializeField] private Vector2 angleLimitX;
    [SerializeField] private Vector2 angleLimitZ;

    private void Start()
    {
        if (!cockpitStick)
        {
            print("コックピットのハンドルを参照してください。");
        }
    }

    private void Update()
    {
        //スティック入力量を取得
        Vector2 stickValue = Gamepad.current.rightStick.ReadValue();

        //負の値が入力されていた場合は補正
        if (angleLimitX.x < 0) angleLimitX.x = -angleLimitX.x;
        if (angleLimitZ.x < 0) angleLimitZ.x = -angleLimitZ.x;

        //ハンドル縦（前後移動）
        //X軸回転
        float rotateX = 0;
        //上入力
        if (stickValue.y > 0)
        {
            rotateX = angleLimitX.y * stickValue.y;
        }
        //下入力
        else
        {
            rotateX = angleLimitX.x * stickValue.y;
        }

        //ハンドル横（左右移動）
        //Z軸回転
        float rotateZ = 0;
        //上入力
        if (stickValue.x < 0)
        {
            rotateZ = angleLimitZ.y * -stickValue.x;
        }
        //下入力
        else
        {
            rotateZ = angleLimitZ.x * -stickValue.x;
        }

        //回転
        Vector3 stickAngle = new Vector3(rotateX, 0, rotateZ);
        cockpitStick.transform.localRotation = Quaternion.Euler(stickAngle);
    }
}
