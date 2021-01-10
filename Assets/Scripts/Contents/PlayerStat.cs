using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//stat 확장
public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp {
        get { return  _exp; }
        set {
           
            _exp = value; //레벨업 체크

            
            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                level++;
            }

            if (level != Level)
            {
                Debug.Log("Level Up!");
                Level = level;
                SetStat(Level);
            }

        }
    }
    public int Gold { get { return _gold; } set { _gold = value; } }


    private void Start()
    {  
        _level = 1;
        _moveSpeed = 5.0f;
        _gold = 0;
        _hp = 0;
        _maxHp = 0;
        _exp = 0;
        
        SetStat(_level);
    }

    public void SetStat(int level)
    {
        Debug.Log("--start set stat---");
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Debug.Log(dict);
        Data.Stat stat = dict[level];


        _hp = stat.maxHp; //hp , exp 가 저장되는 시스템이라면 별도 저장파일에서 꺼내오는 작업 필요
        _maxHp = stat.maxHp;
        _attack = stat.attack;

    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");

    }
}
