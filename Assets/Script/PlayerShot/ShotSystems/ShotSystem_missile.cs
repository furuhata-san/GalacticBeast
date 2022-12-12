using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotSystem_missile : ShotSystem_Base
{
    [Header("ƒVƒXƒeƒ€‚Ì‘}“ü"), SerializeField]
    private PlayerMissile_Creater[] shots = new PlayerMissile_Creater[1];
    private bool shotting = false;

    public override void ShotPlay()
    {
        if (Gamepad.current.rightShoulder.isPressed)
        {
            if (!shotting)
            {
                ShotStart();
            }
        }
        else
        {
            ShotEnd();
        }
    }

    public override void ShotChanged()
    {
        ShotEnd();
    }

    private void ShotStart()
    {
        for (int i = 0; i < shots.Length; ++i)
        {
            shots[i].Shoot();
        }
        shotting = true;
    }

    private void ShotEnd()
    {
        shotting = false;
    }
}
