# 📌 GuildGame Refactoring Plan

> 목표: 구조 개선 + 성능 개선 + 면접 대응력 강화  
> 우선순위: 🔥 Object Pool 최우선

---

# 🥇 1. Object Pool 도입 (최우선)

## 🎯 목표
- Instantiate / Destroy 제거
- GC Alloc 감소
- 성능 안정화
- 포트폴리오에 성과 기록

---

## 1️⃣ Pool 시스템 구현

- [ ] PoolManager 생성
- [ ] Get() / Release() 메서드 구현
- [ ] Prewarm 기능 구현 (초기 생성 수량 지정)
- [ ] PoolContainer 정리 구조 추가
- [ ] Poolable 초기화/해제 인터페이스 정의 (선택)

---

## 2️⃣ 적용 대상 선정 (1개 이상 필수)

### 우선 적용 후보
- [ ] 투사체
- [ ] 이펙트
- [ ] HP바
- [ ] Soldier 생성

---

## 3️⃣ 실제 적용

- [ ] 기존 Instantiate 제거
- [ ] Destroy 제거
- [ ] Spawn → Release 흐름으로 변경
- [ ] 반환 시 위치/회전/상태 초기화

---

## 4️⃣ 성능 확인

- [ ] Profiler 적용 전 캡처
- [ ] Profiler 적용 후 캡처
- [ ] GC Alloc 감소 여부 확인
- [ ] README에 결과 정리

---

# 🥈 2. 이벤트 기반 UI 구조 전환

## 🎯 목표
- Update 의존 제거
- 이벤트 기반 구조 적용

---

- [ ] UIManager.Update() 제거
- [ ] OnHpChanged 이벤트 추가
- [ ] HP 변경 시 이벤트 발생 구조로 수정
- [ ] UI는 이벤트 구독 방식으로 변경

---

# 🥉 3. ClassManager 구조 개선

## 🎯 목표
- switch 제거
- 확장성 향상

---

- [ ] Dictionary 기반 데이터 관리로 변경
- [ ] 문자열 의존 최소화
- [ ] Constants 기반 키 통일

---

# 🏅 4. Soldier 책임 분리

## 🎯 목표
- SRP 적용
- 유지보수성 향상

---

- [ ] SoldierStat 분리
- [ ] SoldierLife(Health) 분리
- [ ] SoldierCombat 분리
- [ ] UI 의존 제거

---

# 🏅 5. FSM 개선 (선택)

- [ ] 인터페이스 기반 상태 패턴 적용
- [ ] Enter / Update / Exit 구조로 변경

---

# 📈 완료 후 README에 추가할 내용

- 적용한 시스템
- 개선 전 문제점
- 개선 방법
- 성능 변화 결과
- 구조 개선 포인트

---

# 🧠 면접 대비 포인트

- Object Pool 도입 이유 설명 가능
- GC 발생 원인 설명 가능
- 이벤트 기반 설계 장점 설명 가능
- 책임 분리 이유 설명 가능
