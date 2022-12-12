using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_ShotManager : MonoBehaviour
{
    [Header("�V���b�g�V�X�e�������ׂĎQ�Ƃ�����")]
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
            //���݂̕���̕ύX����
            shotSystems[nowWeapon].ShotChanged();

            //����ύX�i�ԍ������j
            ++nowWeapon;
            if(nowWeapon >= shotSystems.Length)
            {
                nowWeapon = 0;
            }
        }

        //�V���b�g�V�X�e���̏���
        shotSystems[nowWeapon].ShotPlay();
    }
}
