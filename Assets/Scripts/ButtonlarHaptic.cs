using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonlarHaptic : MonoBehaviour
{

    public void ButtonPressHapticActive()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
    }

}
