using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.Rendering;

public class SpeechRec : MonoBehaviour
{
    [SerializeField] private string language = "es-ES";
    [Serializable]    
    
    public struct VoiceCommand
    {
        public string KeyWord;
        public UnityEvent VoiceEvent;
    }
    
    public VoiceCommand[] voiceCommands;
    private Dictionary<string, UnityEvent> commands = new Dictionary<string, UnityEvent>();


    private void Awake()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
        foreach (var command in voiceCommands)
        {
            commands.Add(command.KeyWord.ToLower(), command.VoiceEvent);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpeechToText.Instance.Setting(language);
        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
#if UNITY_ANDROID
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif
        StartListening();
    }
    
    //speech to text
    private void StartListening()
    {
        //Debug.Log("--------------_START_------------------------");
        SpeechToText.Instance.StartRecording();
    }
    private void StopListening()
    {
        SpeechToText.Instance.StopRecording();
    }
    private void OnFinalSpeechResult(string result)
    {
        //asignar al texto result
       /* uIText.text = result;
        if(result != null)
        {
            var response = commands[result.ToLower()];
            if (response != null)
            {
                response?.Invoke;
            }
        }*/
    }
    private void OnPartialSpeechResult(string result)
    {
        //uIText.text = result;
        Debug.Log($"Voice rec = {result}");
    }
    public void Update()
    {
        //OnPartialSpeechResult("AAAAAAAAAAAAAAAAAAA");
    }
}
