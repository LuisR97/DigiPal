using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.AI;
using UnityEditor;
using System.IO;
public class OllamaHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public string model = "";
    private List<ChatMessage> chatHistory;
    IChatClient chatClient;
    TextToSpeechHandler tts;
    public AudioSource audioSource;

    void Start()
    {
        chatClient = new OllamaChatClient(new Uri("http://localhost:11434/"), model);
        chatHistory = new();
        chatHistory.Add(new ChatMessage(ChatRole.System, "SYSTEM: Respond to all user messages as if you were a the AI, JARVIS, from the Iron Man films."));
    }

    public async void OnSubmit(string input)
    {
        Debug.Log("User: " + input);
        chatHistory.Add(new ChatMessage(ChatRole.User, input));
        string response = "";
        await foreach (var item in chatClient.GetStreamingResponseAsync(chatHistory))
        {
            Debug.Log("Before response append item.text");
            response += item.Text;
            Debug.Log("After response append item.text");
        }
        Debug.Log("After foreach loop");
        chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
        Debug.Log("AI: " + response);
        tts = new TextToSpeechHandler();
        tts.GenerateSpeech(response, audioSource);

    }

    public void SaveChatHistory()
    {
        List<Entry> chatLog = new List<Entry>();
        foreach (var message in chatHistory)
        {
            chatLog.Add(new Entry(message.Role, message.Text));
        }
        string jsonOutput = JsonConvert.SerializeObject(chatLog);
        File.WriteAllText(Application.dataPath + "/Saves/saves.txt", jsonOutput);
    }

    public void LoadChatChistory()
    {
        string contents = File.ReadAllText(Application.dataPath + "/Saves/saves.txt");
        List<Entry> chatLog  = JsonConvert.DeserializeObject<List<Entry>>(contents);
        Debug.Log("First entry: " + chatLog[1].message);

        foreach (var Entry in chatLog)
        {
            
        }
    }

    public class Entry
    {
        public ChatRole role;
        public string message;

        public Entry(ChatRole writer, string input)
        {
            role = writer;
            message = input;
        }
    }
}