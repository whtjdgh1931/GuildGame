# Recent Changes

기준일: 2026-03-03

## 요약
- 전투 로직(FSM)과 UI(HP Canvas), 스테이지 버튼 로직, 오브젝트풀이 동시에 정리됨.
- 성능 검증을 위한 테스트 버튼/테스트 씬이 추가됨.
- 플레이어/클래스 관련 프리팹이 업데이트됨.

## 코드 변경 상세

### 1) FSM 전투 흐름 개선
- 파일: `Assets/02_Scripts/02_Soldier/FSM/FSM.cs`
- 반영 내용:
  - 플레이어(`Player`)일 때 `Player_Attack.DoAutoAttack()` 우선 사용
  - `curTime`, `curAttackTime` 기반 스킬/기본공격 분기 명확화
  - `isStun`, `isTaunt` 상태와 결합된 공격 제한 처리
  - 목표 미존재 시 `SEARCH` 상태로 복귀

### 2) HP 캔버스 생성/동기화
- 파일: `Assets/02_Scripts/04_UI/HpCanvas.cs`
- 반영 내용:
  - 전투 시작 시 아군/적군 각각 HP 슬라이더를 ObjectPool에서 생성
  - 매 프레임 캐릭터 위치를 화면 좌표로 변환해 HP 바 위치 갱신
  - 슬라이더 비율(`SetHpRatio`) 실시간 반영

### 3) Stage 버튼 풀링 적용
- 파일: `Assets/02_Scripts/52_StageScene/StagePanel.cs`
- 반영 내용:
  - 월드/레벨 상태에 맞는 버튼 프리팹 선택 후 ObjectPool로 생성
  - 패널 재진입 시 기존 버튼 일괄 반환(`ReleaseStageButtons`)
  - `Stage_{world}_{level}_Cleared` 키로 해금/클리어 상태 관리

### 4) ObjectPool 안정성 보강
- 파일: `Assets/02_Scripts/97_Manager/ObjectPool/ObjectPool.cs`
- 반영 내용:
  - `objectPoolKeys`(InstanceID -> 원본 poolKey) 맵 추가
  - 이름 변경/부모 변경 상황에서도 원본 풀로 정확히 반환되도록 보강
  - null/미등록 풀/비풀 모드 방어 처리

### 5) 대량 투사체 테스트 도구 추가
- 파일: `Assets/02_Scripts/97_Manager/ObjectPool/TestBtn.cs`
- 반영 내용:
  - 지정 시간/횟수 동안 `Arrow`를 풀에서 반복 생성
  - 대상 타겟 연결 및 간격 발사 코루틴 제공
  - ObjectPool 참조 자동 탐색 로직 포함

## 씬/프리팹 반영
- 신규 씬: `Assets/01_Scenes/TestScene.unity`
- 수정 프리팹(대표):
  - `Assets/Resources/04_Prefabs/Player/*.prefab`
  - `Assets/Resources/04_Prefabs/Class/NewClass/*.prefab`
  - `Assets/Resources/04_Prefabs/Class/Unit/Team/*.prefab`

## 패키지
- 파일:
  - `Packages/manifest.json`
  - `Packages/packages-lock.json`
- 반영 내용: 패키지 버전/잠금 정보 동기화
