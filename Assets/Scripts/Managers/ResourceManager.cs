using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//경로를 통해 데이터를 로드하거나
//프리팻 인스턴스를 복사/desctory
public class ResourceManager 
{
   public T Load<T>(string path) where T : Object{ //Object를 넣으면 정상적으로 로드해주슨 제네릭
       return Resources.Load<T>(path);
   }

    public GameObject Instantiate(string path, Transform parent = null){ 
       GameObject prefab = Load<GameObject>($"Prefabs/{path}"); //프리팹 폴더의 해당 패쓰의 게임 오프젝트를 반환
        if(prefab == null){
            Debug.Log($"Failed to load prefab");    
            return null;
        }
        return Object.Instantiate(prefab, parent);
   }


    public void Destroy(GameObject go){ 
       if(go == null) return;
       Object.Destroy(go);
   }

   //public GameObject Ins
}
