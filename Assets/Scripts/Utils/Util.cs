using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 최상위 객체(game obj)의 자식 객체들 을 찾아주는 기능
// 파라매터로 이름 받음
public class Util{

    public static T GetOrAddComponent<T>(GameObject go) where T: UnityEngine.Component{
        T component = go.GetComponent<T>();
        if(component == null){
            component = go.AddComponent<T>();
        }
            return component;
        
    }

    //1. GameObj 를 받아 child GameObj 를 리턴
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false) {
        Transform transform =  FindChild<Transform>(go, name, recursive);
        if(transform != null){
            return transform.gameObject;
        }else{
            return null;
        }
    }
    //.GameObj 를 받아 child GameObj 를 리턴
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T: UnityEngine.Object{
      
        if(go == null){
            return null;
        }

        if(recursive == false){

            for(int i = 0; i <go.transform.childCount; i++){
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name ) || transform.name == name ){
                    T component = transform.GetComponent<T>();
                    if(component != null){
                        return component;
                    }
                }
            }
                
        }else{
             //recursive == true : 재귀적으로 자식의 자식까지 찾을것이냐
            foreach(T component in go.GetComponentsInChildren<T>()){
          
                if(string.IsNullOrEmpty(name) || component.name == name){
                    return component;
                }
            }
        }
        return null;
    }
}