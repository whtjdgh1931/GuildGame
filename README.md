# GuildGame

<p align="center">
  <img src="./guildgame-cover.png" alt="GuildGame cover" width="100%" />
</p>

<p align="center">
  Unity 기반 2D 길드/전투 포트폴리오 프로젝트
</p>

---

## 프로젝트 소개

GuildGame은 **길드 운영과 전투 흐름**을 중심으로 구성한 Unity 2D 프로젝트입니다.  
단순 전투 화면 구현에 그치지 않고, **전투 FSM**, **병사 관리**, **HP UI 동기화**, **스테이지 진행**, **Object Pool 기반 재사용 구조**, **테스트 씬 검증 흐름**까지 함께 다루는 것을 목표로 했습니다.

### 프로젝트 개요

| 항목 | 내용 |
|---|---|
| 프로젝트명 | GuildGame |
| 저장소 형태 | 개인 프로젝트 / Public Repository |
| 저장소 비고 | GitHub 공개용 미러 저장소 |
| 개발 인원 | 1인 |
| 개발 기간 | 2025.09 ~ |
| 사용 엔진 | Unity 2022.3.62f2 |
| 사용 언어 | C# |
| 주요 패키지 | URP, AI Navigation, TextMeshPro, UGUI, Memory Profiler, Newtonsoft JSON |
| 주요 대상 플랫폼 | PC (Standalone) |
| 플랫폼 설정 값 | Android Min SDK 22 / Target SDK 34, iOS Target 12.0 |
| 현재 배포 상태 | GitHub Release 미사용 |
| 데모 영상 | 준비 중 |

### 이런 점을 중점으로 설계했습니다

- 전투 로직이 커져도 상태 관리가 무너지지 않도록 **FSM 기반 구조**를 유지
- 병사 수가 늘어나도 UI 반영과 전투 상태가 어긋나지 않도록 **동기화 흐름**을 정리
- 반복 생성 오브젝트가 많은 전투 환경을 고려해 **Object Pool 재사용 구조**를 적용


---

## 설치 및 실행 방법

### 1) 소스 코드로 실행

```bash
git clone https://github.com/whtjdgh1931/GuildGame.git
cd GuildGame
```

1. Unity Hub에서 프로젝트를 추가합니다.
2. **Unity 2022.3.62f2** 버전으로 엽니다.
3. `Assets/01_Scenes/TitleScene.unity` 를 열어 실행합니다.
4. Play 버튼으로 프로젝트를 확인합니다.

### 2) 권장 시작 씬

- `Assets/01_Scenes/TitleScene.unity`  
  → 전체 플레이 흐름의 시작점입니다.

### 3) Build 기준

Unity 메뉴에서 **File > Build Settings** 로 이동한 뒤, 포함된 씬을 확인하고 Standalone PC 기준으로 빌드합니다.

- `TitleScene`
- `LobbyScene`
- `BattleScene`
- `LevelScene`
- `StageScene`
- `CharacterScene`


---

## 프로젝트 구조 및 코드 컨벤션

### 폴더 구조

```text
GuildGame
├─ Assets
│  ├─ 01_Scenes
│  ├─ 02_Scripts
│  │  ├─ 01_Player
│  │  ├─ 02_Soldier
│  │  ├─ 03_Attack
│  │  ├─ 04_UI
│  │  ├─ 47_HomeScene
│  │  ├─ 48_LevelScene
│  │  ├─ 49_TitleScene
│  │  ├─ 50_LobbyScene
│  │  ├─ 51_CharacterScene
│  │  ├─ 52_StageScene
│  │  ├─ 97_Manager
│  │  ├─ 98_ScriptableObject
│  │  └─ 99_Constants
│  ├─ Resources
│  │  └─ 04_Prefabs
│  └─ ...
├─ Packages
├─ ProjectSettings
├─ docs
│  ├─ PREFAB_STRUCTURE.md
│  └─ RECENT_CHANGES.md
├─ ObjectPool.md
└─ refactoringPlan.md
```

### 에셋 관리 방식

런타임에서 사용하는 주요 프리팹은 `Assets/Resources/04_Prefabs` 기준으로 관리합니다.

- `Attack/` : 투사체, 공격 관련 프리팹
- `Class/` : 클래스별 전투 유닛 프리팹
- `Player/` : 플레이어 캐릭터 프리팹
- `Stage/` : 스테이지 진행 관련 UI 프리팹

`Class` 하위는 목적에 따라 다시 분리합니다.

- `Class/Enemy/` : 적 캐릭터 프리팹
- `Class/NewClass/` : 클래스 원본 또는 신규 규격 프리팹
- `Class/Unit/Enemy/` : 전투 배치용 적 유닛 프리팹
- `Class/Unit/Team/` : 전투 배치용 아군 유닛 프리팹

