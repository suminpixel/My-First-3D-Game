using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    // static Managers : 유일성 보장되는 매니저 인스턴스 
    // 프로그램이 실행될 때 메모리에 할당되며, 이는 프로그램이 종료될 때 해제
    static Managers s_instance; 

    //매니저.cs 를 모든 컴포넌트에서 구독할 수 있도록 getter 역할의 public class 만듬 (일종의 싱글턴) 
    static Managers Instance { get { Init(); return s_instance; } } 

    //기타 매니저들 인스턴스 생성 및 연결
    InputManager _input = new InputManager();
    SceneMangesrEx _scene = new SceneMangesrEx();
    ResourceManager _resorce = new ResourceManager();
    UIManager _ui = new UIManager();


    public static InputManager Input{ get{return Instance._input;} }
    public static ResourceManager Resource{ get{return Instance._resorce;}}
    public static UIManager UI{ get{return Instance._ui;}}
    
    public static SceneMangesrEx Scene{ get{return Instance._scene;}}
    

    void Start()
    {
      Init();
    }

    void Update()
    {
        _input.OnUpdate();
       
    }

    static void Init(){
        if(s_instance == null){
            GameObject go = GameObject.Find("@Managers"); //@Mangers.cs Import
            if(go == null){ //해당 폴더(상위 오브젝트) 없는 경우 새로 생성
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go); //해당 파일이 삭제되지 않게 명시
            s_instance = go.GetComponent<Managers>(); //@Managers.cs > static Managers 인스턴스 참조
        }
    }
}

