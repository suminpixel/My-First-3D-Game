using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //퍼블릭으로 외부에서 세팅하고싶은데, 보안상 혹은 로직상 어려운경우 [SerializeField]
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuaterView;
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

    [SerializeField]
    GameObject _player = null;

    void Start()
    {
        
    }

    //LateUpdate : Update 문에서 캐릭터의 좌표이동이 이루어지고 있기 때문에 
    //카메라 이동과 캐릭터 이동 순서가 충돌하여 버벅거리는 현상이 있을 수 있어, 
    //업데이트 이후 카메라 이동하여 해당 현상 해결
  
    void LateUpdate()

    {
        
        if(_mode == Define.CameraMode.QuaterView){

            //플레이어 및 몬스터 사망시
            //유니티가 메모리에서 들고는 있을텐데 null 체크때 null 로 인식할수 있도록 해줌
            if (_player == null) {

                return;
            }


            RaycastHit hit;
            
            Debug.DrawRay(Camera.main.transform.position, _player.transform.position * 200.0f, Color.red, 1.0f);
     
            // 카메라가 쏘는 레이캐스팅에 벽이 걸려든 경우
            if(Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall"))){
                //Debug.Log("-- (Wall) zoom move ");
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }else{
                //Debug.Log("-- just move ");
                transform.position = _player.transform.position + _delta; //플레이어 방향 벡터에 * 카메라 현재 좌표 더하기
                transform.LookAt(_player.transform); //(특정 오브젝트)의 방향을 바라보게 회전(주시)
   
            }

        
        }
    }

    public void SetQuaterView(Vector3 delta){
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
