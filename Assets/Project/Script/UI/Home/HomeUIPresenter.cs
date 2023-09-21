using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class HomeUIPresenter : MonoBehaviour
{

    [SerializeField] private HomeUiSet _homeUiSet;

    [SerializeField] private HomeUIView _homeUIView;
    
    void Start()
    {
        _homeUiSet.ObserveEveryValueChanged(x => _homeUiSet.UserName)
            .Subscribe(x => _homeUIView.ChangeName(x));
    }
}
