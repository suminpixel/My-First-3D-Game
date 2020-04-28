using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1. RigitBody (isKinnematic: off)
    // 2.Collider (isTrigger: off)
    // 3.상대방에 Collider (isTrigger : off)
    private void OnCollisionEnter(Collision collision){
        Debug.Log("Collistion");
    }

    private void OnTriggetEnter(Collider other){
        Debug.Log("Trigger");
    }
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
}
