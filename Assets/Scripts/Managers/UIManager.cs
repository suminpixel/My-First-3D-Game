using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //팝업시 sort order 를 숫자 (층) 으로 관리
    int _order = 10;  //0과 구분되기위해 10부터 시작 

    //가장 마지막에 띄운 팝업이 먼저 사라져야하기 때문에 stack 자료형으로 팝업 요소들을 가지고있음
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    UI_Scene _sceneUI = null;


    //UI 컴포넌트들의 루트오브젝트(최상위 부모)를 반환
    public GameObject Root {
        get{
            GameObject root = GameObject.Find("@UI_Root");
            if(root == null){
                root = new GameObject{name = "@UI_Root"}; 
            }
            return root;

        }
    }

    //Popup UI가 켜질 때 _order ++
    public void SetCanvas(GameObject go, bool sort = true){
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true; //
        if(sort){
            canvas.sortingOrder = _order;
            _order++;
        }else{
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene{

        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        Debug.Log(name);
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.Transform); // 해당 obj를 root obj 자식으로 지정
    
        return sceneUI;
    }

    //팝업 컴포의 이름을 받아 가져오는 매서드
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup{

        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        Debug.Log(name);
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);


        go.transform.SetParent(Root.Transform); // 해당 obj를 root obj 자식으로 지정
    
        return popup;
    }

    public void ClosePopupUI(UI_Popup popup){
        if(_popupStack.Count == 0){
            return;
        }
        if(_popupStack.Peek() != popup){ //원하는 팝업이 아닌 녀석이 닫히게 되는 경우를 방지
            Debug.Log("Close Popup Failed");
            return;
        }
        ClosePopupUI();
    }


    public void ClosePopupUI(){
        if(_popupStack.Count == 0){
            return;
        }

        UI_Popup popup = _popupStack.Pop(); //가장 상단의 팝업을 닫음
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void CloseAllPopupUI(){
        while(_popupStack.Count != 0){
            ClosePopupUI();
        }
    }

    
}
