using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlowPreset : MonoBehaviour
{

    public GameObject thisPreset;
    public GameObject otherPreset;
    // Start is called before the first frame update


    public void ChangeAirFlowPreset() {
        if (!thisPreset.activeSelf) thisPreset.SetActive(true);
        if (otherPreset.activeSelf) otherPreset.SetActive(false);
    }

}
