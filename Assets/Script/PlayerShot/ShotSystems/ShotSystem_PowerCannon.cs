using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotSystem_PowerCannon : ShotSystem_Base
{
    [Header("パーティクルシステムの挿入"), SerializeField]
    private ParticleSystem[] shots = new ParticleSystem[1];

    [Header("リロード時間"), SerializeField]
    private float reloadTime;
    private float nowReloadCount;

    private void Start()
    {
        nowReloadCount = 0;
    }

    private void Update()
    {
        nowReloadCount -= Time.deltaTime;
        if(nowReloadCount <= 0)
        {
            nowReloadCount = 0;
        }
    }

    public override void ShotPlay()
    {
        if (Gamepad.current.rightShoulder.isPressed)
        {
            if (nowReloadCount <= 0)
            {
                ShotStart();
            }
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
        nowReloadCount = reloadTime;
    }

    private void ShotEnd()
    {
        //nop
    }
}
