# GuildGame

Unity 기반 2D 길드/전투 프로젝트입니다.  
클래스별 유닛 생성, 전투 FSM, 스테이지 진행, 오브젝트 풀링 기반 런타임 최적화를 포함합니다.

## 기술 스택
- Unity 2022.x (URP)
- C#
- NavMesh 기반 이동
- Object Pool 기반 런타임 생성/회수

## 주요 씬
- `TitleScene`
- `LobbyScene`
- `LevelScene`
- `StageScene`
- `BattleScene`
- `CharacterScene`
- `TestScene` (성능/기능 테스트용)

## 최근 반영 사항 (2026-03-03)
- 전투 FSM 개선
  - 플레이어 유닛 자동공격 경로 분리 (`Player_Attack.DoAutoAttack`)
  - 스킬/기본공격 쿨타임 분기 정리
  - 스턴/도발 상태 처리 보강
- HP UI 갱신 흐름 정리
  - 시작 시 팀/적군 HP 슬라이더를 오브젝트풀에서 생성
  - 전투 중 월드 좌표 -> 화면 좌표 동기화
- Stage 버튼 생성 방식 개선
  - 스테이지 버튼을 오브젝트풀에서 생성/반환
  - 클리어/잠금/진행 가능 상태별 프리팹 분기
- ObjectPool 안정성 보강
  - 인스턴스 ID 기준 원본 풀 키 추적
  - 잘못된 풀 반환 상황 방어 처리 추가
  - 지연 반환, 전체 반환/클리어 함수 유지
- 테스트 환경 추가
  - `TestScene` 추가
  - `TestBtn` 추가 (화살 대량 발사 스트레스 테스트)
- 프리팹 구성 보완
  - Player/Class(Unit)/NewClass 계열 프리팹 업데이트
  - 전투/미리보기용 프리팹 경로 정리

## 폴더 개요
- `Assets/01_Scenes`: 씬 파일
- `Assets/02_Scripts`: 게임 로직
- `Assets/Resources/04_Prefabs`: 전투/플레이어/스테이지 프리팹
- `Packages`: Unity 패키지 의존성

## 실행 방법
1. Unity Hub에서 프로젝트를 연다.
2. `Assets/01_Scenes/TitleScene.unity` 또는 `LobbyScene.unity`부터 실행한다.
3. 전투/풀링 테스트는 `TestScene.unity`에서 실행한다.

## 문서
- 최근 변경 상세: `docs/RECENT_CHANGES.md`
- 프리팹 구조 가이드: `docs/PREFAB_STRUCTURE.md`
