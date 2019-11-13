using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown dropdown;

    public void Start() {
        if (!dropdown) dropdown = gameObject.GetComponentInChildren<TMP_Dropdown>();
    }

    public void SubscribeToDropdownValueChange(UnityAction<int> call) {
        dropdown.onValueChanged.AddListener(call);
    }

    public void UnusbscribeFromDropdownValueChange(UnityAction<int> call) {
        dropdown.onValueChanged.RemoveListener(call);
    }

    public string GetSelectedAudioDevice() {
        return dropdown.options[dropdown.value].text;
    }

    public void SetDropdownOptions(List<string> options) {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        dropdown.RefreshShownValue();
    }

    public void SetDropdownSelection(string option) {
        for(int i = 0; i < dropdown.options.Count; i++) {
            if (dropdown.options[i].text == option) {
                dropdown.SetValueWithoutNotify(i);
                return;
            }
        }
        Debug.LogWarning("Unable to set dropdown selection to " + option);
    }

}
