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
    SceneMangersEx _scene = new SceneMangersEx();
    ResourceManager _resorce = new ResourceManager();
    UIManager _ui = new UIManager();
    SoundManager _sound = new SoundManager();
    PoolManager _pool = new PoolManager();
    DataManager _data = new DataManager();


    public static InputManager Input{ get{return Instance._input;} }
    public static ResourceManager Resource{ get{return Instance._resorce;}}
    public static UIManager UI{ get{return Instance._ui;}}
    public static SceneMangersEx Scene{ get{return Instance._scene;}}
    public static SoundManager Sound{ get{return Instance._sound;} }
    public static PoolManager Pool{get{return Instance._pool;}}
    public static DataManager Data { get { return Instance._data; } }


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
            
            s_instance._data.Init(); //데이터 매니저의 경우 항상 들고있는 경우가 대부분이라 별도로 클리어하지는 않음
            s_instance._pool.Init();
            s_instance._sound.Init();//sound manger 불러들어서 게임 진행 내내 쓰일 SoundManager.cs 의 audioSource 배열을 채워줌 
        }
    }
    
    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();//혹시 다른 곳에서 풀링된 옵젝 쓰고있을지 모르니 가장 마지막에 클리어 
    }
  
}

