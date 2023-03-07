using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTap : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseTapButton()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        _panel.SetActive(false);

        PlayerPrefs.SetInt("OnboardingDone", 1);
    }
}
