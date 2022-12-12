using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_ShotManager : MonoBehaviour
{
    [Header("ショットシステムをすべて参照させる")]
    [SerializeField]
    private ShotSystem_Base[] shotSystems = new ShotSystem_Base[1];

    private int nowWeapon;


    // Start is called before the first frame update
    void Start()
    {
        nowWeapon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            //現在の武器の変更処理
            shotSystems[nowWeapon].ShotChanged();

            //武器変更（番号増加）
            ++nowWeapon;
            if(nowWeapon >= shotSystems.Length)
            {
                nowWeapon = 0;
            }
        }

        //ショットシステムの処理
        shotSystems[nowWeapon].ShotPlay();
    }
}
