using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//키보드 입력 매니저 
public class InputManager 
{
    public Action KeyAction = null; // need 'import Stystem'

    // Update is called once per frame
    public void OnUpdate()
    {
        if(Input.anyKey == false){ // 키 입력이 없는경우
            return;
        }

        if(KeyAction != null){
            KeyAction.Invoke();
        }
    }

}
