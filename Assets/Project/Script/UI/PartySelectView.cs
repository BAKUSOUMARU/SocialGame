using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectView : MonoBehaviour
{
    [SerializeField] private Image[] _image;

    public void SpriteChange(Sprite party1,Sprite party2,Sprite party3)
    {
        _image[0].sprite = party1;
        _image[1].sprite = party2;
        _image[2].sprite = party3;
    }
}
