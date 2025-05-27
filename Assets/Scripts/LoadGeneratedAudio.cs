using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class LoadGeneratedAudio : MonoBehaviour
{   
    public AudioSource audioSource;
    public AudioClip audioClip;
    IEnumerator GetAudioClip()
    {
        string path = "C:/Users/rayga/Desktop/DigiPal/DigiPal/Assets/output.wav";
        if(File.Exists(path))
        {
            Debug.Log("IT EXISTS");
        }
        else
        {
            Debug.Log("NULL FILE");
        }
            
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip($"file://{path}", AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"UnityWebRequest error: {www.error}");
                yield break; // Exit the coroutine if there's an error
            }

            audioClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAudioClip());
    }
}