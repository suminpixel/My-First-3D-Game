using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// C# 리플렉션 : 
// 프로그램 실행 도중에 최상위 객체의 정보를 조사하거나, 
// 다른 모듈에 선언된 인스턴스를 생성하거나, 
// 기존 개체에서 형식을 가져오고 해당하는 메소드를 호출,
// 또는 해당 필드와 속성에 접근할 수 있는 기능

// getType() : 타입 조회 , getMembers() : 멤버조회, getMothods(): 메소드 목록 가져옴
public abstract class UI_Base : MonoBehaviour
{
  
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    
    public abstract void Init();
    
    //_object 필드에 특정 타입의 UI컴포넌트를 배열로 삽입
    protected void Bind<T>(Type type) where T :UnityEngine.Object{
        
        string[] names = Enum.GetNames(type);  // 타입들을 스트링 배열로 반환
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);


        for(int i = 0; i < names.Length ; i++){
            if(typeof(T) == typeof(GameObject)){
                    objects[i] = Util.FindChild(gameObject, names[i], true); 
     
            }else{
                objects[i] = Util.FindChild<T>(gameObject, names[i], true); //최상위 부모 : UI_BUTTON 를 넣음
     
            }
            if(objects[i]== null){
                Debug.Log("CAN NOT FIND UI object");
            }
        }
    }

    // index 번호로 _object 필드에 있는 UI 컴포넌트 리턴
    protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (_objects.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}

	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
	protected Text GetText(int idx) { return Get<Text>(idx); }
	protected Button GetButton(int idx) { return Get<Button>(idx); }
	protected Image GetImage(int idx) { return Get<Image>(idx); }

    //게임 오브젝트, 액션, 이벤트 정의(Define)
    public static void BindEvent(GameObject go, Action<PointerEventData> action , Define.UIEvent type = Define.UIEvent.Click){ //Define 초기값으로는 가장 많이 쓰는 click 이벤트루 지정
        
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch(type){
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action; //이전에 있던 액션이 걸려있는경우를 대비해 한번 뺘줌
                evt.OnClickHandler += action;
            break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
            break;
        }
       //evt.OnDragHandler += ((PointerEventData data)=>{evt.gameObject.transform.position = data.position;});
    }
  
}
