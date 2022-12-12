using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSystem_Base : MonoBehaviour
{
    public virtual void ShotPlay()
    {
        print("ShotSystem : 関数をオーバーライドしてコードを書いてください");
    }

    public virtual void ShotChanged()
    {
        print("ShotSystem : 関数をオーバーライドしてコードを書いてください");
    }

}
