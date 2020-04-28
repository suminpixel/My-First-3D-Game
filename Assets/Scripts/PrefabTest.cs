using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    public GameObject prefab; //GameObject : 유니티 씬에 배치가능한 모든 객체들의 베이스 클래스
    GameObject tank;
    void Start()
    {
        tank = Managers.Resource.Instantiate("Tank");
        //prefab = Resources.Load<GameObject>("Prefabs/Tank"); // Asset>Resources 안에 있는 특정 경로의 소스를 로드해서 프리펩에  넣음
        //tank = Instantiate(prefab); //시작하자마자 프리팹 인스턴스 생성 
        
        Destroy(tank, 3.0f); // 3초후에 tank 프리팹 인스턴스 삭제
    }

}
