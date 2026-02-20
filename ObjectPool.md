# 🔥 GuildGame Object Pooling Refactoring Plan

> 목표: Instantiate / Destroy 제거  
> 목적: GC Alloc 감소 + 프레임 안정화 + 면접 어필용 성과 확보

---

# 🥇 1. 설계 단계 체크리스트

## 🎯 풀링 대상 정의

- [ ] Arrow (투사체)
- [ ] Archer SkillEffect (스킬 이펙트)
- [ ] Magicial_Explosion (폭발 이펙트)
- [ ] 기타 투사체/이펙트

---

## 🎯 설계 결정

- [ ] Prefab 참조 기반 Dictionary 구조 사용
- [ ] Stack 자료구조 사용 (빠른 Push/Pop)
- [ ] Prewarm 지원 (초기 N개 생성)
- [ ] 풀 부족 시 자동 확장 허용
- [ ] Release 시 상태 초기화 정책 정의

---

# 🥈 2. PoolManager 구현 체크리스트

## 📁 폴더 구조

- [ ] Assets/02_Scripts/Core/Pooling 폴더 생성
- [ ] PoolManager.cs 생성
- [ ] IPoolable.cs 생성 (선택)

---

## 🧱 PoolManager 기능

- [ ] Dictionary<Prefab, Stack<GameObject>> 생성
- [ ] Get(prefab, position, rotation) 구현
- [ ] Release(instance) 구현
- [ ] Prewarm(prefab, count) 구현
- [ ] PoolContainer 자동 생성
- [ ] 잘못된 Release 방어 로직 추가

---

## 🧩 IPoolable 인터페이스 (선택)

- [ ] OnSpawned() 정의
- [ ] OnDespawned() 정의
- [ ] Reset 로직은 OnDespawned에서 수행

---

# 🥉 3. Arrow 풀링 적용

## 🔄 변경 사항

- [ ] Player_Archer에서 Instantiate 제거
- [ ] Pool.Get(arrowPrefab)으로 변경
- [ ] Arrow에서 Destroy(gameObject) 제거
- [ ] target == null 시 Release로 변경
- [ ] 충돌 시 Release로 변경

---

## ♻️ Reset 체크

- [ ] target null 초기화
- [ ] arrowPower 초기화
- [ ] arrowSpeed 초기화
- [ ] transform scale 초기화
- [ ] rotation 초기화
- [ ] velocity 초기화 (있다면)

---

# 🏅 4. Skill Effect 풀링 적용

- [ ] Instantiate(skillEffect) 제거
- [ ] Pool.Get(skillEffectPrefab) 사용
- [ ] Destroy(skillEffect) → Release로 변경
- [ ] ParticleSystem Stop + Clear 처리
- [ ] TrailRenderer Clear 처리

---

# 🏅 5. 폭발 이펙트 풀링

- [ ] Destroy(gameObject, t) 제거
- [ ] 코루틴 또는 타이머 후 Release
- [ ] OverlapSphere 로직 유지
- [ ] 재사용 시 위치 초기화 확인

---

# 📊 6. 성능 검증 체크리스트

## 🔍 적용 전

- [ ] 200회 투사체 발사 테스트
- [ ] Profiler GC Alloc 확인
- [ ] CPU Spike 확인

---

## 🔍 적용 후

- [ ] 동일 조건 테스트
- [ ] GC Alloc 감소 확인
- [ ] Instantiate 호출 제거 확인
- [ ] Destroy 호출 제거 확인

---

# 📈 7. 포트폴리오 기록

- [ ] 적용 전/후 Profiler 캡처 저장
- [ ] README에 3줄 요약 추가

예시:

문제: 투사체 생성 시 GC Alloc 및 프레임 드랍 발생  
해결: Object Pool 적용 (Prewarm + Reset 정책 포함)  
결과: GC Alloc 감소 및 프레임 안정화

---

# 🧠 8. 면접 대비 체크

- [ ] 왜 Instantiate가 비용이 큰지 설명 가능
- [ ] GC가 언제 발생하는지 설명 가능
- [ ] Reset을 왜 반납 시 수행하는지 설명 가능
- [ ] Prewarm이 필요한 이유 설명 가능

---

# 🎯 완료 기준

- [ ] Arrow 완전 풀링 적용
- [ ] SkillEffect 풀링 적용
- [ ] Destroy 코드 제거
- [ ] Profiler 결과 확보
- [ ] README 반영 완료
