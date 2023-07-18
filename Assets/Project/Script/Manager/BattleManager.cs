using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private List<Character> _characters;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var partymember in PartyManager.Instance.PartyList)
        {
            if (partymember.IsCharacter)
            {
                _characters.Add((Character)partymember);
                Debug.Log(partymember.IsCharacter);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
