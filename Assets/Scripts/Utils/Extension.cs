using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public static class Extension 
{
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action , Define.UIEvent type = Define.UIEvent.Click){ //Define 초기값으로는 가장 많이 쓰는 click 이벤트루 지정
        UI_Base.AddUIEvent(go, action, type);    
    }
     
}
/*
public static class Extension
{
	public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
	{
		return Util.GetOrAddComponent<T>(go);
	}
	
	public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
	{
		UI_Base.BindEvent(go, action, type);
	}

}
*/