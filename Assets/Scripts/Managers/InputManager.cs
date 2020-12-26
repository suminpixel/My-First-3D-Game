using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//키보드 컨트롤 / 마우스 컨트롤 관련 매니저
/*
왜 Input.GetMouseButton(0) 등으로 쓰지 않고 인풋 매니저 쓰는가?
취향차이 or 유니티에서 지원하지 않는 조금더 디테일한 동작들 PressUp 등을 사용하기 위해
*/
public class InputManager 
{
    public Action KeyAction = null; // need 'import Stystem'
    public Action<Define.MouseEvent> MouseAction = null;
    bool _pressed = false;
    float _pressedTime = 0; //마우스를 길게 누르는 pressUp 등을 받기 위해 시간 잼

    public void OnUpdate()
    {
        // 버튼, UI 클릭은 무시
        if(EventSystem.current.IsPointerOverGameObject()){
           // Debug.Log("UI 영역 클릭");
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
                if (!_pressed) {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if (Time.time < _pressedTime + 0.2f)
                    {
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    }
                    else
                    {
                        MouseAction.Invoke(Define.MouseEvent.PointerUp);
                    }

                }
                _pressed = false;
                _pressedTime = 0;
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