### 아키텍처 / 코드 스타일

이 프로젝트는 기능별 책임을 나누는 방향으로 구조를 정리했습니다.

- **전투 로직**: `FSM` 중심으로 상태 전환을 관리
- **UI 처리**: HP Canvas 및 Slider 갱신 흐름을 전투 로직과 분리
- **반복 생성 오브젝트**: `ObjectPool`을 통해 재사용
- **씬 전용 스크립트 분리**: `49_TitleScene`, `50_LobbyScene`, `52_StageScene` 처럼 씬/기능 단위 폴더를 별도 운영
- **공용 영역 분리**: `97_Manager`, `98_ScriptableObject`, `99_Constants`

### Commit Message Prefix

현재 히스토리에는 `feat`, `feature`, 한글 메시지가 혼용되어 있습니다.  
이 README 기준으로는 아래 Prefix를 사용하는 방식을 권장합니다.

| Prefix | 설명 | 예시 |
|---|---|---|
| `feat` | 기능 추가 | `feat: add stage button pooling` |
| `fix` | 버그 수정 | `fix: hp bar screen position bug` |
| `refactor` | 구조 개선 | `refactor: split object pool release flow` |
| `docs` | 문서 수정 | `docs: update README` |
| `test` | 테스트 코드 / 검증 로직 | `test: add projectile stress tool` |
| `chore` | 설정, 패키지, 빌드 관련 변경 | `chore: sync package manifest` |

### PR 가이드

개인 프로젝트 기준이지만, 이력 관리와 협업 대응력을 보여주기 위해 아래 규칙을 사용합니다.

- PR 제목은 `type: summary` 형식으로 작성
- 변경 목적 / 변경 파일 / 테스트 내용 / 확인한 씬을 본문에 정리
- UI 변경이 있다면 스크린샷 또는 GIF를 첨부
- Object Pool, FSM 같은 구조 변경은 **문제 → 해결 → 결과** 순서로 요약
- 머지 전에는 최소 한 번 이상 플레이 테스트 또는 대상 씬 테스트 수행

### 주요 Branch 설명

| 브랜치명 | 용도 |
|---|---|
| `feat-2D` | 기본 작업 브랜치 / 통합 기준 브랜치 |
| `feat-supplement` | 보완 기능 및 문서 정리용 브랜치 |
| `objectPool` | Object Pool 실험 및 적용 작업 브랜치 |

---

## 주요 기능 소개

### 1) 전투 FSM 기반 상태 제어

전투 로직은 입력, 이동, 타겟 탐색, 기본 공격, 스킬 사용, 상태 이상 처리까지 얽히기 쉬운 영역입니다.  
GuildGame은 이를 **SEARCH / MOVE / ATTACK** 흐름 중심으로 나누고, 상태 전환 책임을 명확히 하는 방식으로 구성했습니다.

핵심 포인트

- 타겟이 없으면 `SEARCH` 상태로 복귀
- 플레이어일 때 `Player_Attack.DoAutoAttack()` 우선 사용
- `curTime`, `curAttackTime` 기준으로 스킬/기본 공격 분기
- `isStun`, `isTaunt` 상태에 따라 공격 제한 처리

**핵심 코드**  
[`Assets/02_Scripts/02_Soldier/FSM/FSM.cs`](./Assets/02_Scripts/02_Soldier/FSM/FSM.cs)

---

### 2) HP Canvas 생성 및 UI 동기화

전투 씬에서는 실제 HP 값과 화면상의 HP 바가 어긋나지 않는 것이 중요합니다.  
이 프로젝트에서는 전투 시작 시 팀/적군의 HP Slider를 생성하고, 매 프레임 캐릭터 위치를 화면 좌표로 변환해 HP 바 위치와 수치를 갱신합니다.

핵심 포인트

- 전투 시작 시 아군/적군 각각 HP Slider 생성
- 월드 좌표를 화면 좌표로 변환해 HP 바 위치 갱신
- `SetHpRatio()` 로 체력 비율 실시간 반영
- 전투 로직과 UI 반영 타이밍을 최대한 맞추도록 구성

**핵심 코드**  
[`Assets/02_Scripts/04_UI/HpCanvas.cs`](./Assets/02_Scripts/04_UI/HpCanvas.cs)

---

### 3) Stage 버튼 생성 / 반환 구조

스테이지 패널은 월드와 레벨 진행 상태에 따라 버튼 종류가 달라지고, 패널 재진입 시 재사용까지 고려해야 합니다.  
GuildGame은 버튼 프리팹을 상태에 따라 선택하고, 패널 재진입 시 기존 버튼을 반환하는 방식으로 구조를 정리했습니다.

