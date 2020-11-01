## 2D Player Object

### Player Oject 생성
다운로드 받은 캐릭터 Object 를 pixel per unit 을 조정 후, Scene에 추가한다.
Player Controller class 해당 object에 추가하여 별도의 액션을 가능하도록 구성한다.

---- 

### Player Controller 

#### Player 이동
1.  input.GetKey(KeyCode.설정키) 매서드를 통해 방향키 이동을 제어할 수있다. 이때, 플레이어의 이동속도는 Time.deltaTime(frame rate) 을 곱해 다양한 기기나 콘솔에서도 동일한 이동 경험을 줄 수있도록 한다.
2.  멀티 게임 (특히 mmo rpg) 의 경우, 플레이어 컨트롤러의 Update()문에서 방향키 이동을 직접 제어하는것은 적절하지 않다. 아래와 같이 메서드를 별도로 만들어 구성한다.
3.  실제 좌표 이동이 아닌 기획상 타일 (1칸) 이동의 경우 를 고려해야할 경우가 있다. 다음 칸(셀)까지는 걷는 모션만 하다가 실제 다음칸의 시작점에 위치하게 되면 실질적인 이동이 발생하게 하는 방법이 있다.

```
void Update()
{
	GetDirInput(); //1. key 입력 받기 
    UpdateIsMoving(); // 2. 이동 가능한 상태일때 실제 좌표 이동
    UpdatePosition(); // 3. 셀(칸) 단위 이동 처리 (클라이언트 뷰 에서 보이는것)
        
}
```

 

#### Vector 3
new Vector3은 3차원 벡터를 생성하는 생성자로, 방향 + 크기  대한 데이터를 모두 가지고 있다.
- 방향 : 이동해야하는 목적지 - 현재 위치 를 통해 방향벡터를 구할 수 있다.  
- 거리 : (vector3).magnitude 로 목적지까지의 남은 거리를 알 수 있다.

----

### Player Animation Control
Unity에서 제공하는 Animator Controller 객체를 사용하여 플레이어 혹은 객체의 애니메이션을 컨트롤 할 수 있다. 
Player Object 에 Animatior 속성에 애니메이션 Asset을 (.anim 확장자) 추가한다.
(img)

Player controller 스크립트에서는 시작시 animator 컴포넌트를 getComponent로 불러와 재생할 수 있다.
```
switch(value){
	case MoveDir.Up :
    	_animator.Play("WALK_BACK");
        break;
```

