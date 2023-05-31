using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PartySelectPresenter : MonoBehaviour
{
    [SerializeField] private PartySelectView _partySelectView;
    // Start is called before the first frame update
    void Start()
    {
        PartyManager.Instance.ObserveEveryValueChanged(x => PartyManager.Instance.PartyList)
            .Subscribe(x =>
                _partySelectView.SpriteChange(x[0].Icon, x[1].Icon, x[2].Icon));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
