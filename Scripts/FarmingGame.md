
### Farming Game Architecture

#### Farming Game System & Main Components
농장 시뮬레이션 류 게임을 만들기 위한 주요 게임 시스템 및 메인 컴포넌트를 정리
- Player : 플레이어 이동, 애니메이션 컨트롤
- Game Scenes & Tilemaps : 충돌 처리, 타일 및 장애물 컴포넌트간 계층
- Inventory & Items : 인벤토리 UI, 아이템, 자산, 재고, 아이템 획득 및 드롭
- Game Time System : 시간 관련 이벤트, 시계 UI
- Scene Management : 씬, 장소 간 이동
- Preserving Scene State
- Tilemap Grid Properties : 그리드 커서 
- Using Tools : 아이템 사용 (농업 장비, 무기,  건축 등…) 
- Pool Manager : Prefabs Pool 관리
- VFX Manager & Particle Effects 
- Crops 
- Pause Menu : 인벤토리 혹은 메뉴 진입시 일시중지
- Save Game : 세이브 파일 I/O, 클레스 데이터 저장, 패킷 직렬화
- Player Customizing : 캐릭터의 의상 및 장식
- Pathfinding for NPCs : NPC 자유 이동 알고리즘 및 시간 기반 이벤트
- Sound Effect & Music 
