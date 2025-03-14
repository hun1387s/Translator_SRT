# SRT 자동 번역기

이 프로젝트는 OpenAI API를 활용하여 **SRT(SubRip Subtitle) 파일을 자동으로 번역하는 도구**입니다. C#을 사용하여 개발되었으며, 영어 자막을 한국어로 변환하는 기능을 제공합니다.

## 📌 주요 기능

- **SRT 파일 번역**: 영어 자막을 한국어로 변환
- **OpenAI API 연동**: GPT-4o 모델을 사용하여 번역 수행
- **파일 입출력 지원**: SRT 형식을 유지한 상태로 번역된 결과 저장
- **에러 처리**: API 요청 실패 시 예외 처리 및 로깅 지원

## 🛠️ 사용 방법

### 1️⃣ 프로젝트 설정

1. **OpenAI API 키 준비**

   - OpenAI API를 사용하려면 [OpenAI 계정](https://openai.com/)에서 API 키를 발급받아야 합니다.
   - 프로젝트 폴더 내 `APIKEY` 파일을 생성하고 API 키를 저장합니다.

2. **환경 구성**

   ```
   git clone https://github.com/your-repo/srt-translator.git
   cd srt-translator
   ```

### 2️⃣ 실행 방법

**프로그램 실행**

   - Visual Studio 또는 명령 프롬프트에서 실행합니다.
   - `Program.cs`에서 `inputPath` 및 `outputPath`를 적절히 설정합니다.

   ```
   string fileName = "20";
   string path = "D:\\Tutorial\\ShaderDev\\";
   
   string inputPath = $"{path}{fileName}.srt";
   string outputPath = $"{path}kor\\{fileName}kor.srt";
   ```


## 📂 프로젝트 구조

```
SRT-Translator/
│── Translate/
│   ├── OpenAITranslator.cs  # OpenAI API를 이용한 번역 기능
│   ├── SrtTranslator.cs     # SRT 파일 번역 처리
│   ├── Program.cs           # 실행 파일 (메인 엔트리 포인트)
|   │── APIKEY               # OpenAI API 키 저장 파일
│── README.md               # 프로젝트 설명 파일
│── input.srt               # 번역할 자막 파일 (예제)
│── output.srt              # 번역된 자막 파일 (결과물)
```

## ⚙️ 기술 스택

- **언어**: C#
- **프레임워크**: .NET 8.0
- **라이브러리**:
  - `Newtonsoft.Json` - JSON 데이터 처리
  - `System.Net.Http` - API 통신

## 🚀 개선 및 확장 가능성

- **다국어 지원**: 현재 영어 → 한국어 번역만 가능하지만, 다른 언어도 추가 지원 가능
- **GUI 개발**: 콘솔 앱이 아닌 Windows 애플리케이션 형태로 확장 가능


## 📄 라이선스

MIT License를 따릅니다.

### 👨‍💻 개발자 정보

- **이름**: Lee Sanghun
- **이메일**: hun1387s@gmail.com
- **GitHub**: [hun1387s (HUNinamtion)](https://github.com/hun1387s)
- **Blog**: [[AI) GPT API를 활용한 자막 번역 | HUNimation Lab](https://hun1387s.github.io/ai/0051/)](https://github.com/hun1387s)



