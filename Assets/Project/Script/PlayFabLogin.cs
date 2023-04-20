using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using save;

public class PlayFabLogin : MonoBehaviour
{
    private string _id;
    
    void Start()
    {
        CheckAccountExistence(_id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


    public void CheckAccountExistence(string customId)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = false,
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnAccountExistenceCheckSuccess, OnAccountExistenceCheckFailure);
    }

    private void OnAccountExistenceCheckSuccess(LoginResult result)
    {
        _id = CreateID().ToString();
        CheckAccountExistence(_id);
    }

    private void OnAccountExistenceCheckFailure(PlayFabError error)
    {
        
    }

    void CreateAccount()
    {
        int ID = CreateID();
        
        
    }
    
     int CreateID()
    {
        int min = 100000000;
        int max = 999999999;
        int randomNumber = Random.Range(min, max + 1);

        return randomNumber;
    }
}
