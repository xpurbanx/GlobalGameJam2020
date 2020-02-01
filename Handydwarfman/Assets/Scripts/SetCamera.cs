using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SetCamera : MonoBehaviour
{
    Vector3 pos, rot;

    void Update()
    {
        if (VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset) != null)
        {
            pos = new Vector3(VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset).position.x,
                VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset).position.y - 0.75f,
                VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset).position.z);


            rot = new Vector3(VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset).localEulerAngles.x,
                0, VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset).localEulerAngles.z);
        }

        transform.position = pos;
        transform.localEulerAngles = rot;
    }
}
