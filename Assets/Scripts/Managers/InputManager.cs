using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//키보드 컨트롤 매니저 
public class InputManager 
{
    public Action KeyAction = null; // need 'import Stystem'

    public void OnUpdate()
    {
        if(Input.anyKey == false){ // 키 입력이 없는경우
            return;
        }

        if(KeyAction != null){ 
            KeyAction.Invoke(); //대리자 실행
        }
    }

}
