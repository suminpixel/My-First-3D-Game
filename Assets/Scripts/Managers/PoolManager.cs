using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager 
{
    #region Pool
    class Pool{
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }


        Stack<Poolable> _poolStack = new Stack<Poolable>();
        public void Init(GameObject original, int count = 5){
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for(int i = 0; i < count ; i++ ){
                Push(Create());
            }
        }

        Poolable Create(){
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable){
            if(poolable == null){
                return;
            }
            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;
            
            _poolStack.Push(poolable);
        }


        public Poolable Pop(Transform parent){
            Poolable poolable;
            if(_poolStack.Count > 0){
                poolable = _poolStack.Pop();
            }else{poolable = Create();}
            poolable.gameObject.SetActive(true);

            // DontDestroyOnLoad 해제 용도
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform; //꼼수로 Scene에 가져다 붙임 (상위 옵젝 아무거나 상관없긴함)

            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;

        }
    }
    #endregion

    //풀 매니저 산하에 여러개의 풀들을 가지고있는데 key: string 과 value: pool 을 Dictionary 로 가지고 있다.
  	Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;


    //리소스를 매번 initialize 를 하지 않고 풀 매니저에서 풀링된 녀석이 있을까 조회후 가져올수있도록함
    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void Push(Poolable poolable){
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false){
            GameObject.Destroy(poolable.gameObject);
            return;
        }
        _pool[name].Push(poolable);
    }

    public void CreatePool(GameObject original, int count = 5){
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root.transform;

        _pool.Add(original.name, pool);
    }
    public Poolable Pop(GameObject original, Transform parent = null){
        if(_pool.ContainsKey(original.name) == false){
            CreatePool(original);
        }
        return _pool[original.name].Pop(parent);
    }

    //풀매니저가 원본을 들고있지는 않은지 확인 (매번 로드하지 않게)
    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false){
             return null;
        }
        
        return _pool[name].Original;
    }

    public void Clear(){ //거의 사용할일 없지만 대규모인 경우 사용
        foreach (Transform child in _root){
            GameObject.Destroy(child.gameObject);
        }
        
        _pool.Clear();
    }
}
