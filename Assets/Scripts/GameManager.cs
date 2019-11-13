using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [SerializeField] public UIManager uiManager;
    [SerializeField] public AudioInputCapture audioInputCapture;
    [SerializeField] public AudioVisualizer audioVisualizer;

    private UnityAction<int> selectDeviceAction;

    // Start is called before the first frame update
    void Start()
    {
        audioInputCapture.Initialize();
        string defaultDevice = audioInputCapture.GetSelectedDevice();

        uiManager.SetDropdownOptions(new List<string>(audioInputCapture.GetAvailableDevices()));
        uiManager.SetDropdownSelection(defaultDevice);

        selectDeviceAction += OnChangeSelectedAudioDevice;
        uiManager.SubscribeToDropdownValueChange(selectDeviceAction);

        audioInputCapture.RecordWithDevice(defaultDevice);
        audioVisualizer.Initialize(1024);
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    void OnChangeSelectedAudioDevice(int index) {
        audioInputCapture.RecordWithDevice(uiManager.GetSelectedAudioDevice());
    }
}
