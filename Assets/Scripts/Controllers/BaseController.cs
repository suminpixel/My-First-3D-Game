using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseController : MonoBehaviour
{

    [SerializeField]
    protected Vector3 _desPos;

    [SerializeField]
    protected Define.State _state = Define.State.Idle; //플레이어의 기본상태

    [SerializeField]
    protected GameObject _lockTarget;


    virtual public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    //anim.SetBool("attack", true);
                    break;
                case Define.State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    //anim.SetBool("attack", true);
                    //anim.SetFloat("speed", _stat.MoveSpeed);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0); //반복 액션 제거
                    //anim.SetBool("attack", true);
                    break;
                case Define.State.Die:
                    break;
            }
        }
    }

    public abstract void Init();

    void Start() {
        Init();
    }

    void Update()
    {

        switch (_state)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
    }

    protected virtual void UpdateDie() {
    }

    protected virtual void UpdateMoving()
    {
    }

    protected virtual void UpdateIdle()
    {
    }

    protected virtual void UpdateSkill()
    {
    }


}
