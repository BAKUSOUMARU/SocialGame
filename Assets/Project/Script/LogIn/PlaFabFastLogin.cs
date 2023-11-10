using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PlaFabFastLogin : MonoBehaviour
{
    [SerializeField] 
    private GameObject _loginUI;
    
    [SerializeField]
    private GameObject _accountnameSetUI;

    [SerializeField] 
    private TMP_Text _text;

    [SerializeField] 
    private string sceneChangeName ="HomeScene";

    private bool isLogin = false;

    private void Awake()
    {
        PlayFabcustomidLogin.FarstLoginTure += AccountSetUP;
        PlayFabcustomidLogin.LoginFalse += LoginFalse;
        PlayFabcustomidLogin.LoginTure += rodeseen;
    }

    private void OnDisable()
    {
        PlayFabcustomidLogin.FarstLoginTure -= AccountSetUP;
        PlayFabcustomidLogin.LoginFalse -= LoginFalse;
        PlayFabcustomidLogin.LoginTure -= rodeseen;
    }

    public async void Login()
    {
        if (isLogin)return;
        isLogin = true;
        await PlayFabcustomidLogin.Set();
    }

    void AccountSetUP()
    {
        _loginUI.SetActive(false);
        _accountnameSetUI.SetActive(true);
        isLogin = false;
    }

    void LoginFalse()
    {
        _text.text = "ログインに失敗しました";
        isLogin = false;
    }

    void rodeseen()
    {
        SceneChanger.ChangeScene(sceneChangeName);
        isLogin = false;
    }
}
