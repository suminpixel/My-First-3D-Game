using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

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

	//해당 옵젝이 유효한 상태인지 (죽음)
	public static bool isValid(this GameObject go) {
		return go != null && go.activeSelf;
	}

}
