
## UI Component 개요
Canvas Component 산하에 UI 요소들을 배치하여 관리한다. 캔버스의 갯수는 제한이 없다.
버튼 처럼 Common 하게 활용되는 UI컴포의 경우 Prefab으로 만들어 사용한다.
┣ 📦 @RootUI(Env)
┃ ┗ 📦 StartButton 
┃ ┃ ┗ 📦 Blocker (투명, 해당 UI 뒤 클릭 방지 )
┃ ┃ ┗ 📦 Button…
┃ ┃ ┃ ┗ 📦 Text…
Ex) 게임시작버튼

#### Rect Transform & SortOrder
UI Component 는 Rect Transform 이라는 툴을 통해 위치나 사이즈를 조절한다. 
UI는 사용자 디바이스의 화면 비율 , 종류에 따라 UI 컴포넌트의 사이즈나 여백이 동적인 비율로 늘어났다 줄어났다 할 수 있어야한다. 때문에 Rect Transform 을 사용하게 되며, ‘앵커’ 를 통해 구현이 가능하다. 
또한 Canvas 속성의 SortOrder 값을 조절하여 UI컴포넌트 간 중첩이 가능하다.

#### Anchor Presets
UI 컴포넌트의 부모가 Rect Tranform 을 가지고 있는경우 앵커가 활성화 된다.
앵커 포인트를 조절하여 부모 컴포넌트와 본인의 거리를 %로 조절하거나,
중앙에 위치하거나 혹은 사이드에 붙이는등의 상대 위치 조절이 가능하다.
(img)
(img)


## Button Events
Button Script에 public click method를 생성하여 사용한다.
*주의)*만약 클릭에 대해 다른 입력 이벤트를 설정해두었다면,  중복되지 않도록 input 에 대한 예외처리가 필요하다.
```

if(EventSystem.current.IsPointerOverGameObejct()){
	return //game obj 클릭이 아닌 ui obj 클릭이기 때문에 이후에 일어나는 다른 입력 관련 이벤트 무시
}
//기타 키입력, 마우스입력, 이동 등 이벤트코드 이후에 작성된다.

```


----

## UI 자동화
대규모 프로젝트의 경우 수많은 UI컴포넌트가 존재하는데, 각 컴포넌트와 수많은 이벤트를 연결하는데 많은 시간이 소요된다.  또한 이과정에서 개발자의 실수나 의도치 않은 버그가 발생한다.
때문에 UI요소들과 이벤트가 자동으로 바인딩 될 수 있도록 구성하는게 좋다.

아래 코드는 상위 UI 컴포넌트의 자식 혹은 인스턴스를 찾아 Bind 하고, 필요한 경우 코드상에서 Get 하여 이벤트를 발생시킬수 있게 한다.

```
// UI 컴포넌트가 삽입될 Dictionary 필드
protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary< Type,UnityEngine.Object[]>();

//_object 필드에 특정 타입의 UI컴포넌트를 배열로 삽입
    protected void Bind<T>(Type type) where T :UnityEngine.Object{
        
        string[] names = Enum.GetNames(type);  // 타입들을 스트링 배열로 반환
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);


        for(int i = 0; i < names.Length ; i++){
            if(typeof(T) == typeof(GameObject)){
                    objects[i] = Util.FindChild(gameObject, names[i], true); 
     
            }else{
                objects[i] = Util.FindChild<T>(gameObject, names[i], true); //최상위 부모를 넣음
     
            }
            if(objects[i]== null){
                Debug.Log("CAN NOT FIND UI object");
            }
        }
    }

    // index 번호로 _object 필드에 있는 UI 컴포넌트 리턴
    protected T Get<T>(int idx) where T : UnityEngine.Object{
        UnityEngine.Object[] objects = null;
        if(_objects.TryGetValue(typeof(T), out objects) == false){
            return null;
        }
        return objects[idx] as T;
    }
```


#### 버튼 이벤트 핸들링
버튼 이벤트의 종류에 따라 (클릭, 드래그) 이벤트를 UI에서 캐치하여 콜백으로 날려주어야 한다.

#### 팝업 레이어 관리
복수의 팝업창이 뜰 때는, 레이어의 갯수를 별도 필드로 관리하고 가장 늦게 출력된 UI 먼저 닫히도록 (Stack) 관리한다.

```

//팝업시 sort order 를 숫자 (층) 으로 관리
int _order = 10;  //0과 구분되기위해 10부터 시작 


//가장 마지막에 띄운 팝업이 먼저 사라져야하기 때문에 stack 자료형으로 팝업 요소들을 가지고있음
Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

public void SetCanvas(GameObject go, bool sort = true){
	// Popup UI 가 켜질때 캔버스(레이어) 층을 설정
}


public T ShowPopupUI<T>(string name = null) where T : UI_Popup{
	//팝업창을 띄움
}

public void ClosePopupUI(UI_Popup popup){
	// 지정된 팝업창을 닫음
}
```


----

## Inventory 제작 간단 과정

#### 1) 배경 패널 만들기
2D Sprite , Sprite Editor를 설치한다. 
Panel 의 Image 속성의 Source Image 탭에서 배경 리소스 변경이 가능하다. 
Image Type은 Slice로 하며, 9-patch 의 경우 resource 를  sprite editor 에서 조절한다.

#### 2) 그리드 패널
아이콘 요소들이 들어갈 grid panel을 만든다.

#### 3) 아이콘 만들기
이미지 리소스의 Pixel per Unit 값 조절로 해상도를 조절할 수 있다.

