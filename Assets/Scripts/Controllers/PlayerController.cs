using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
class Tank
{
    public float speed = 10.0f;
    Player player; //포함 관계 : Nested Prefeb(중첩된 프리펫)
}
class Player {

}
*/

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    //float _speed = 10.0f; //스피드 설정

    PlayerStat _stat;

    //bool _moveToDest = false; //움직임 여부
    //float _yAngle = 0.0f;
    //float wait_run_ratio = 0;

    Vector3 _desPos; //플레이어의 위치 데이터 (벡터3)

    void Start()
    {
        //Input Manager 구독/

        /*
        Managers.Input.KeyAction -= OnKeyboard; 
        Managers.Input.KeyAction += OnKeyboard;
        */

        _stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction -= OnMouseClicked; //다른 곳에서 구독하고 있는 경우를 방지하기 위해 우선 -- 후 +
        Managers.Input.MouseAction += OnMouseClicked;

        //TEMP : 아래 코드들은 테스트용 코드 
        //Tank tank1 = new Tank();
        //Managers.Resource.Instantiate("UI/UI_Button"); //UI 폴더에 있는 cs 파일 구독
        //Managers.UI.ShowPopupUI<UI_Button>("UI_Button");
        //Managers.UI.ShowSceneUI<UI_Inven>();

    }

    public enum PlayerState {    //다양한 애니메이션 상태 state 를 정리한것 : State Machine
        Die,
        Moving,
        Idle,
        Skill, //공격, 치유 등 (추후 분리하던지..)
        //Channeling,
        //Jumping,
        //Falling,
    }

    PlayerState _state = PlayerState.Idle; //플레이어의 기본상태 죽음

    void UpdateDie() {
        // TODO : 죽었을 때 처리 아무것도 못함
    }

    void UpdateMoving() {

        Vector3 dir = _desPos - transform.position; //클릭한 위치 - 현재 사용자의 위치 = 방향 벡터

        if (dir.magnitude < 0.1f) {
            //거리가 클릭 위치와 가까워졌다면 멈춤
            _state = PlayerState.Idle;
        } else {
            // 길찾기를 하는 컴포넌트 => NavMeshAgent
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude); //min~max 사이의 값

            //Navmeshagent.move => mash 중 내가 갈수있는 지역에만 접근 가능
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);

            //Block 인 Layer에 닿으면 멈춤처리 
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block"))) {
                _state = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

        }

        // 애니메이션
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태에 대한 정보를 넘겨준다
        anim.SetFloat("speed", _stat.MoveSpeed);

        /*
        if(_state == PlayerState.Moving){//움직이고 있다면 
            //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime); //0에서 1사이의 값을 섞음 (Lerp)
            //Animator anim = GetComponent<Animator>();
            //anim.SetFloat("wait_run_ratio", wait_run_ratio); //블랜드한 애니메이션의 변수를 조작 

           // anim.Play("WAIT_RUN");

            
        }
        */

    }

    void UpdateIdle() {

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);

        //ㅌㅊwait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime); //0에서 1사이의 값을 섞음
        //anim.SetFloat("wait_run_ratio", wait_run_ratio); //블랜드한 애니메이션의 변수를 조작
        //anim.Play("WAIT_RUN");

    }

    void Update() {

        switch (_state) {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
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

    int _layerMask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
    void OnMouseClicked(Define.MouseEvent evt){

        if(_state == PlayerState.Die){
            return;
        }
 
        Debug.Log($"마우스 클릭 이벤트 실행 : OnMouseClicked");

        // 마우스 클릭시 레이 캐스팅
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, _layerMask))
        { //layer 8, 9번인 오브젝트가 hit 된다면
            _desPos = hit.point ;
            _state = PlayerState.Moving;
            Debug.Log($"레이캐스트 : {hit.collider.gameObject.tag} / {hit.collider.gameObject.name}");


            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster) {
                Debug.Log("Monster Click!");

                // 몬스터 커리시 공격처리
            }
            else {
                Debug.Log("Gound Click!");
            }
        };


        
    }
}
