using UnityEngine;
using UnityEngine.Experimental.VFX;

public class AudioVisualizer : MonoBehaviour
{
    [SerializeField] public bool transformFrequency = true;

    [SerializeField] public Transform cube;

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AnimationCurve frequencyCurve = new AnimationCurve();

    [SerializeField] public bool sendToEffect = true;
    [SerializeField] public VisualEffect visualEffect;
    [SerializeField] public string visualEffectFrequencyInput = "Frequency";

    [SerializeField] private int _count = 0;
    [SerializeField] private bool _isInitialized = false;

    [SerializeField] public AnimationCurve modCurve = new AnimationCurve();
    [SerializeField] public float modA = 1f;
    [SerializeField] public float modB = 1f;

    [SerializeField] VowelSaver vs;

    private Keyframe[] _dataFrames = new Keyframe[0];
    private float[] _data = new float[0];
    int[] bins = new int[7];
    float[] intensities = new float[7];

    public void Initialize(int samples = 128) {
        _count = samples;
        _data = new float[samples];
        _dataFrames = new Keyframe[samples];
        frequencyCurve = new AnimationCurve();
        SetFrames(0);

        //if (sendToEffect && visualEffect == null) {
        //    sendToEffect = false;
        //    Debug.LogError("Visual Effect has not been set, disabling");
        //}

        _isInitialized = true;
    }

    public void Update() {
        if (!_isInitialized) return;
        audioSource.GetSpectrumData(_data, 0, FFTWindow.Rectangular);
        UpdateFrames();
        float hz = maxHz(_data);
        cube.localScale = new Vector3(1.0f, hz, 1.0f);

        float targetFrequency = 200;
        float hertzPerBin = (float)AudioSettings.outputSampleRate / 2f / 256;
        float targetIndex = targetFrequency / hertzPerBin;
        
        if ((int)targetIndex >0) {
            //Debug.LogError(hertzPerBin);
        }

    }


    public void SetFrames(float value = 0) {
        float floatCount = _count;
        for (int i = 0; i < _count; i++) {
            _data[i] = value;
            float time = i / floatCount;
            _dataFrames[i].value = value;
            _dataFrames[i].time = time;
        }
        frequencyCurve.keys = _dataFrames;
    }

    public void UpdateFrames() {
        float floatCount = _count;
        for (int i = 0; i < _count; i++) {
            float time = i / floatCount;
            _dataFrames[i].value = transformFrame(_data[i], time);
            _dataFrames[i].time = time;
        }
        frequencyCurve.keys = _dataFrames;
        //if (sendToEffect) {
        //    visualEffect.SetAnimationCurve(visualEffectFrequencyInput, frequencyCurve);
        //}
    }

    public float transformFrame(float value, float t) {
        if (!transformFrequency) return value;
        return value + (value * modA * modCurve.Evaluate(t));
    }

    public float maxHz(float[] data) {
        float max = 0;
        int index = 0;
        string dstring = "";
        for (int i = 0; i < _data.Length; i++) {
            if (_data[i] > max) {
                max = _data[i];
                index = i;
            }

            //dstring += " " + i + ": " + _data[i];
        }
        for (int j = 0; j < 7; j++) {
            bins[j] = (_data.Length - 7) + j;
            intensities[j] = _data[(_data.Length - 7) + j] * 10;
        }
        //Debug.Log(dstring);
        Debug.Log("Index: " + index + "Intensity: " + max * 10);
        return max * 10;
    }

    public Vector2 maxHz() {
        float max = 0;
        int index = 0;
        string dstring = "";
        for (int i = _data.Length - 7; i < _data.Length; i++) {
            //if (_data[i] > _data[i - 1]) {
            //    max = _data[i];
            //    index = i;
            //}
            dstring += i + ": " + _data[i];
        }
        Debug.Log(dstring);
        //Debug.LogError("Index: " + index + "Intensity: " + max * 5000000);
        return new Vector2(max * 5000000, index);
    }

    public int[] GetBins() {
        return bins;
    }

    public float[] GetIntensities() {
        return intensities;
    }

    public float[] GetData() {
        return _data;
    }
}
