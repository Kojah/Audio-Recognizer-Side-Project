using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VowelSaver : MonoBehaviour
{
    [SerializeField] Vowel[] vowels;
    [SerializeField] AudioVisualizer visualizer;
    int i = 0;
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Save(i);
            if (i > 4) {
                i = 0;
            } else { i++; }
        }
    }

    void Save(int i) {
        string s = "";
        int[] bins = GetBins();
        float[] intensities = GetIntensities();
        for(int k = 0; k < bins.Length; k++) {
            vowels[i].intensity[k] = intensities[k];
            vowels[i].bin[k] = bins[k];
        }
        for(int k = 0; k < visualizer.GetData().Length; k++) {
            s += "" + visualizer.GetData()[k] + "\n";
        }

        File.AppendAllText(Application.dataPath + "/data.txt", s);
        Debug.LogError(Application.dataPath);
    }

    int[] GetBins() {
        return visualizer.GetBins();
    }

    float[] GetIntensities() {
        return visualizer.GetIntensities();
    }
}
