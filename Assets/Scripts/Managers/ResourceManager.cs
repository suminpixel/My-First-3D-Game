using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//경로를 통해 데이터를 로드하거나
//프리팻 인스턴스를 복사 혹은 제거(Destroy)
// + 풀링매니저를 적극 이용
public class ResourceManager 
{
    //Object path 넣으면 어떤 타입의 리소스든 리턴(로드)
    public T Load<T>(string path) where T : Object{ 


        if(typeof(T) == typeof(GameObject)){
            string name = path;
            int index = name.LastIndexOf('/');
            if(index >= 0){
                name = name.Substring(index + 1);
            }
            //  original 녀석이 있는지 조회
            GameObject go = Managers.Pool.GetOriginal(name);
            
            if(go != null){
                return go as T;
            }
        }
        return Resources.Load<T>(path);
    }

    //프리팹 GameObject 생성
    //path(경로)와 부모를 받아 게임 오브젝트를 생성 and 리턴
    public GameObject Instantiate(string path, Transform parent = null){ 

    
        GameObject original = Load<GameObject>($"Prefabs/{path}"); //프리팹 폴더의 해당 패쓰의 게임 오프젝트를 반환
        
        if(original == null){
            Debug.Log($"프리팹 없음 : Failed to load prefab : {path}");    
            return null;
        }

        // 풀링된 녀석이 있는지 조회
        if(original.GetComponent<Poolable>() != null){
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent); 
        go.name = original.name;

        //obj 에 (clone)이라는 수식구가 붙는 경우가 있어서 그것은 제거
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
   }

    // 게임 오브젝트 제거
    public void Destroy(GameObject go){ 
       if(go == null) return;

        //만약 풀링하고싶은 녀석이면 풀링매니저에게 위탁함
        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable != null){
            Managers.Pool.Push(poolable);
            return;
        }

       Object.Destroy(go);
   }

}
