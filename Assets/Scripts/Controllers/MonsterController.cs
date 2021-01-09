using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 10; //몬스터의 스캐닝 사정거리

    [SerializeField]
    float _attackRange = 2; //공격 사정거리


    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;

        _stat = gameObject.GetComponent<Stat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);


    }

    protected override void UpdateIdle()
    {
        Debug.Log("Monster UpdateIdle");
        //TODO: 매니저가 생기면 옮길것.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        float distance = (player.transform.position - transform.position).magnitude;

        //사정거리 안에 들어오면 따라가서 
        if (distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        
        }
    }

    protected override void UpdateMoving()
    {
        Debug.Log("Monster UpdateMoving");

        //사정거리 안에 들어오면 공격
        if (_lockTarget != null)
        {
            _desPos = _lockTarget.transform.position;
            float distance = (_desPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                //밀리지 않게 현재 좌표로
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = _desPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(_desPos);
            nma.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

    }

    protected override void UpdateSkill()
    {
        //Debug.Log("Monster UpdateSkill");
        //Debug.Log("update skill");
        if (_lockTarget != null)
        {
            //타겟 방향으로 플레이어 방향(벡터ㄹ)를 돌림
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        Debug.Log("Monster OnHitEvent");

        if (_lockTarget != null)
        {

            Stat targetStat = _lockTarget.GetComponent<Stat>();
            int damage = Mathf.Max(0, _stat.Attack - targetStat.Defense);
            targetStat.Hp -= damage;

            if (targetStat.Hp <= 0)
            {
                //난죽택
                Managers.Game.Despawn(targetStat.gameObject);
            }

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }

        }
        else {
            State = Define.State.Idle;
        }
    

    
    }
}
