using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleSwimmingScript : MonoBehaviour
{
    [Header("this is a simple swimming script inspired by gorilla tag, but better. made by axiom.")]
    InputDevice LeftControllerDevice;
    InputDevice RightControllerDevice;
    public string headName = "PlayerHead";
    public AudioSource swimsoundL;
    public AudioSource swimsoundR;
    public Rigidbody player;
    public float swimThreshhold;
    public float swimSpeed;
    [Header("please ignore these!")]
    public float LMag;
    public float RMag;
    public Vector3 LeftControllerVelocity;
    public Vector3 RightControllerVelocity;
    private InputDevice targetDevice;
    private InputDevice targetDevice1;
    private bool canSwim = false; 

    void Start()
    {
        LeftControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void FixedUpdate()
    {
        swimsoundL.volume = LMag / 2f;
        swimsoundR.volume = RMag / 2f;

        if (canSwim == true)
        {
           targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
           targetDevice1.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue1);
           targetDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity);
           targetDevice1.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);
           LMag = LeftControllerVelocity.magnitude;
           RMag = RightControllerVelocity.magnitude;


          if (LMag > swimThreshhold || RMag > swimThreshhold)
          {
             player.AddRelativeForce(-(LeftControllerVelocity + RightControllerVelocity) * swimSpeed);
          }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == (headName))
        {
            canSwim = true;
            player.useGravity = false;
        }
    }

        public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == (headName))
        {
            canSwim = false;
            player.useGravity = true;
            swimsoundL.volume = 0f;
            swimsoundR.volume = 0f;
        }
    }
    

    public void LateUpdate()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        List<InputDevice> devices1 = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices1);

        if (devices1.Count > 0)
        {
            targetDevice1 = devices1[0];
        }
    }
}
