using System;
using System.Text;
using System.Threading.Tasks;

namespace Translate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 1. OpenAI API 키 설정
            string[] apiKey = await File.ReadAllLinesAsync("../../../APIKEY", Encoding.UTF8);

            // 2. OpenAITranslator 인스턴스 생성
            var translator = new OpenAITranslator(apiKey[0]);

            // 3. SrtTranslator 인스턴스 생성
            var srtTranslator = new SrtTranslator();

            // 4. SRT 파일 경로 설정
            // - 절대 경로 예시: "C:\\Users\\User\\Documents\\input.srt"
            // - 상대 경로 예시: "./input.srt"
            string fileName = "File Name";
            string Path = "File Path";


            string inputPath = $"{Path}{fileName}.srt";    // 원본 SRT 파일
            string outputPath = $"{Path}kor\\{fileName}kor.srt";  // 번역된 결과를 저장할 파일

            try
            {
                // 5. 번역 실행
                await srtTranslator.TranslateSrtAsync(inputPath, outputPath, translator);
                Console.WriteLine("SRT 번역이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"에러 발생: {ex.Message}");
            }
        }
    }
}
