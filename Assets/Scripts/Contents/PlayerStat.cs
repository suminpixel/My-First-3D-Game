using UnityEngine;
using System.Collections;

//stat 확장
public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp { get { return  _exp; } set { _exp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }


    private void Start()
    {
        //원래 여기서 서버에서 데이터 받아와야하나
        //일단 더미데이터로 쑤셔넣기
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 5;
        _moveSpeed = 5.0f;
        _exp = 0;
        _gold = 0;
    }
}
