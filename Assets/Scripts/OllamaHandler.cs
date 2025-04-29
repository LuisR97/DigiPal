using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ollama;
using System.Linq;
using System.Text;
using UnityEditor.VersionControl;
public class OllamaHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public string model = "llama3.2latest";
    private Queue<string> chatHistory;
    private bool isStreaming;

    void OnEnable() 
    { 
        Ollama.OnStreamFinished += StreamFinished; 
    }

    void OnDisable() 
    { 
        Ollama.OnStreamFinished -= StreamFinished; 
    }
    void Awake() 
    {
         Ollama.Launch(); 
    }

    async void Start()
    {
        chatHistory = new Queue<string>();
        Ollama.InitChat();
        var models = await Ollama.List();
    }

    void LateUpdate()
    {
        if (!isStreaming)
            return;

    }
    
    public async void OnSubmit(string input)
    {
        if (isStreaming)
            return;

        isStreaming = true;
        await Ollama.ChatStream((string text) => chatHistory.Enqueue(text), model, input);
        Debug.Log(Ollama.publicChat);

        isStreaming = false;
    }

    public void SaveChat()
    {
        Ollama.SaveChatHistory();
    }

    private void StreamFinished() 
    {
         isStreaming = false; 
    }
}
