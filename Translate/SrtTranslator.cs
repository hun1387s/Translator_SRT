using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Translate
{
    public class SrtTranslator
    {
        /// <summary>
        /// SRT 파일을 번역하는 비동기 메서드
        /// </summary>
        /// <param name="inputPath">입력 SRT 파일 경로</param>
        /// <param name="outputPath">출력 SRT 파일 경로</param>
        /// <param name="translator">OpenAITranslator 인스턴스</param>
        public async Task TranslateSrtAsync(string inputPath, string outputPath, OpenAITranslator translator)
        {
            // SRT 파일을 UTF-8로 읽어옴
            var lines = await File.ReadAllLinesAsync(inputPath, Encoding.UTF8);
            var sb = new StringBuilder();  // 번역된 내용을 저장할 StringBuilder
            var textBuffer = new StringBuilder(); // 여러 줄에 걸친 자막을 누적 저장

            foreach (var line in lines)
            {
                // 자막 번호 (예: 1, 2, 3...) 또는 타임코드 (예: 00:01:02,500 --> 00:01:07,000)는 그대로 복사
                if (Regex.IsMatch(line, @"^\d+$") || line.Contains("-->"))
                {
                    if (textBuffer.Length > 0)
                    {
                        // 이전 자막 블록의 번역 수행
                        string originalText = textBuffer.ToString().Trim();
                        string translatedText = await translator.TranslateTextAsync(originalText, "ko");
                        PrintLine(originalText, translatedText);

                        sb.AppendLine(translatedText);
                        textBuffer.Clear(); // 버퍼 초기화
                    }
                    sb.AppendLine(line); // 번호 또는 타임코드는 번역 없이 그대로 저장
                }
                else if (string.IsNullOrWhiteSpace(line)) // 빈 줄 (자막 블록 구분)
                {
                    if (textBuffer.Length > 0)
                    {
                        // 누적된 텍스트를 번역
                        string originalText = textBuffer.ToString().Trim();
                        string translatedText = await translator.TranslateTextAsync(originalText, "ko");
                        PrintLine(originalText, translatedText);

                        sb.AppendLine(translatedText);
                        textBuffer.Clear();
                    }
                    sb.AppendLine(line); // 빈 줄 그대로 저장
                }
                else
                {
                    // 자막 텍스트를 누적 (자막이 여러 줄일 수도 있음)
                    textBuffer.AppendLine(line);
                }
            }

            // 마지막 자막 블록 처리
            if (textBuffer.Length > 0)
            {
                string originalText = textBuffer.ToString().Trim();
                string translatedText = await translator.TranslateTextAsync(originalText, "ko");
                PrintLine(originalText, translatedText);
                sb.AppendLine(translatedText);
            }

            // 번역된 내용을 새 파일에 저장
            await File.WriteAllTextAsync(outputPath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 원문과 번역문을 출력하는 메서드
        /// </summary>
        /// <param name="eng">영어 원문</param>
        /// <param name="kor">번역된 한국어 문장</param>
        void PrintLine(string eng, string kor)
        {
            Console.WriteLine(eng);
            Console.WriteLine(kor);
        }
    }
}
