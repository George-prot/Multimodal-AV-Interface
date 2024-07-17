using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcPreset : MonoBehaviour
{
    public GameObject preset;
    //public GameObject firstPreset;
    public List<GameObject> presetList = new List<GameObject>();
    public bool isPresetActive = false;
    public GameObject onButton;
    public GameObject offButton;

    public Color afterFocus = new Color32(255, 255, 255, 200);


    // Start is called before the first frame update
    public void ChangeAcPreset() {
        preset.SetActive(true);
        foreach (GameObject pr in presetList) {
            if (pr != preset) pr.SetActive(false);
        }
        if (onButton.activeSelf) onButton.SetActive(false);
        if (!offButton.activeSelf) offButton.SetActive(true);
    }

    public void OpenAc() {
        bool isActive = false;
        foreach (GameObject pr in presetList)
        {
            if (pr.activeSelf) isActive = true;
        }

        if (isActive) {
            return;
        }
        else
        {
            preset.SetActive(true);
        }

        onButton.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
        if (onButton.activeSelf) onButton.SetActive(false);
        if (!offButton.activeSelf) offButton.SetActive(true);

    }

    public void CloseAc()
    {
        offButton.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
        if (offButton.activeSelf) offButton.SetActive(false);
        if (!onButton.activeSelf) onButton.SetActive(true);
        foreach (GameObject pr in presetList)
        {
            pr.SetActive(false);
        }
    }

}
