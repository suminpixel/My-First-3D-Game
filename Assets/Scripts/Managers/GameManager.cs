using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

해당 컨텐츠, 인게임에서만 사용되는 특수한 설정 매니저 
 
 */
public class GameManager : MonoBehaviour
{
    // 서버에서 받아오는 int <-> gamgeObject
    //Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();

    // id(key) 가 없는 서버리스 케이스의 경우 해시셋  
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    GameObject _player;
    public Action<int> OnSpawnEvent;

    //스폰
    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null) //서버의 경우 path 가 아닌, 고유 id, number를 던져주면 그 numer로 몬스터 디렉토리스크립트에서 긁어오거나 함 
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go) {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null) { return Define.WorldObject.UnKnown; };
        return bc.WorldObjectType;
    }

    //스폰
    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
        }

        Managers.Resource.Destroy(go);

    }

    public GameObject GetPlayer() { return _player;  }
}
