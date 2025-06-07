/*public class temp : MonoBehaviour
{
    //public TMP_Text notif;
    //public WhisperManager whisper;
    //public PlayerInput playerInput;
    public InputActionAsset inputActionAsset;
    public InputActionReference actionRef;
    private InputAction recordButton;

    private void OnEnable()
    {
        inputActionAsset.FindActionMap("ControlMap", true).Enable();
        Debug.Log(inputActionAsset.name + " ENABLED");
        actionRef.action.performed += RecordVoice;
    }
    
    private void OnDisable()
    {
        inputActionAsset.FindActionMap("ControlMap").Disable();
    }

    private void Awake()
    {
        //recordButton = InputSystem.actions.FindAction("Record");

        Debug.Log("AWAKE");
    }

    /*
    void FixedUpdate()
    {
        if (recordButton.WasPressedThisFrame())
        {
            RecordVoice();
        }
    } 
    public void RecordVoice(InputAction.CallbackContext obj)
    {
        Debug.Log("RECORDING");
    }
} */


    /*
    private void OnEnable() 
    {
        actionRef.action.performed += RecordVoice;
    }

    private void OnDisable()
    {
        //actionRef.action.performed -= RecordVoice;
        inputActionAsset.FindActionMap("ControlMap").Disable();
    }

    void Start()
    {
        //actionRef.action.performed += RecordVoice;
        //actionRef.action.canceled += StopRecording;
    }

    public void RecordVoice(InputAction.CallbackContext obj)
    {
        //OnButtonPressed();
        Debug.Log("RECORDING");
    }
    public void StopRecording(InputAction.CallbackContext obj)
    {
        //OnRecordStop();
        Debug.Log("NO LONGER RECORDING");
    }

    private void OnButtonPressed()
    {
        if (!microphoneRecord.IsRecording)
        {
            microphoneRecord.StartRecord();
            Debug.Log("RECORDING");
        }
        else
        {
            microphoneRecord.StopRecord();
            Debug.Log("DONE RECORDING");
        }
    }
    
    private async void OnRecordStop(AudioChunk recordedAudio)
    {
        _buffer = "";
        
        var res = await whisper.GetTextAsync(recordedAudio.Data, recordedAudio.Frequency, recordedAudio.Channels);
        if (res == null) 
            return;

        var text = res.Result;

        if (printLanguage)
            text += $"\n\nLanguage: {res.Language}";
        
        Debug.Log("RECORDED SCRIPT " + text);
    }
} */
