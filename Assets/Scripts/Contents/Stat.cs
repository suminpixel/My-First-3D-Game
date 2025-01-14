﻿using UnityEngine;
using System.Collections;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        //원래 여기서 서버에서 데이터 받아와야하나
        //일단 더미데이터로 쑤셔넣기
        _level = 1;
        _hp = 50;
        _maxHp = 50;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
    }

    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }


    protected virtual void OnDead(Stat attacker)
    {
        Debug.Log("onDead...");
        PlayerStat playerStat = attacker as PlayerStat; //경험치는 플레이어에게만 있으니 캐스팅 
        if (playerStat != null){
            Debug.Log("Exp + 150...");
            playerStat.Exp += 150;

        }
        Managers.Game.Despawn(gameObject);


    }
}
