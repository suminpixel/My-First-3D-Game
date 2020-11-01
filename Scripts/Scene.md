
## Scene 

#### UnityEngine.SceneManagement 
로그인화면, 인게임 화면등을 UnityEngine에서 제공하는 UnityEngine.SceneManagement 라이브러리의 도움을 받아  관리할 수 있다.
1. 씬 등록
	File > Build Settings > Add Open Scenes
2. 씬 불러오기
	(코드상에서)
	SceneManager.LoadScene(“InGame”);


#### SceneManagerEx.cs
SceneManagement 라이브러리의 기능을 확장하거나 의존하지 않기위해 매니저 파일을 만들어 관리하는 방법도 있다.

```

public class SceneMangesrEx 
{
    public BaseScene CurrentScene{ // 현재 사용중인 씬 리턴
        get{
            return GameObject.FindObjectOfType<BaseScene>();
        }
    }

    //씬 로드
    public void LoadScene(Define.Scene type){
        CurrentScene.Clear(); //현재 씬을 클리어 후에
        SceneManager.LoadScene(GetSceneName(type)); //새로 원하는 씬 로드 
    }

    //씬 종류로 이름 리턴
    string GetSceneName(Define.Scene type){
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }



}


```


#### 씬의 종류 및 구현
Login / InGame / Tutorial / Lobby / Setting / Unknown 등의 타입으로 관리하며
기본적으로 팝업 오픈 여부, UI 노출 여부등을 관리한다.

```
//인게임 씬 타입의 스크립트 일부
public class InGameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

        //인게임 씬에서는 UI 팝업창이 열리도록 구현
        Managers.UI.ShowSceneUI<UI_Inven>();
    }
    public override void Clear()
    {
        
    }


}

```

