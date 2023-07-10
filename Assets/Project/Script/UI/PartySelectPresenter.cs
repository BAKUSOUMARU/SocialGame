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
        for (int i = 0; i < 3; i++)
        {
            int n = i;
            PartyManager.Instance.ObserveEveryValueChanged(x => PartyManager.Instance.PartyList[n])
                .Subscribe(x =>
                    _partySelectView.SpriteChange(PartyManager.Instance.PartyList[0].Icon,
                        PartyManager.Instance.PartyList[1].Icon,
                        PartyManager.Instance.PartyList[2].Icon));
        }
    }
}
