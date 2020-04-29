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
        Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));
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
    }
}
