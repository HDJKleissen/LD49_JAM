using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.UI;

public class BusController : MonoBehaviour
{

    Bus Bus;
    public string BusPath;
    private float BusVolume;
    private float FinalBusVolume;
    private Slider Slider;

    // Start is called before the first frame update
    void Start()
    {
        Bus = RuntimeManager.GetBus("bus:/" + BusPath);
        Bus.getVolume(out BusVolume, out FinalBusVolume);

        Slider = GetComponent<Slider>();
        Slider.value = BusVolume;
    }

    public void VolumeLevel (float SliderValue)
    {
        Bus.setVolume(SliderValue);
    }
}
