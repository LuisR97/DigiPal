using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Whisper.Utils;

namespace Whisper
{
    public class SpeechToText : MonoBehaviour
    {
        public InputActionReference record;
        public WhisperManager whisper;
        public WhisperWrapper whisperWrapper;
        public MicrophoneRecord microphoneRecord;
        public OllamaHandler ollamaHandler;

        private void OnEnable()
        {
            record.action.performed += StartRecording;
            record.action.canceled += StopRecording;
        }

        private void OnDisable()
        {
            record.action.performed -= StartRecording;
            record.action.canceled -= StopRecording;
        }

        private void Awake()
        {
            microphoneRecord.OnRecordStop += OnRecordStop;

        }

        private void StartRecording(InputAction.CallbackContext context)
        {
            microphoneRecord.StartRecord();
            Debug.Log("Record");
        }

        private void StopRecording(InputAction.CallbackContext context)
        {
            microphoneRecord.StopRecord();
            Debug.Log("STOPPED RECORDING");
        }

        private async void OnRecordStop(AudioChunk recordedAudio)
        {
            var res = await whisper.GetTextAsync(recordedAudio.Data, recordedAudio.Frequency, recordedAudio.Channels);
            if (res == null)
                return;

            var text = res.Result;
            ollamaHandler.OnSubmit(text);
            Debug.Log(text);
        }
        
    }

}
