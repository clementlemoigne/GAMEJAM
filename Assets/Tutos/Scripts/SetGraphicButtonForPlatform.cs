using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGraphicButtonForPlatform : MonoBehaviour {

    public Sprite targetMobile;
    public Sprite targetDesktop;

    // Use this for initialization
    void Start () {
        Image image = GetComponent<Image>();

#if UNITY_ANDROID || UNITY_IOS

        image.sprite = targetMobile;

#endif

#if UNITY_STANDALONE || UNITY_WEBGL

        image.sprite = targetDesktop;

#endif
        image.enabled = true;
    }

}
