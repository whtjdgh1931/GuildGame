# GuildGame

> Unity 기반 2D 길드/전투 프로젝트  
> 클래스별 유닛 생성, 전투 FSM, 스테이지 진행, 오브젝트 풀링 중심으로 구조를 개선한 개인 프로젝트입니다.

<!--
추후 추가 권장
- 메인 플레이 GIF 1개
- 전투 화면 스크린샷 2~3장
- Object Pool 적용 전/후 Profiler 캡처
-->

## 프로젝트 소개

**GuildGame**은 Unity 2022 기반으로 제작한 2D 길드/전투 프로젝트입니다.  
단순 기능 구현에 그치지 않고, 전투 상태 전이(FSM), 스테이지 해금 흐름, HP UI 동기화, 오브젝트 풀링을 중심으로 런타임 구조를 정리하는 데 초점을 두었습니다.

특히 최근 작업에서는 다음 두 가지를 중요하게 다뤘습니다.

- 전투 흐름을 상태 기반으로 정리해 자동 공격, 스킬, 스턴/도발 예외 처리까지 한 흐름으로 관리
- 반복 생성되는 투사체, UI, 버튼을 오브젝트 풀로 관리해 런타임 생성/회수 비용을 줄이기 위한 구조 개선

## 핵심 기능

### 1. 전투 FSM 기반 상태 관리
- `SEARCH / MOVE / ATTACK` 상태 전이 기반으로 전투 흐름을 구성했습니다.
- 플레이어 유닛은 `Player_Attack.DoAutoAttack()` 경로를 분리해 일반 유닛과 다른 자동 공격 로직을 처리합니다.
- 스킬 쿨타임, 기본 공격 쿨타임, 스턴, 도발 여부에 따라 공격 분기를 제어합니다.

### 2. 클래스별 유닛 생성 및 전투 참여
- 클래스별 전투 유닛 프리팹 구조를 분리해 플레이어/아군/적군 용도를 나누어 관리합니다.
- 전투 배치용 유닛과 플레이어 조작용 프리팹을 분리해 역할별 책임을 명확히 유지합니다.

### 3. 스테이지 진행 및 해금 로직
- 월드/레벨 단위의 스테이지 진행 구조를 가집니다.
- `Stage_{world}_{level}_Cleared` 키 기반으로 클리어 여부를 저장합니다.
- 스테이지 버튼은 `클리어 / 진행 가능 / 잠금` 상태에 따라 다른 프리팹으로 분기합니다.

### 4. Object Pool 기반 런타임 최적화
- 오브젝트 풀은 `availablePools`, `inUsePools`, `poolContainers` 구조로 관리합니다.
- 인스턴스의 `InstanceID -> poolKey` 매핑을 유지해 이름이나 부모가 바뀌어도 원래 풀로 안전하게 반환할 수 있도록 구성했습니다.
- 즉시 반환, 지연 반환, 전체 반환, 전체 클리어 기능을 제공해 테스트와 운영 양쪽에서 사용할 수 있도록 했습니다.

### 5. 전투 HP UI 동기화
- 전투 시작 시 팀/적군 HP 슬라이더를 오브젝트 풀에서 생성합니다.
- 전투 중에는 `WorldToScreenPoint`로 월드 좌표를 화면 좌표로 변환해 HP UI를 캐릭터 위에 맞춰 갱신합니다.
- HP 비율 갱신 흐름을 분리해 UI 반영 경로를 명확히 했습니다.

### 6. 테스트 씬 기반 성능 검증 흐름
- `TestScene`과 `TestBtn`을 추가해 대량 화살 생성 스트레스 테스트가 가능하도록 구성했습니다.
- 일정 시간 동안 다수의 `Arrow`를 반복 생성하는 코루틴 기반 테스트 흐름을 통해 풀링 구조를 점검할 수 있습니다.

## 기술 스택

- **Engine**: Unity 2022.3.62f2
- **Language**: C#
- **Rendering**: URP (Universal Render Pipeline)
- **Navigation**: Unity AI Navigation / NavMesh 기반 이동
- **UI**: UGUI, TextMeshPro
- **Profiling / Utility**: Unity Memory Profiler, Newtonsoft Json

## 주요 씬

- `TitleScene`
- `LobbyScene`
- `LevelScene`
- `StageScene`
- `BattleScene`
- `CharacterScene`
- `TestScene` - 성능 및 기능 검증용 테스트 씬

## 설계 포인트

### 전투 로직을 “기능 구현”이 아니라 “상태 전이”로 정리
전투는 시간이 지날수록 예외 케이스가 늘어나기 쉽기 때문에, 단순 조건문 확장보다 상태 전이 구조가 유지보수에 유리하다고 판단했습니다.  
현재 구조는 `SEARCH → MOVE → ATTACK` 흐름을 중심으로 하며, 타겟 유무와 상태 이상 여부에 따라 다시 탐색 상태로 복귀하도록 구성했습니다.

### 풀링은 단순 재사용이 아니라 “반환 안정성”까지 고려
오브젝트 풀을 적용할 때 가장 신경 쓴 부분은 **잘못된 반환 상황 방어**였습니다.  
인스턴스 이름 변경이나 부모 변경이 발생해도 풀 키를 잃지 않도록 `InstanceID` 기준 매핑을 유지했고, 미등록 풀 반환 상황에서는 방어적으로 정리되도록 구성했습니다.

