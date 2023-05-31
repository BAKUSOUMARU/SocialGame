using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeUIView : MonoBehaviour
{
    [SerializeField] private TMP_Text _userNameText;


    public void ChangeName(string usetname)
    {
        _userNameText.text = usetname;
    }
}
