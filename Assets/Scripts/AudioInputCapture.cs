using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInputCapture : MonoBehaviour
{
    const int audioSampleRate = 44100;

    [SerializeField] public int defaultDeviceIndex = 0;

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip audioClip;

    [SerializeField] private string _selectedDevice = null;
    [SerializeField] private string[] _availableDevices = new string[0];
    [SerializeField] private bool _isRecording = false;
    [SerializeField] private bool _isInitialized = false;

    public void Initialize() {
        if (!audioSource) throw new UnityException("Please set an AudioSource for audio capture");
        _availableDevices = Microphone.devices;

        if (defaultDeviceIndex >= _availableDevices.Length || defaultDeviceIndex < 0) {
            Debug.LogWarning("Default device index of " + defaultDeviceIndex + " is out of range");
            defaultDeviceIndex = 0;
        }

        _selectedDevice = _availableDevices[defaultDeviceIndex];

        audioSource.Stop();
        audioSource.loop = true;

        _isRecording = false;
        _isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stop() {
        if (!_isRecording) {
            Debug.LogError("Cannot call Stop() before recording");
            return;
        }

        Microphone.End(_selectedDevice);

        _isRecording = false;
    }

    public void RecordWithDevice(string device) {
        if (_isRecording) Stop();

        audioClip = Microphone.Start(device, true, 10, audioSampleRate);
        audioSource.clip = audioClip;
        audioSource.loop = true;

        _selectedDevice = device;

        Debug.Log("Recording Status: " + Microphone.IsRecording(_selectedDevice).ToString());
        if (Microphone.IsRecording(_selectedDevice)) {
            while (!(Microphone.GetPosition(_selectedDevice) > 0)) {
                // Wait until recording has started
            }

            Debug.Log("Started recording with " + _selectedDevice);
            audioSource.Play();
            _isRecording = true;
        } else {
            Debug.LogWarning("Failed to record with " + _selectedDevice);
        }
    }

    public string GetSelectedDevice() {
        if (!_isInitialized) throw new UnityException("Please initialize before working with audio devices.");
        return _selectedDevice;
    }

    public string[] GetAvailableDevices() {
        if (!_isInitialized) throw new UnityException("Please initialize before working with audio devices.");
        return _availableDevices;
    }
}
