using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Status
{
    [SerializeField] private string _englishname;
    public string Englishname => _englishname;

    [SerializeField] private string _japaneseNeme;

    public string JapaneseName => _japaneseNeme;
    
    [SerializeField] private int _level;
    public int Level => _level;
    
    [SerializeField] private int _maxHp;
    public int MaxHP => _maxHp;
    public int Hp { get; set; }

    [SerializeField] private int _atk;
    public int Atk => _atk;

    [SerializeField] private int _speed;
    public int Speed => _speed;

    [SerializeField] private int _lucky;
    public int Lucky => _lucky;
    

    public void Initialize()
    {
        Hp = _maxHp;
    }

    public bool IsDead()
    {
        return Hp <= 0;
    }

    public void Set(int level, int maxhp, int atk, int speed, int lucky, string englishname,string japansename)
    {
        _englishname = englishname;
        _japaneseNeme = japansename;
        _level = level;
        _maxHp = maxhp;
        _atk = atk;
        _speed = speed;
        _lucky = lucky;
    }
}
