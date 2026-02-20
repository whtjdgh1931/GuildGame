# 📌 GuildGame Refactoring Checklist (Code-based)

> Scope: 내 코드(Assets/02_Scripts) 중심. 외부 에셋 폴더는 제외.
> Goal: 성능 + 구조 개선 + 면접 어필(개선 전/후 기록)

---

## 🥇 P0. Object Pool (최우선 / 반드시 성과 남기기)

### 0-1) 적용 대상 선정 (우선순위 높은 것부터)
- [ ] Arrow(투사체) 풀링 적용 (Arrow.cs: Destroy 제거 → Release로 변경)
- [ ] Archer SkillEffect 풀링 적용 (Player_Archer: Instantiate/Destroy 제거)
- [ ] Magicial_Explosion(폭발 이펙트) 풀링 적용 (Destroy(gameObject, t) 제거)
- [ ] (선택) 기타 이펙트/투사체 동일 패턴 확장

### 0-2) Pool 시스템 구현
- [ ] PoolManager(or ObjectPool<T>) 생성 (Core/Pooling 폴더)
- [ ] API 정의: Get(prefab), Release(instance)
- [ ] Prewarm(초기 N개 생성) 지원
- [ ] 반환 시 Reset 규칙 정의
  - [ ] 위치/회전 초기화
  - [ ] 부모(PoolContainer)로 이동
  - [ ] SetActive(false)
- [ ] Poolable 훅(선택)
  - [ ] IPoolable.OnSpawned()
  - [ ] IPoolable.OnDespawned()

### 0-3) Arrow 풀링 마이그레이션
- [ ] Player_Archer: Instantiate(arrowPrefab) → Pool.Get(arrowPrefab)
- [ ] Arrow: Destroy(gameObject) → Pool.Release(this)
- [ ] Arrow: 타겟 null 처리도 Release로 변경
- [ ] Arrow: 재사용 시 값 초기화(타겟/스피드/데미지/스케일)

### 0-4) Effect 풀링 마이그레이션
- [ ] Player_Archer StartSkillEffect: Instantiate/Destroy → Pool.Get/Release
- [ ] Magicial_Explosion: Destroy(gameObject, 0.5f) → Delay 후 Release
- [ ] Particle/Trail reset 필요 여부 확인 후 OnDespawned에서 처리

### 0-5) 성능 측정(포트폴리오용)
- [ ] Profiler 캡처(적용 전): 투사체 100~200회 생성 시 GC/프레임 확인
- [ ] Profiler 캡처(적용 후): GC Alloc 감소 확인
- [ ] README에 “문제 → 해결 → 결과” 3줄 기록

---

## 🥈 P1. Update 의존 제거 (이벤트 기반 구조로)

### 1-1) UI Update 제거
- [ ] UIManager.Update()에서 매 프레임 UI 갱신 제거
- [ ] Soldier(또는 Health)에서 OnHpChanged 이벤트 발행
- [ ] UI는 이벤트 구독으로만 Slider 갱신

### 1-2) SoldierManager Update 최소화
- [ ] 매 프레임 RemoveAll / 리스트 정리 로직 제거 또는 빈도 축소
- [ ] Spawn/Die 시점에 Register/Unregister로 리스트 관리
- [ ] 승/패 판단도 이벤트 기반(카운트 감소)으로 전환

---

## 🥉 P2. Find 계열 제거 (성능/안정성)

- [ ] GameObject.Find 사용 제거 (UIManager 등)
- [ ] FindGameObjectsWithTag 사용 제거 (SoldierManager StartBattle 등)
- [ ] SerializeField로 참조 주입
- [ ] 동적 생성 객체는 Register 패턴 적용
  - [ ] SoldierSpawn 시 Register
  - [ ] Soldier Die 시 Unregister

---

## 🏅 P3. Singleton 정리 (안전한 초기화)

- [ ] instance 초기화는 Awake에서 처리
- [ ] 중복 생성 방지(있으면 Destroy)
- [ ] DontDestroyOnLoad 사용 여부 명확히 결정(필요한 것만)
- [ ] Instance() 메서드 null 방어 및 초기화 순서 보장

---

## 🏅 P4. 책임 분리(SRP)

- [ ] Soldier에서 Health 분리 (HP/Shield/Die)
- [ ] Soldier에서 Stat 분리 (레벨/스탯 적용)
- [ ] Combat/Attack 분리 (타겟팅/공격)
- [ ] UI가 Soldier 컴포넌트 직접 탐색(GetComponent)하지 않도록 구조 개선

---

## 📌 마무리(면접 대비 문서화)

- [ ] “Pooling 적용 전/후” 비교 스크린샷 1~2장 준비
- [ ] “왜 Pool이 필요한가” 한 문장 정리(GC/Instantiate 비용)
- [ ] “내가 적용한 범위/설계” 한 문장 정리(Prewarm/Reset/Release 규칙)
- [ ] README에 핵심 코드 링크 2개 추가
  - [ ] PoolManager
  - [ ] Arrow(또는 Effect) 적용 코드
