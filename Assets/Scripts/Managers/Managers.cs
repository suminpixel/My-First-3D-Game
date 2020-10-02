using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers s_instance; //static 으로 유일성 보장되는 매니저 인스턴스 복사
    //매니저.cs 를 모든 컴포넌트에서 구독할 수 있도록 getter 역할의 public class 만듬 (일종의 싱글턴) 
    static Managers Instance{ get{ Init(); return s_instance;} }

    InputManager _input = new InputManager();
    ResourceManager _resorce = new ResourceManager();
    UIManager _ui = new UIManager();


    public static InputManager Input{ get{return Instance._input;} }
    public static ResourceManager Resource{ get{return Instance._resorce;}}
    
    public static UIManager UI{ get{return Instance._ui;}}
    
    // Start is called before the first frame update
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
            if(go == null){ //해당 파일이 없는 경우 새로 생성
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go); //해당 파일이 삭제되지 않게 명시
            s_instance = go.GetComponent<Managers>(); //@Managers.cs > static Managers 인스턴스 참조

            
            
        }
    }
}

