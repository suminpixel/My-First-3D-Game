using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//인게임 씬 타입의 스크립트
public class LoginScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
    }
    public override void Clear()
    {
        Debug.Log("Login scene clear()");
    }
    
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Q)){
            // TODO: 아이디 패스워드 등을 확인하여 인게임으로 이동 하는 응용 가능 
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }


}
