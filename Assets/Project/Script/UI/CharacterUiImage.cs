using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUiImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image.sprite = CharacterManager.Instance._getCharacters[0].CharacterSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
