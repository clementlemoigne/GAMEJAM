using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteContainer : MonoBehaviour {

    public Sprite[] sprites;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ChangeSprite(int id)
    {
        image.sprite = sprites[id];
    }

}
