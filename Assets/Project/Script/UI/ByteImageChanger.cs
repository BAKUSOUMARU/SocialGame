using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ByteImageChanger : MonoBehaviour
{
    public Texture2D image;
    
    public byte[] bytes;

    public Image a;
    // Start is called before the first frame update
    void Start()
    { 
        bytes = image.EncodeToPNG();
        Debug.Log(BitConverter.ToString(bytes));
       
        Sprite reimu = CreateSpriteFromBytes(bytes);
        a.sprite = reimu;
    }
    
    public static Sprite CreateSpriteFromBytes(byte[] bytes)
    {
        //横サイズの判定
        int pos = 16;
        int width = 0;
        for (int i = 0; i < 4; i++)
        {
            width = width * 256 + bytes[pos++];
        }
        //縦サイズの判定
        int height = 0;
        for (int i = 0; i < 4; i++)
        {
            height = height * 256 + bytes[pos++];
        }

        //byteからTexture2D作成
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
    
}