### UI도 전투 오브젝트처럼 런타임 자원으로 취급
HP Slider 역시 전투 시작 시마다 생성되는 오브젝트이므로, 프리팹을 직접 생성하기보다 풀에서 꺼내어 사용하는 방향으로 정리했습니다.  
이로 인해 전투 오브젝트와 UI 오브젝트의 생성/회수 정책을 비슷한 방식으로 관리할 수 있습니다.

## 문제 해결 기록

### 1) 반복 생성 오브젝트 관리 문제
- **문제**: 투사체, 스킬 이펙트, HP UI, 스테이지 버튼처럼 반복적으로 생성되는 오브젝트가 많았습니다.
- **해결**: Object Pool을 중심으로 생성/반환 흐름을 통합하고, 반환 시 상태 초기화와 원본 풀 추적 로직을 추가했습니다.
- **결과**: 런타임 생성/회수 경로가 단일화되었고, 테스트 씬에서 반복 검증 가능한 구조를 갖추게 되었습니다.

### 2) 전투 분기 복잡도 증가
- **문제**: 자동 공격, 스킬 사용, 상태 이상 처리까지 섞이면서 전투 흐름이 복잡해질 수 있었습니다.
- **해결**: FSM 구조 안에서 쿨타임과 상태값을 기준으로 공격 분기를 명확히 나누고, 플레이어 자동 공격 경로를 별도로 분리했습니다.
- **결과**: 전투 흐름을 추적하기 쉬워졌고, 이후 클래스별 전투 규칙 확장에도 대응하기 쉬운 구조가 되었습니다.

### 3) 스테이지 UI 재진입 시 정리 문제
- **문제**: 스테이지 패널 재진입 시 기존 버튼 정리와 상태 반영이 함께 필요했습니다.
- **해결**: 패널 시작 시 기존 버튼을 모두 반환하고, 해금/클리어 상태에 따라 버튼 프리팹을 다시 생성하는 방식으로 정리했습니다.
- **결과**: 스테이지 상태 표현과 UI 재생성 흐름이 명확해졌습니다.

## 폴더 구조

```bash
Assets/
├─ 01_Scenes/                # 씬 파일
├─ 02_Scripts/               # 핵심 게임 로직
└─ Resources/
   └─ 04_Prefabs/
      ├─ Attack/             # 투사체 / 공격 관련 프리팹
      ├─ Class/              # 클래스별 유닛 프리팹
      │  ├─ Enemy/
      │  ├─ NewClass/
      │  └─ Unit/
      │     ├─ Enemy/
      │     └─ Team/
      ├─ Player/             # 플레이어 프리팹
      └─ Stage/              # 스테이지 / 진행 UI 프리팹
```

## 실행 방법

1. **Unity Hub**에서 프로젝트를 엽니다.
2. 에디터 버전은 **Unity 2022.3.62f2**를 사용하는 것을 권장합니다.
3. 기본 진입은 아래 씬 중 하나에서 시작합니다.
   - `Assets/01_Scenes/TitleScene.unity`
   - `Assets/01_Scenes/LobbyScene.unity`
4. 오브젝트 풀링 또는 대량 생성 테스트는 아래 씬에서 확인합니다.
   - `Assets/01_Scenes/TestScene.unity`

## 주요 코드 위치

- 전투 FSM: [`Assets/02_Scripts/02_Soldier/FSM/FSM.cs`](./Assets/02_Scripts/02_Soldier/FSM/FSM.cs)
- 오브젝트 풀: [`Assets/02_Scripts/97_Manager/ObjectPool/ObjectPool.cs`](./Assets/02_Scripts/97_Manager/ObjectPool/ObjectPool.cs)
- HP UI 동기화: [`Assets/02_Scripts/04_UI/HpCanvas.cs`](./Assets/02_Scripts/04_UI/HpCanvas.cs)
- 스테이지 패널: [`Assets/02_Scripts/52_StageScene/StagePanel.cs`](./Assets/02_Scripts/52_StageScene/StagePanel.cs)
- 스트레스 테스트 버튼: [`Assets/02_Scripts/97_Manager/ObjectPool/TestBtn.cs`](./Assets/02_Scripts/97_Manager/ObjectPool/TestBtn.cs)

## 문서

- 최근 변경 사항: [`docs/RECENT_CHANGES.md`](./docs/RECENT_CHANGES.md)
- 프리팹 구조: [`docs/PREFAB_STRUCTURE.md`](./docs/PREFAB_STRUCTURE.md)
- 오브젝트 풀 리팩토링 계획: [`ObjectPool.md`](./ObjectPool.md)
- 리팩토링 체크리스트: [`refactoringPlan.md`](./refactoringPlan.md)

## 앞으로의 개선 계획

현재 문서 기준으로는 아래 항목들이 다음 개선 포인트입니다.

- UI 갱신을 이벤트 기반 구조로 전환
- `Find` 계열 호출 제거 및 참조 주입 구조 강화
- Singleton 초기화 안정성 정리
- `Soldier` 중심 책임 분리(SRP) 진행
- Pooling 적용 전/후 Profiler 결과를 README에 시각 자료로 추가

## 회고

이 프로젝트는 단순히 “전투가 돌아가는 게임”을 만드는 것보다,  
**반복 생성되는 오브젝트를 어떻게 관리할지**, **전투 흐름을 어떻게 구조화할지**, **테스트 가능한 형태로 개선 작업을 남길지**에 더 집중한 작업입니다.

특히 Object Pool, FSM, Stage UI 재구성은 기능 구현과 구조 개선을 함께 고민한 흔적으로 남겨두고 싶었습니다.

---

개인 프로젝트이며, 현재 저장소는 **GitLab 미러링** 형태로 관리되고 있습니다.
