using System;
using System.Collections;
using UnityEngine;

public class AudioRecorder : MonoBehaviour {
    public bool isRecording = false;
    AudioClip audioClip = null;
    float start;
    float finish;
    public float[] tmp = new float[512];
    [SerializeField]
    AudioSource aSource;


    public bool IsClip {
        get {
            return audioClip != null;
        }
    }

    public void RecordingToggle() {
        if (isRecording) {

        } else {
            Debug.Log("recording");
            foreach (string device in Microphone.devices) {
                Debug.Log(device);
            }
        }
    }

    public void StartRecording() {
        int c = 0;
        foreach (string mic in Microphone.devices) {
            if (!mic.Contains("Xbox")) {
                start = Time.time;
                isRecording = true;
                audioClip = Microphone.Start(Microphone.devices[c], false, 600, 44100);
                aSource.clip = audioClip;
                break;
            }
            c++;
        }
    }

    public void StopRecording() {
        finish = Time.time;
        Microphone.End(null);
        isRecording = false;
        audioClip.name = $"Log-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";
        
        aSource.Play();
    }

    public IEnumerator Thing() {
        yield return null;
    }

    void Update() {
        //if(isRecording) {
        //    aSource.GetSpectrumData(tmp, 0, FFTWindow.BlackmanHarris);
        //    for (int i = 1; i < tmp.Length - 1; i++) {
        //        Debug.DrawLine(new Vector3(i - 1, tmp[i] + 10, 0), new Vector3(i, tmp[i + 1] + 10, 0), Color.red);
        //        Debug.DrawLine(new Vector3(i - 1, Mathf.Log(tmp[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(tmp[i]) + 10, 2), Color.cyan);
        //        Debug.DrawLine(new Vector3(Mathf.Log(i - 1), tmp[i - 1] - 10, 1), new Vector3(Mathf.Log(i), tmp[i] - 10, 1), Color.green);
        //        Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(tmp[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(tmp[i]), 3), Color.blue);
        //    }
        //}
    }
}
