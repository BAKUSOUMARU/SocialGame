using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeUIView : MonoBehaviour
{
    [SerializeField] private TMP_Text _userNameText;
    
    [SerializeField] private TMP_Text _virtualCurrencyText;


    public void ChangeName(string usetname)
    {
        _userNameText.text = usetname;
    }

    public void ChangeVirtualCurrency(int value)
    {
        _virtualCurrencyText.text = "魔法石所持数" + value.ToString();
        Debug.Log("魔法石所持数" + value);
    }
}
