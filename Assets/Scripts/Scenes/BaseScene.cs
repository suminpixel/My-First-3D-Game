using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{

    //게임 실행시 씬의 초기값 설정

    public Define.Scene SceneType {
        get;
        protected set; //겟은 public 이지만 setting은 나와 연관된 자식들만 가능하도록 보호됨
    } = Define.Scene.Unknown;


    void Awake(){ //Start() 보다 먼저 실행
        Init();
    }

    protected virtual void Init() //protected virtual void
    {
        //모든 종류 씬 오브젝트들에 eventSystem 프리팹 삽입
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if(obj == null){
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }

    }

    public virtual void Clear(){

    }

}
