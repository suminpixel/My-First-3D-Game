using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//경로를 통해 데이터를 로드하거나
//프리팻 인스턴스를 복사 혹은 제거(Destroy)
public class ResourceManager 
{
    //Object path 넣으면 어떤 타입의 리소스든 리턴(로드)
    public T Load<T>(string path) where T : Object{ 
        return Resources.Load<T>(path);
    }

    //프리팹 GameObject 생성
    //path(경로)와 부모를 받아 게임 오브젝트를 생성 and 리턴
    public GameObject Instantiate(string path, Transform parent = null){ 

        GameObject prefab = Load<GameObject>($"Prefabs/{path}"); //프리팹 폴더의 해당 패쓰의 게임 오프젝트를 반환
        
        if(prefab == null){
            Debug.Log($"프리팹 없음 : Failed to load prefab : {path}");    
            return null;
        }

        GameObject go = Object.Instantiate(prefab, parent); 

        //obj 에 (clone)이라는 수식구가 붙는 경우가 있어서 그것은 제거
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
   }

    // 게임 오브젝트 제거
    public void Destroy(GameObject go){ 
       if(go == null) return;
       Object.Destroy(go);
   }

}
