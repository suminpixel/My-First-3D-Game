using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 10; //몬스터의 스캐닝 사정거리

    [SerializeField]
    float _attackRange = 2; //공격 사정거리



    public override void Init()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);


    }

    protected override void UpdateIdle()
    {
        Debug.Log("Monster UpdateIdle");
    }

    protected override void UpdateMoving()
    {
        Debug.Log("Monster UpdateMoving");
    }

    protected override void UpdateSkill()
    {
        Debug.Log("Monster UpdateSkill");
    }

    void OnHitEvent()
    {
        Debug.Log("Monster OnHitEvent");
    }
}
