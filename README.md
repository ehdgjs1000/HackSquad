# 📱 Unity Android 게임 개발 & Google Play 배포 포트폴리오 (Card Layout)

김동헌의 Unity 기반 Android 게임 개발 및 실전 배포 경험을 카드형 구성으로 정리한 프로젝트 포트폴리오입니다.

---

## 🗂️ 프로젝트 개요

**📛 프로젝트명**: *(직접 입력)*  
**🛠 개발환경**: Unity 2022.x (LTS)  
**📦 빌드 형식**: Android App Bundle (.aab)  
**🚀 배포 대상**: Google Play (Internal Testing)  
**🔐 서명 방식**: Android Keystore (.keystore)  
**📱 테스트 기기**: Galaxy S21, LG Velvet 외

---

## 🔧 개발 설정 카드

### 💾 빌드 설정
- Scripting Backend: **IL2CPP**
- Target Architecture: **ARM64**
- Target API Level: **33** (Android 13)
- Gradle: **7.5**

### 🔑 Keystore 적용
- 자체 서명 키 생성
- Unity 내 키스토어 설정 연결 및 릴리즈 키 적용

### 📂 AAB 업로드 준비
- Build App Bundle 활성화
- Debug/Release 환경 구분

---

## 🧠 문제 해결 카드

### ⚠️ 64비트 미지원 오류
- **문제**: Mono 설정으로 업로드 시 경고 발생
- **해결**: IL2CPP 전환 + ARM64 체크

### ⚠️ Keystore 서명 오류
- **문제**: Keystore 경로 또는 비밀번호 오류
- **해결**: 새 Keystore 생성 및 재설정으로 해결

### ⚠️ AAB 업로드 실패
- **문제**: Gradle 구성 불일치
- **해결**: Build.gradle 수정 및 버전 명시

---

## 🎮 실행 결과 카드

- **앱 이름**: *(직접 입력)*
- **앱 특징**: 간단한 캐릭터 선택 → 전투 → 레벨업 UI
- **배포 상태**: Internal Testing 트랙에서 기기 설치 완료
- **첨부자료**:
  - ✅ 메인 화면 스크린샷
  - ✅ 로그인 or 시작 장면
  - ✅ 전투 흐름 또는 스킬 선택 화면 (최소 3장)

---

## 🧾 회고 카드

- Google Play 기준에 맞춰 AAB 빌드, 서명, 테스트 트랙 업로드를 전부 경험하며 **개발부터 배포까지의 흐름을 체계화**함
- 실무에서의 Android 정책 변경에 적응한 경험
- 추후 Firebase, In-App Purchase, Push Notification 등 기능 확장 예정

---

## 🔗 링크 카드

- [🔗 GitHub Repository](https://github.com/YourProjectLink)
- [🔗 Google Play Internal Test Link](https://play.google.com/) *(선택)*

---

✅ *Unity 개발자가 아닌 기획자, 디자이너, 퍼블리셔도 이해할 수 있도록 구성한 카드형 실무 포트폴리오입니다.*