핵심 포인트

- 현재 월드/레벨 상태에 맞는 버튼 프리팹 선택
- Stage 버튼을 Object Pool 기반으로 생성
- 패널 재진입 시 `ReleaseStageButtons()` 로 일괄 반환
- `Stage_{world}_{level}_Cleared` 키로 해금/클리어 상태 관리

**핵심 코드**  
[`Assets/02_Scripts/52_StageScene/StagePanel.cs`](./Assets/02_Scripts/52_StageScene/StagePanel.cs)

---

### 4) Object Pool 기반 재사용 구조

투사체, 이펙트, UI 등 반복 생성되는 오브젝트가 많은 구조에서는 `Instantiate / Destroy` 비용이 누적될 수 있습니다.  
이 프로젝트는 Object Pool을 통해 반복 생성 오브젝트를 재사용하고, 반환 안정성을 보강하는 방향으로 개선했습니다.

핵심 포인트

- 풀에서 오브젝트를 가져오고 사용 후 반환
- `InstanceID -> poolKey` 매핑을 통해 원본 풀 복귀 안정성 강화
- null / 미등록 풀 / 비풀 모드 방어 처리
- 이후 투사체, 이펙트, UI까지 확장 가능한 구조

**핵심 코드**  
[`Assets/02_Scripts/97_Manager/ObjectPool/ObjectPool.cs`](./Assets/02_Scripts/97_Manager/ObjectPool/ObjectPool.cs)

---

### 5) TestScene 기반 검증 흐름

기능이 단순히 “작동한다”에서 끝나지 않고, 반복 상황에서도 버티는지 확인하기 위해 테스트 씬과 테스트 버튼을 별도로 운영했습니다.

핵심 포인트

- 지정 시간 / 횟수 동안 투사체 반복 생성
- 간격 발사 코루틴을 이용한 부하 테스트
- Object Pool 참조 자동 탐색
- 병사 수 증가, UI 갱신 반복, 투사체 생성 반복 검증

**핵심 코드**  
[`Assets/02_Scripts/97_Manager/ObjectPool/TestBtn.cs`](./Assets/02_Scripts/97_Manager/ObjectPool/TestBtn.cs)

---

## 주요 씬 구성

| 씬 | 역할 |
|---|---|
| `TitleScene` | 프로젝트 시작 진입 씬 |
| `LobbyScene` | 전투 진입 전 허브 역할 |
| `BattleScene` | 전투 FSM, 병사, 스킬, UI 검증의 중심 씬 |
| `LevelScene` | 레벨 단위 흐름 확인 |
| `StageScene` | 월드/레벨 진행 및 스테이지 버튼 흐름 검증 |
| `CharacterScene` | 캐릭터 상태 및 정보 확인 |
| `EnemyScene/1-1 ~ 1-6` | 스테이지 단위 적 구성 테스트 |

---

## 문서

저장소에는 README 외에도 구조와 변경 이력을 확인할 수 있는 문서가 포함되어 있습니다.

- [RECENT_CHANGES.md](./docs/RECENT_CHANGES.md)  
  최근 반영된 기능과 수정 사항 요약
- [PREFAB_STRUCTURE.md](./docs/PREFAB_STRUCTURE.md)  
  프리팹 폴더 구조 및 운영 규칙
- [ObjectPool.md](./ObjectPool.md)  
  Object Pool 관련 설계 및 정리 문서
- [refactoringPlan.md](./refactoringPlan.md)  
  구조 개선 계획 및 리팩토링 체크리스트

---

## 개선 방향

현재 문서와 코드 기준으로, 이후에는 아래 방향을 우선적으로 개선할 계획입니다.

- Update 의존도를 줄이고 **이벤트 기반 UI 구조**로 전환
- `Find` 계열 호출을 줄이고 **참조 주입 / Register 패턴** 강화
- Singleton 초기화 순서와 중복 생성을 더 명확하게 정리
- `Soldier` 의 책임을 HP / Stat / Combat 단위로 더 세분화
- README에 **문제 → 해결 → 결과** 형태의 성능 측정 기록 추가

---

## 마무리

GuildGame은 단순히 기능을 나열한 프로젝트가 아니라,  
**게임 기능이 늘어나도 구조가 유지되는지**, 그리고 **반복 상황에서도 버티는지**를 확인하는 방향으로 정리한 포트폴리오 프로젝트입니다.

특히 아래 포인트를 중심으로 보시면 좋습니다.

- 전투 FSM을 이용한 상태 제어
- 병사와 UI의 동기화 구조
- 스테이지 버튼 및 반복 생성 오브젝트의 재사용 처리
- 테스트 씬을 통한 구조 검증
