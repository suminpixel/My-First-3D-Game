
# Coroutine & Pooling

## Coroutine (코루틴)

코루틴이란 프로그래밍언어 세계관에서 중요한 개념이다.
연산 시작전의 상태를 캡쳐(저장)했다가 연산이후에 로드(복원)하는 것이다.
만약, 코루틴을 사용하지 않는다면 매우 복잡한 로직을 실행할 때 (길찾기, 딜레이가 긴 시뮬레이션 연산) 해당 연산들을 한 틱(1프레임)에 몰아서 진행한다면 과한부하가 생길 수 있다.

- 오래걸리는 작업을 원하는 시점에 잠시 끊어야 할 때
- 원하는 타이밍에 함수를 잠시 stop/복원 하고싶을 때



### 유니티에서 Coroutine 활용하기

```

public void Init(){
	CoroutineTest test = new CoroutineTest();
	foreach(System.Object t in test){
		int value = (int)t;
		Debug.Log(value);
	}
}


class CoroutineTset : IEnumerable {
	public IEnumerable GetEnumerator(){
		//  로직 실행
		yield return 결과물;
		// 중간 로직 실행
		yield return 결과물;
		// 중간 로직 실행
		yield return null; //만약 중간에 break 하고 싶은경우
		// 실행되지 않음
		yield return 결과물;
	}
}

```



## Object Pooling

어딘가에 저장되어 있는 데이터(오브젝트)들을 필요할때 마다 불러와서 사용하였으나, 그 과정이 매우 느리다. 따라서 대규모 프로젝트에서는, 기존에 사용했던 리소스를 효율적으로 재사용 할 수 있도록 Pool 을 만들어 관리하는 ‘Object Pooling’ 이 필요하다.
예로, 빗발치는 총알, 방대한 숫자의 몹들에 Poolable 속성을 적용할 수 있다.

### Pool Architecture
풀링을 하고싶은 오브젝트에 풀링 컴포넌트를 달아준다.
런타임 도중 풀링되어있는 오브젝트들은 다음과 같이 보관 된다.

📂 DontDestroyOnLoad
┣ 📂 @PoolRoot
┃ ┗ 📂 MonsterRoot
┃ ┃ ┗ 📜 Monsster
┃ ┗ 📂 CharacterRoot
┃ ┃ ┗ 📜 Character

(img)


