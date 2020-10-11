using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//키보드 컨트롤 매니저 
public class InputManager 
{
    public Action KeyAction = null; // need 'import Stystem'
    public Action<Define.MouseEvent> MouseEvent = null;
    bool _pressed = false; 

    public void OnUpdate()
    {
        // 버튼, UI 클릭은 무시
        if(EventSystem.current.IsPointerOverGameObject()){
            Debug.Log("you click ui obj");
            return;
        }
       

        // 그 외 다른 쪽을 클릭한다면 
        if(Input.anyKey && KeyAction != null){ 
            KeyAction.Invoke(); //대리자 실행
        }

        // TODO: 추후 클릭, 드래그 등의 기능 정의 가능
        if(MouseEvent != null){
            if(Input.GetMouseButton(0)){
                MouseEvent.Invoke(Define.MouseEvent.Press); //한번 눌렀을 때
                _pressed = true;

            }else{
                if(_pressed){
                    MouseEvent.Invoke(Define.MouseEvent.Click); //눌렀다 뗀 순간
                    _pressed = false;
                }
            }   
        }

        
    }

}
