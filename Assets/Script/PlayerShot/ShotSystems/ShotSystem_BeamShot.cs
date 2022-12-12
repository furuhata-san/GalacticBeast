using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotSystem_BeamShot : ShotSystem_Base
{
    [Header("パーティクルシステムの挿入"), SerializeField]
    private ParticleSystem[] shots = new ParticleSystem[1];
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
            shots[i].Play();
        }
        shotting = true;
    }

    private void ShotEnd()
    {
        for (int i = 0; i < shots.Length; ++i)
        {
            shots[i].Stop();
        }
        shotting = false;
    }
}
