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

    //스폰
    public GameObject Spawn(Define.WorldObject type, string path, Transform parant = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parant);

        switch (type) {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
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
                if (_monsters.Contains(go)) {
                    _monsters.Remove(go);
                }
                break;
            case Define.WorldObject.Player:
                if (_player == go) {
                    _player = null;
                }
               
                break;

        }

        Managers.Resource.Destroy(go);

    }
}
