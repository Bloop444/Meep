using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StartEffect : MonoBehaviour
{
    public Volume volume;

    public float Max;
    public float fadeTime = 1;

    private ColorAdjustments ca;

    private void Start(){
        if (volume.profile.TryGet(out ca)){
            ca.postExposure.value = -10;
        }
    }
    private void Update(){
        if (ca && ca.postExposure.value < Max){
            ca.postExposure.value += fadeTime * Time.deltaTime;

            if (ca.postExposure.value > Max){
                ca.postExposure.value = Max;
            }
        }
    }
}
