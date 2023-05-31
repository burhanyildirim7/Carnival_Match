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
        if (PlayerPrefs.GetInt("vibration_enabled") == 0)
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        }
        else
        {

        }
        _panel.SetActive(false);

        PlayerPrefs.SetInt("OnboardingDone", 1);
    }
}
