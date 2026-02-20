# ⚔️ Mercenary Squad (출격! 용병단)
Strategy Simulation + Action  

> 전략 시뮬레이션과 실시간 전투를 결합한 1인 Unity 프로젝트입니다.  
> 시스템 설계와 데이터 기반 구조에 집중하여 제작했습니다.

---

## 🎮 프로젝트 개요

- 장르: 전략 시뮬레이션 + 액션
- 플랫폼: PC / Mobile
- 개발 인원: 1인 개발

🎥 Gameplay Video  
👉 [(소개 영상)](https://youtu.be/nZN4ruosJbg?si=mAEbad9o8nKZTsq8)

---

# 🏗 주요 시스템

## 1️⃣ 캐릭터 생성 및 배치 시스템

### 구현 내용
- 드래그 & 드롭 기반 배치
- 모바일 터치 입력 분기 처리
- 미리보기 오브젝트 제공 (공격 범위 시각화)
- 배치 취소 기능 구현

### 시스템 흐름

```
버튼 클릭
→ StartDrag()
→ 미리보기 생성
→ 위치 이동
→ 입력 해제 시 캐릭터 생성
→ 스탯 적용
```

### 핵심 코드 예시

```csharp
public void StartDrag(string className)
{
    if (previewInstance != null) Destroy(previewInstance);
    previewInstance = MakeSoldierPreview(className);
    isDrag = true;
}
```

---

## 2️⃣ CSV 기반 캐릭터 스탯 시스템

### 구현 내용
- 직업별 레벨 데이터 CSV 관리
- ClassManager Singleton 구조
- 캐릭터 생성 시 레벨 기반 스탯 적용

```csharp
public List<Dictionary<string, object>> GetLevelData(string className)
{
    switch (className)
    {
        case "Tanker": return tankerData;
        ...
    }
}
```

### 설계 의도
- 하드코딩 제거
- 레벨 확장 시 CSV 수정만으로 대응
- 데이터와 로직 분리

---

## 3️⃣ FSM 기반 캐릭터 AI

### 구현 내용
- Idle / Move / Attack 상태 분리
- 상태 기반 행동 제어

```csharp
switch (currentState)
{
    case State.Idle:
        break;
    case State.Move:
        break;
    case State.Attack:
        break;
}
```

---

# 📈 개선 사항

- UI 구조 개선
- 로비 / 캐릭터 선택 씬 분리
- HP바 표시 추가
- 경험치 시스템 도입

---

# 🚧 향후 개선 예정

- 이벤트 기반 UI 구조 전환
- FSM 인터페이스 기반 리팩토링
- Update 의존도 감소

---

# 🛠 Tech Stack

- Unity 2022.3.62f2
- C#
- CSV 데이터 관리
- Git

---

> 개인 프로젝트지만 구조 설계 경험을 보여주기 위해 제작했습니다.
