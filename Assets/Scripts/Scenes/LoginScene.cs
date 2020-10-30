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

        //임시로 5개 꺼내고
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 5; i++)
            list.Add(Managers.Resource.Instantiate("unitychan"));

        //메모리에서 바로 날려버리는 임시 코드
        foreach (GameObject obj in list) {
            Managers.Resource.Destroy(obj);
        }
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
