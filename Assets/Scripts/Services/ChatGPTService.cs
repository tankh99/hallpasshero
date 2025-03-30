using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Text;
using System;

public class ChatGPTService : MonoBehaviour
{
    private const string API_URL = "https://api.openai.com/v1/chat/completions";
    private const string API_KEY = "your-api-key-here"; // Store this securely!
    
    [Serializable]
    private class ChatGPTRequest
    {
        public Message[] messages;
        public string model = "gpt-3.5-turbo";
    }

    [Serializable]
    private class Message
    {
        public string role;
        public string content;
    }

    [Serializable]
    private class ChatGPTResponse
    {
        public Choice[] choices;
    }

    [Serializable]
    private class Choice
    {
        public Message message;
    }

    public async Task<string> GetChatGPTResponse(string userInput)
    {
        var request = new ChatGPTRequest
        {
            messages = new Message[]
            {
                new Message { role = "user", content = userInput }
            }
        };

        string jsonRequest = JsonUtility.ToJson(request);

        using (UnityWebRequest webRequest = new UnityWebRequest(API_URL, "POST"))
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonRequest);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {API_KEY}");

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                ChatGPTResponse response = JsonUtility.FromJson<ChatGPTResponse>(webRequest.downloadHandler.text);
                return response.choices[0].message.content;
            }
            else
            {
                Debug.LogError($"Error: {webRequest.error}");
                return "Sorry, I couldn't process that request.";
            }
        }
    }
} 