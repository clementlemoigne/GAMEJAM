using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateHandler : MonoBehaviour {

    public delegate void OnDamageCar(float value);
    public static OnDamageCar ChangeGlobalspeed;

    public delegate void OnTunnelEffect(Texture2D texture);
    public static OnTunnelEffect ChangeGlobaltexture;

    public void OnDamage(float value)
    {
        ChangeGlobalspeed(value);
    }

    public void OnTunnel(Texture2D texture)
    {
        ChangeGlobaltexture(texture);
    }

}
