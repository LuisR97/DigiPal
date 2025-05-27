using UnityEngine;
using System.Net.Http;
using System;
using TMPro;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.IO;
using Newtonsoft;
using System.Text;
using UnityEngine.Networking;
using System.Linq;
using WWUtils.Audio;
using Unity.Android.Gradle;

public class TextToSpeechHandler : MonoBehaviour
{
    public Uri kokoroUri = new Uri("http://localhost:8880/v1/audio/speech");
    public TMP_InputField inputField;
    public HttpClient client = new HttpClient();


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public async void GenerateSpeech(string userInput, AudioSource audioSource)
    {
        using(client)
        {
            AudioPayload payload = new AudioPayload
            {
                model = "kokoro",
                input = userInput,
                voice = "af_bella",
                response_format = "wav",
                speed = 1.0f
            };
            string payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            //Debug.Log("JSON: " + payloadJson);
            StringContent content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(kokoroUri, content);
            Debug.Log((int)response.StatusCode);

            byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
            //string path = Application.dataPath + "/Assets";
            //File.WriteAllBytes(path + "output.wav", audioBytes);
            
            WAV wav = new WAV(audioBytes);
            AudioClip audioClip = AudioClip.Create("testSound", wav.SampleCount, 1, wav.Frequency, false, false);
            audioClip.SetData(wav.LeftChannel,0);
            audioSource.clip = audioClip;

            audioSource.Play();

        }
    }

    public static float[] NormalizeArray(float[] floatArray)
    {
        float min = floatArray.Min();
        float max = floatArray.Max();
        float range = max - min;

        float[] newArray = new float[floatArray.Length];

        for (int i = 0; i < floatArray.Length; i++)
        {
            newArray[i] = (floatArray[i] - min)/range;
        }

        
        return newArray;
    }

    public static float[] ConvertToFloat(byte[] byteArray)
    {
        int floatCount = byteArray.Length / 4;
        float[] floatArray = new float[floatCount];

        for (int i = 0; i < floatCount; i++)
        {
            floatArray[i] = BitConverter.ToSingle(byteArray, i * 4);
        }

        return floatArray;
    }

    async Task<AudioClip> LoadWavAsync(string filepath)
    {
        using (UnityWebRequest www = new UnityWebRequest(new System.Uri(filepath).AbsoluteUri))
        {
            DownloadHandlerAudioClip dlHandler = new UnityEngine.Networking.DownloadHandlerAudioClip(www.url, AudioType.WAV);
            www.downloadHandler = dlHandler;
            await www.SendWebRequest();

            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                return DownloadHandlerAudioClip.GetContent(www);
            }
            else
            {
                Debug.LogError($"WWW Error: {www.error}");
                return null;
            }
        }
    }
}


public class AudioPayload
{
    public string model;
    public string input;
    public string voice;
    public string response_format;
    public float speed;
}