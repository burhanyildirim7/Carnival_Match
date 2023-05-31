using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonlarHaptic : MonoBehaviour
{

    public void ButtonPressHapticActive()
    {
        if (PlayerPrefs.GetInt("vibration_enabled")==0)
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        }
        else
        {

        }
    }

}
