using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Translate
{
    public class OpenAITranslator
    {
        private readonly string apiKey; // OpenAI API 키를 저장하는 변수
        private readonly HttpClient client; // HTTP 요청을 수행할 HttpClient 객체

        // 생성자: API 키를 받아서 HttpClient를 초기화함
        public OpenAITranslator(string apiKey)
        {
            this.apiKey = apiKey;
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        /// <summary>
        /// 텍스트를 지정된 언어로 번역하는 비동기 메서드
        /// </summary>
        /// <param name="text">번역할 원본 텍스트</param>
        /// <param name="targetLanguage">목표 언어 (기본값: 한국어 "ko")</param>
        /// <returns>번역된 텍스트를 반환</returns>
        public async Task<string> TranslateTextAsync(string text, string targetLanguage = "ko")
        {
            // OpenAI API에 보낼 요청 데이터 구성
            var requestBody = new
            {
                model = "gpt-4o", // 사용할 OpenAI 모델
                messages = new[]
                {
                    new { role = "user", content = $"Translate the following text to {targetLanguage}:\n\n{text}" }
                },
                temperature = 0.3 // 번역 결과의 일관성을 유지하기 위해 낮은 temperature 설정
            };

            // 요청 데이터를 JSON 문자열로 직렬화
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // OpenAI API의 Chat Completions 엔드포인트에 POST 요청 전송
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            response.EnsureSuccessStatusCode(); // HTTP 응답이 성공적이지 않으면 예외 발생

            // API 응답을 문자열로 읽어옴
            var responseContent = await response.Content.ReadAsStringAsync();

            // JSON 응답을 동적 객체(dynamic)로 변환
            dynamic result = JsonConvert.DeserializeObject(responseContent);

            // 번역된 텍스트 추출
            string translation = result.choices[0].message.content.ToString().Trim();

            return translation; // 번역 결과 반환
        }
    }
}
