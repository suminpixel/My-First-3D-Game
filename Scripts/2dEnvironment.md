### 2D 환경 세팅

유니티 설치 후 기본 세팅환경은 3D 이므로, Project Setting(Edit) 에서 Default Mode 를 3D에서 2D로 변경후 새 Scene을 생성(cmd + n)해야 한다. 또한 Scene 오브젝트에서 MainCamera Obj의 Camera Projection은 Orthographic로 설정한다.
(img)
----

### 프로젝트 아키텍처 / 폴더 트리

📦 Client
┣ 📂 Resource
┃ ┗ 📂 Data
┃ ┗ 📂 Tiles
┃ ┗ 📂 Prefabs
┣ 📂 Scenes
┣ 📂 Scripts
┃ ┗ 📂 Data
┃ ┗ 📂 Manager
┃ ┃ ┗ 📜 Managers.cs
┃ ┃ ┗ 📂 Core
┃ ┗ 📂 UI
┃ ┗ 📂 Utils

외부 리소스와 / Scence / Scripts(코드) 파일로 구분한다. 특히 Scripts > Manager 폴더의 경우 UIManager, PoolManager, ResourceManager, SceneManager, SoundManager, DataManager 등을 정리한다.

사용 시에는 해당 스크립트들을 한꺼번에 관리하는 Managers 클래스를 만들어 인스턴스 초기화, GameObject 에 복사해서 Client 환경 전역에 적용한다.

유일성 보장과 대규모 프로젝트로 성장했을때의 용이한 관리를 위해 위해 아래와 같이 옵저버 패턴으로 사용한다.


```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
//인스턴스 초기화
static Managers s_instance;
static Managers Instance {
	get {
		Init();
		return s_instance;
	}
}
/*
Core 폴더의 각 역할을 수행하는 Manager Script 를 선언
UIManager, PoolManager, ResourceManager, SceneManager, SoundManager ... 도 위와 같이 선언 
*/
DataManager _data = new DataManager();
public static DataManager Data { 
	get { return Instance._data; } 
}

/*	
	해당 Manager.cs 스크립트 파일을 게임 프로젝트에 실행시키기 위해 Game Object를 만들어 동적으로 실행
	GameObject go = GameObject.Find("@Managers");
	s_instance = go.GetComponent<Managers(스크립트의 타입)>();
	s_instance._data.Init(); // core manager 인스턴스 초기화 
*/
static void Init()
{
    if (s_instance == null)
    {
		//@Managers 라는 GameObj 에 AddComponent
		GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        s_instance = go.GetComponent<Managers>();
        s_instance._data.Init();
   
    }		
}

void Update(){}
void Start(){
        Init();
}

/*
클리어 함수가 필요한 경우
Sound.Clear(); 와 같이 사용
*/
public static void Clear()
{
    
}

```

----

### 2D Asset

Asset Store에서 Import 된 소스는 외부 폴더가 아닌 Resources 폴더에서 관리하면 좋다.

**Sprites (Texture Type)**
2D 화면에 뿌려주는 이미지 데이터로 일반 텍스처와 동일하게 랜더링 된다.
- Single : 하나의 통 이미지로 사용
- Multiple : 여러개의 이미지를 셋으로 한파일에서 관리하는 경우 (pixel per Unit 을 조절하여 사용)

----

### Unity Grid System

Unity에서는 2D TitleMap Editor 라이브러리를 통해 타일 시스템 구축이 가능하다.
2D Game 배경을 Grid System으로 구축하는경우 다음과 같은 장점을 지닌다.
- 격자 단위, Layer 단위로 정확하고 체계적인 리소스 배치가 가능하다.
- TitleMap Collision  : 서버쪽에서 관리 가능한 맵 데이터를 추출 할 수 있다.
	반드시 서버쪽에서도 플레이어가 해당 영역에 접근(충돌) 가능한지 크로스 체킹을 해야한다. 클라이언트를 조작하거나 해킹(월핵)하여 접근 불가능한 영역에 접근하는것을 막아야 하기 때문이다.

####  1.  Tile Map 구성 방법
TileMap 오브젝트를 생성 후 2D Asset을 TilePalette 에 포함시켜 드로잉한다.
TileMap 오브젝트와 팔레트는 한 Scene 내에 여러 Copy를 만들수 있다.
TileMap 오브젝트는 Order In Layer 속성에 숫자 오름차순으로 Layer (층) 단위로 나누어 구성할 수 있다. 반투명한 배경이 있는경우, 혹은 배경에 장애물이 필요한 경우 등 다양한 케이스에 맞게 적절히 구성한다.
(img)

어셋의 퀄리티의 경우 ProjectSetting의 Quality 탭에서 소급적으로 조절 가능하다.
만들어진 맵그룹은 Prefabs 폴더에서 관리할 수 있다.

####  2. Tile Collision 처리 방법
- TileMap Collision 2D 오브젝트를 Add 하여 자동으로 생성하는 방법
	(img)

-  비어있는 TileMap Object 를 생성후 직접 그려주고, Collision Script code를 작성하여 직접 생성하는 방법
	
```

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

// collision 데이터를 txt 파일로 추출해주는 Map관련 Tool 코드

public class MapEditor 
{

    // 해당 코드 이하는 프로덕션 모드에선 사용되지 않음
    #if UNITY_EDITOR

    //단축키 지정 => % (Ctrl), # (Shift), & (Alt)
    [MenuItem("Tools/GenerateMap %#g")]
	private static void GenerateMap()
	{
        GameObject go = GameObject.Find("Map");

        if (go == null)
            return;
        
        
        Tilemap tm = Util.FindChild<Tilemap>(go, "Tilemap_Collision", true);

        // 서버에 전달할 titlemap collision data 생성 (txt 파일)
        using (var writer = File.CreateText($"Assets/Resources/Map/{go.name}.txt"))
        {
            // 지도의 상하좌우 크기 출력
            writer.WriteLine(tm.cellBounds.xMin);
            writer.WriteLine(tm.cellBounds.yMin);
            writer.WriteLine(tm.cellBounds.xMax);
            writer.WriteLine(tm.cellBounds.yMax);

            /*
            0과 1로 접근가능좌표/접근불가 좌표 출력
            예)
            0000001110010101
            0000001110010100
            0000100001010010
            0000110010100101
            */

            for(int y = tm.cellBounds.yMax; y>= tm.cellBounds.yMin; y--){
                for(int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++){
                    TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                    if(tile != null){
                        writer.Write("1");
                    }else{
                        writer.Write("0");
                    }
                }
                writer.WriteLine();
            }

        }

    }

    #endif
}


```

위와 같이 접근가능/불가 타일을 0과 1의 이중배열로 기록된 text file 생성하여, 서버에서 관리할 수 있도록 한다.
