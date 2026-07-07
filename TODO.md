# HackSquad TODO / DONE

---

## ✅ DONE

### 코어 시스템
- [x] Hero 추상 클래스 (공격, 탐지, 재장전, 스킬 슬롯)
- [x] HeroStatsSO / HeroDataSO ScriptableObject 구조
- [x] Physics.OverlapSphereNonAlloc 기반 몬스터 탐지 최적화
- [x] IAttackBehavior 인터페이스 (AutoAttackBehavior, BazookaAttackBehavior)
- [x] SquadManager 포메이션 시스템 (Transform[] 4슬롯)
- [x] Monster 피격 색상 변화 + 사망 처리

### 히어로
- [x] **람붜 (Rambuwe)** - 미니건, AutoAttackBehavior
  - [x] RambuweAttackSpeedSkill (최종: 최대탄약 +50)
  - [x] RambuweAccuracySkill (최종: 후방 지원 50% 데미지)
  - [x] RambuweOverheatSkill (최종: 관통 +1)
- [x] **그린하사 (GreenHasa)** - 바주카, BazookaAttackBehavior
  - [x] GreenHasaExplosionSkill (최종: 원산폭격)
  - [x] GreenHasaClusterSkill (최종: 융단폭격)
  - [x] GreenHasaNapalmSkill (최종: 플레임타워)
- [x] **길리슈트 (Ghillie)** - 스나이퍼, AutoAttackBehavior 재사용
  - [x] GhilliePierceSkill (최종: 무한관통)
  - [x] GhillieHeadshotSkill (최종: 더블 공격)
  - [x] GhillieWeakpointSkill (최종: 지원사격)

### 투사체 & 이펙트
- [x] Bullet.cs - 직선, 관통, hitVFX 지원
- [x] Rocket.cs - 호밍, AoE, 클러스터, 네이팜, _exploded 가드
- [x] FireZone.cs - 절차적 생성, VFX 지원
- [x] FlameTower.cs - 방향성 화염 (전방 60도 콘), 몬스터 추적 회전
- [x] Hero.hitVFX 필드 - 히어로 단위 VFX 관리

### 그린하사 최종기
- [x] 원산폭격 - 경고 원 2초 → 폭발 VFX + 대형 AoE
- [x] 융단폭격 - LineRenderer 1자 라인 1.5초 → 순차 폭발 (8발 0.15초 간격)
- [x] 플레임타워 - 방향성 화염 타워 8초 지속, 0.1초 틱 데미지
- [x] 네이팜 VFX 체인 (GreenHasa → Rocket → FireZone)

### 스킬 업그레이드 UI
- [x] SkillUpgradeManager - 1번키 패널 오픈, 후보 필터링
- [x] SkillUpgradeUI - 버튼별 스킬 정보 표시
- [x] 최종형태 스킬명/설명 분리 (finalDescription)
- [x] 최종 스킬 업그레이드 풀 제외 처리

### 데미지 텍스트
- [x] DamageText.cs - 상승 + 페이드 애니메이션, 빌보드
- [x] DamageTextManager.cs - 싱글톤, 전역 호출
- [x] Monster.TakeDamage() 연동
- [x] DamageText 프리팹 생성 (Assets/Prefabs/UI/DamageText.prefab)
- [x] 카메라 기울기 무관 수직 빌보드 (LookRotation + Vector3.up)

---

## 📋 TODO

### 히어로 추가 제작 (Hero.md 순서)
- [ ] 아이스맨
- [ ] 복면
- [ ] 사무라이98
- [ ] 로보틱
- [ ] 스타이
- [ ] 중세기술자
- [ ] 카우뽀이
- [ ] 베르
- [ ] 킴승기
- [ ] P.에로
- [ ] 꼬꼬닭
- [ ] 에일리언
- [ ] 길리언
- [ ] 연구원C

### 전투 시스템
- [x] 크리티컬 데미지 텍스트 연동 (CalcDamage에서 isCrit 반환)
- [ ] 몬스터 HP바 UI
- [ ] 몬스터 스포너 / 웨이브 시스템
- [ ] 몬스터 다양화 (스탯 다른 종류)

### 에디터 작업 (미완)
- [ ] FlameTower 프리팹 제작 (FirePos + FlameVFX 자식 구조)
- [ ] DamageTextManager 씬 배치 + prefab 연결
- [ ] 각 히어로 VFX 에셋 제작 및 Inspector 연결
  - [ ] 원산폭격 경고 원 VFX
  - [ ] 원산폭격 폭발 VFX
  - [ ] 융단폭격 폭발 VFX
  - [ ] 네이팜 불길 VFX

### 최적화 / 기타
- [ ] 오브젝트 풀링 (Bullet, Rocket, DamageText)
- [ ] 씬 저장 / 빌드 세팅
