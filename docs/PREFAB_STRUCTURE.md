# Prefab Structure

## 루트 경로
- `Assets/Resources/04_Prefabs`

## 하위 구조
- `Attack/`: 투사체/공격 관련 프리팹
- `Class/`: 클래스별 전투 유닛 프리팹
- `Player/`: 플레이어 캐릭터 프리팹
- `Stage/`: 스테이지/진행 UI 프리팹

## Class 폴더 구성
- `Class/Enemy/`: 적 캐릭터 프리팹
- `Class/NewClass/`: 클래스 원본(또는 신규 규격) 프리팹
- `Class/Unit/Enemy/`: 전투 배치용 적 유닛 프리팹
- `Class/Unit/Team/`: 전투 배치용 아군 유닛 프리팹

## Player 폴더 구성
- `PlayerArcher.prefab`
- `PlayerAssassin.prefab`
- `PlayerMagician.prefab`
- `PlayerTanker.prefab`
- `PlayerWarrior.prefab`

## 운영 규칙 (권장)
- 런타임 생성 대상은 ObjectPool에서 가져오고 직접 `Instantiate`를 최소화한다.
- 전투 밸런스 스탯은 CSV + `ClassManager`를 우선 사용하고 프리팹에는 최소 기본값만 둔다.
- 플레이어/팀/적군 프리팹은 동일 클래스라도 목적(조작, AI, 배치)에 맞게 분리 유지한다.
- 공통 변경(애니메이터, 렌더 순서, 콜라이더)은 `NewClass` 또는 베이스 프리팹에서 먼저 반영 후 파생 프리팹에 전파한다.
