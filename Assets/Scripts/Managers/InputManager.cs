using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//키보드 컨트롤 매니저 
public class InputManager 
{
    public Action KeyAction = null; // need 'import Stystem'
    public Action<Define.MouseEvent> MouseAction = null;
    bool _pressed = false; 

    public void OnUpdate()
    {
        // 버튼, UI 클릭은 무시
        if(EventSystem.current.IsPointerOverGameObject()){
            Debug.Log("UI 영역 클릭");
           // return;
        }
       

        // 그 외 다른 쪽을 클릭한다면 
        if(Input.anyKey && KeyAction != null){ 
            KeyAction.Invoke(); //대리자 실행
        }

        // TODO: 추후 클릭, 드래그 등의 기능 정의 가능
        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }

    }

    //씬마다 마우스액션, 키액션이 다를수 있어서 정리해주는 함수 
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
