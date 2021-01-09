using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//캐릭터 애니메이션 전환 방법
// 1. 코드로 anim.Play("NAME") 전환하는 법 => 디테일한 전환이 어려울수 있다.
// 2. speed, isAttack 등의 key 로 직접 제어 => 스킬, 애니메이션 종륟가 많아졌을때 제어 복잡
// 선택할 수 있다.

public class PlayerController : BaseController
{
    
    int _layerMask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;
    bool _stopSkill = false;

    public override void Init()
    {

        WorldObjectType = Define.WorldObject.Player;
        //Input Manager 구독

        _stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction -= OnMouseEvent; //다른 곳에서 구독하고 있는 경우를 방지하기 위해 우선 -- 후 +
        Managers.Input.MouseAction += OnMouseEvent;

        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        /*
        * 키보드의 경우..
        * Managers.Input.KeyAction -= OnKeyboard;
        * Managers.Input.KeyAction += OnKeyboard;
        */
    }

    void OnHitEvent()
    {
        //공격 이벤트
        //tip ? 배틀매니저를 사용하여 양쪽의 stat을 받아 만드는 방법도 있으나, 그렇지 않은경우 몬스터(상대방)옵젝이 알아서 데미지를 갱신하는게 좋음 (몬스터 컨트롤러)

        if (_lockTarget != null) {

            Stat targetStat = _lockTarget.GetComponent<Stat>();
            PlayerStat myStat = gameObject.GetComponent<PlayerStat>();

            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            Debug.Log(damage);

            targetStat.Hp -= damage;
        }


        Debug.Log("OnHitEvent");                    
        //때리고있었다면 Idle 로 가지 않고 계속 때리고 걷다가였으면 idle
        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }

    protected override void UpdateSkill() {
        //Debug.Log("update skill");
        if (_lockTarget != null) 
        {
            //타겟 방향으로 플레이어 방향(벡터ㄹ)를 돌림
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }

    }

    protected override void UpdateMoving() {

        //몬스터가 내 사정거리 안에 들어오면 공격
        if (_lockTarget != null)
        {
            _desPos = _lockTarget.transform.position;
            float distance = (_desPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }

        //이동처리
        Vector3 dir = _desPos - transform.position; //클릭한 위치 - 현재 사용자의 위치 = 방향 벡터
        dir.y = 0;

        if (dir.magnitude < 0.1f) {
            //거리가 클릭 위치와 가까워졌다면 멈춤
            State = Define.State.Idle;
        } else {
            //길찾기를 하는 컴포넌트 => NavMeshAgent

            //NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            //float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude); //min~max 사이의 값
            //nma.Move(dir.normalized * moveDist);//Navmeshagent.move => mash 중 내가 갈수있는 지역에만 접근 가능

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);

            //Block 인 Layer에 닿으면 멈춤처리 
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block"))) {
                if (Input.GetMouseButton(0) == false) {
                    State = Define.State.Idle;
                }
                return;
            }

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

        }


        /*
        if(_state == Define.State.Moving){//움직이고 있다면 
            //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime); //0에서 1사이의 값을 섞음 (Lerp)
            //Animator anim = GetComponent<Animator>();
            //anim.SetFloat("wait_run_ratio", wait_run_ratio); //블랜드한 애니메이션의 변수를 조작 

           // anim.Play("WAIT_RUN");

            
        }
        */

    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }


    // 정지/이동 상태일때 

    void OnMouseEvent_IdleRun(Define.MouseEvent evt) {


        RaycastHit hit;

        // 마우스 클릭시 레이 캐스팅
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _layerMask); //래이케이스팅 저장 => 어떤 클릭을 햇는지

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    //몬스터 타겟인지 땅클릭인지 구분
                    if (raycastHit)
                    {
                        _desPos = hit.point;
                        State = Define.State.Moving;
                        Debug.Log($"레이캐스트 : {hit.collider.gameObject.tag} / {hit.collider.gameObject.name}");
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster){
                            Debug.Log("Monster Click!");
                            _lockTarget = hit.collider.gameObject; //몬스터 타겟 저장
                            //TODO : 몬스터 커리시 공격처리
                        }
                        else
                        {
                            _lockTarget = null;
                            Debug.Log("Not Monster Click! ");
                        }
                    }


                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _desPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;

        }

    }
    /*
void OnKeyboard()
{
    _yAngle += Time.deltaTime * _speed;

    //Rotate 방법
        //1. 절대 회전 값으로 회전
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        //2. 특정 축 기준으로 회전 +- delta 
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        //3. 쿼터니언 사용 : x, y, z, w
        // 쿼터니언이란 ? 3차원 그래픽에서 회전을 표현할 때 행렬대신 사용하는 수학적 개념. 4차원 복소수 공간의 벡터로서 q = <w, z, y, z> 로 나타낼 수 있다. 행렬연산에 비해 속도가 빠르고 차지하는 메모리의양이 적도 오류 확률이 낮다.
        // transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

    if(Input.GetKey(KeyCode.W)){
        //transform.rotation = Quaternion.LookRotation(Vector3.forward);

        //Slerp(from, to , 중간값 단위 포인트 0~1.0f 사이)
        transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Vector3.forward), 0.2f);
        transform.position += Vector3.forward * Time.deltaTime * _speed;
    }
    if(Input.GetKey(KeyCode.S)){

        transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Vector3.back), 0.2f);
        transform.position += Vector3.back * Time.deltaTime * _speed;
    }
    if(Input.GetKey(KeyCode.A)){

        transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Vector3.left), 0.2f);
        transform.position += Vector3.left * Time.deltaTime * _speed;
    }
    if(Input.GetKey(KeyCode.D)){

        transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Vector3.right), 0.2f);
        transform.position += Vector3.right * Time.deltaTime * _speed;
    }
    _moveToDest = false;
}
*/
}
