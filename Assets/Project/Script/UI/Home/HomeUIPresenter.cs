using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;


public class HomeUIPresenter : MonoBehaviour
{

    [FormerlySerializedAs("_homeUiSet")] [SerializeField] private HomeUiiModel homeUiiModel;

    [SerializeField] private HomeUIView _homeUIView;
    
    void Start()
    {
        homeUiiModel.ObserveEveryValueChanged(x => homeUiiModel.UserName)
            .Subscribe(x => _homeUIView.ChangeName(x));

        homeUiiModel.ObserveEveryValueChanged(x => homeUiiModel.VirtualCurrency)
            .Subscribe(x => _homeUIView.ChangeVirtualCurrency(x));
    }
}
