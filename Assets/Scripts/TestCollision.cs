using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1. RigitBody (isKinnematic: off)
    // 2. Collider (isTrigger: off)
    // 3. 상대방에 Collider (isTrigger : off)
    private void OnCollisionEnter(Collision collision){
        Debug.Log($"Collision @ {collision.gameObject.name}");
    }

    // 1.둘다 Collider 가 있어야 한다
    // 2.둘 중 하나는 isTrigget : On
    // 3.둘 중 하나는 RigidBody 가 있어야 한다.
    private void OnTriggetEnter(Collider other){ //Trigger : Obj - Obj 끼리 닿았을때. ex) 포탈 이벤트... 함정이벤트 등 무한히 응용가능
        Debug.Log($"Trigger @ {other.gameObject.name}");
    }
    void Start()
    {
        
    }

  
    void Update()
    {
        // 좌표의 종류 1.월드 좌표 <-> 2.로컬(오브젝트) 좌표 <-> 3.뷰포트 <-> 4.스크린 좌표(유저 모니터)
        //Debug.Log(Input.mousePosition); //Screen 좌표 표시
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); //ViewPort 좌표 (0에서 1까지의 비율로 표기)


       
        //스크린 좌표를 구했을 때 월드 좌표를 어떻게 구할 수 있을까? 2D -> 3D
        // 좌표가 하나 없어진다는게 중요 x, y, x -> x, y
        // 카메라의 far(깊이) 수치가 0에 가깝게 되었을 때, 평면에 가까워지는 영역이 나옴

        // Camera.main : 메인 카메라

        /*
        if(Input.GetMouseButtonDown(0)){
         
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)); //마우스 찍힌 스크린좌표로 월드 포지션 구하기

            Vector3 dir = mousePos - Camera.main.transform.position;
            dir = dir.normalized; //방향 벡터 구하기
    
            Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f); //1초동안 붉은 광선이 표기됨

            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f)){
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            };   
        }
        */
        if(Input.GetMouseButtonDown(0)){
            //조금더 쉽게 마우스 (클릭)포인터 구현
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log($"-- Ray @ {ray}");
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //int mask = (1 << 8) | (1<<9); //비트로 768
            LayerMask mask = LayerMask.GetMask("Monster");
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask)){ //layer 8, 9번인 오브젝트가 hit 된다면
        
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag} /{hit.collider.gameObject.name}");
            }; 
        }

        /*

        //레이 캐스팅 실슴

        Vector3 look = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
      
        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);
        //레이 캐스팅 : 레이저를 쏘아서 충돌 
       
        if(Physics.Raycast(transform.position + Vector3.up, look * 10, out hit, 10)){//transform.position(:플레이어 위치)에 forward 방향으로 쏨
            Debug.Log($"Raycast @ {hit.collider.gameObject.name}");
        }
        */


        //무엇을 레이케스팅 할것인가 ? 1. 배경, 환경(이동이나 시점 전환) 2.오브젝트(캐릭터, 몬스터)
    }
}
