using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//벡터3의 용도 : 
    //1. 위치 벡터
    //2. 방향 벡터 관련 기능을 사용할 수 있는 API
struct MyVector { // x, y, z 값을 가진 벡터 구조체 선언
    public float x;
    public float y;
    public float z;

    //           +
    //      +    +
    // + ------- +
    public float magnitude{
        get {return Mathf.Sqrt(x*x + y*y + z*z);} //(피타고라스) 3차원 벡터의 크기를 구해 리턴
    }
    public MyVector normalized { get { return new MyVector (x/  magnitude, y / magnitude,  z/ magnitude);}} // 크기가 1인 단위벡터 리턴 (방향 추출) ex) (1.0f, 0.0f, 0.0f)
    public MyVector(float x, float y, float z){ this.x = x; this.y = y; this.z = z;} //생성자 
    public static MyVector operator +(MyVector a, MyVector b){ //벡터 값 증가 오퍼레이터
        return new MyVector(a.x + b.x, a.y + b.y , a.z + b.z);
    }
     public static MyVector operator -(MyVector a, MyVector b){ //벡터 값 감소 오퍼레이터
        return new MyVector(a.x - b.x, a.y - b.y , a.z - b.z);
    }
     public static MyVector operator *(MyVector a, float b){ //벡터 값 곱
        return new MyVector(a.x * b, a.y * b , a.z * b);
    }
}
public class note_vector : MonoBehaviour
{
    [SerializeField] 
    float _speed = 10.0f;
    //public GameObject _obg ;

    // Start is called before the first frame update
    void Start()
    {
        
        MyVector to = new MyVector(10.0f, 0.0f, 0.0f);
        MyVector from = new MyVector(5.0f, 0.0f, 0.0f);

        //방향 벡터 
            //1. 거리(크기) - magnitude
            //2. 실제 방향
        MyVector dir = to - from;
        dir = dir.normalized; //방향만 추출
        MyVector newPos = from + dir * _speed; //pos * dir(방향) * 속도 곱하면 움직임
       
    }

    // GameObejct (Player)
        //Transform
        //PlayerController (*)

    void Update()
    {
        // 이동 방식에 따라서 로컬 포지션을 사용할지, 월드 포지션을 사용할지 고민해야함.
        //transform.TransformDirection() : Local -> World
        //transform.InverseTransformDirection() : World -> Local

        //transform.position.magnitude();
        //transform.position.normalized();
        if(Input.GetKey(KeyCode.W)){
            //new Vector3(0.0f, 0.0f, 1.0f)
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
        }
    }
}
