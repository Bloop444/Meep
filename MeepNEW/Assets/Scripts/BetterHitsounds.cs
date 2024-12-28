using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BetterHitsounds : MonoBehaviour
{
    [Header("Only Set Up one hand")]
    [Header("But make sure both have script")]
    [Header("Select other hand here")]
    public BetterHitsounds OtherHand;
    public List<HitSoundItem> Hitsounds;
    private AudioSource Player;

    private void Start()
    {
        if (OtherHand != null)
        {
            OtherHand.Hitsounds = Hitsounds;
            OtherHand.Player = OtherHand.GetComponent<AudioSource>();
        }
        Player = GetComponent<AudioSource>();
    }

    [System.Serializable]
    public class HitSoundItem
    {
        [Header("Name (Optional)")]
        public string Name;
        [Header("Sounds")]
        public List<AudioClip> Sounds;
        [Space]
        [Range(0, 100)]
        public int Volume = 100;
        [Header("3D Mode")]
        public bool Stereo;
        [Header("AudioRange")]
        public float MinRange;
        public float MaxRange;
        [Header("Roll Of Mode")]
        public AudioRolloffMode RolloffMode = AudioRolloffMode.Linear;
        [Header("Trigger")]
        public bool UseTag;
        public List<string> Tag;
        public bool UseLayer;
        public List<LayerMask> Layers;
        public bool UseMat;
        public List<Material> Materials;
        [Header("Effects")]
        public List<ParticleSystem> Particles;
        [Header("FootSteps")]
        public List<GameObject> FootStepPrefab;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var lol in Hitsounds)
        {
            if (lol.UseTag)
            {
                foreach (string Tag in lol.Tag)
                {
                    if (other.CompareTag(Tag))
                    {
                        int IDX = Random.Range(0, lol.Sounds.Count);
                        AudioClip TheChoosenOne = lol.Sounds[IDX];
                        Player.clip = TheChoosenOne;
                        Player.volume = lol.Volume;
                        Player.minDistance = lol.MinRange;
                        Player.maxDistance = lol.MaxRange;
                        //Uggly i know but kinda cool Ngl
                        if (lol.Stereo) { Player.spatialBlend = 1; }
                        else { Player.spatialBlend = 0; }
                        //Ugly thingy end so ig I love yall... No Homo
                        Player.rolloffMode = lol.RolloffMode;
                        Player.Play();
                        int RIDX = Random.Range(0, lol.FootStepPrefab.Count);
                        SpawnFootPrint(lol.FootStepPrefab[RIDX], other.gameObject);
                    }
                }
            }
            if (lol.UseLayer)
            {
                foreach (LayerMask Layer in lol.Layers)
                {
                    if (other.gameObject.layer == Layer)
                    {
                        int IDX = Random.Range(0, lol.Sounds.Count);
                        AudioClip TheChoosenOne = lol.Sounds[IDX];
                        Player.clip = TheChoosenOne;
                        Player.volume = lol.Volume;
                        Player.minDistance = lol.MinRange;
                        Player.maxDistance = lol.MaxRange;
                        //Uggly i know but kinda cool Ngl
                        if (lol.Stereo) { Player.spatialBlend = 1; }
                        else { Player.spatialBlend = 0; }
                        //Ugly thingy end so ig I love yall... No Homo
                        Player.rolloffMode = lol.RolloffMode;
                        Player.Play();
                        int RIDX = Random.Range(0, lol.FootStepPrefab.Count);
                        SpawnFootPrint(lol.FootStepPrefab[RIDX], other.gameObject);
                    }
                }
            }
            if (lol.UseMat)
            {
                foreach (Material mat in lol.Materials)
                {
                    if (other.material == mat)
                    {
                        int IDX = Random.Range(0, lol.Sounds.Count);
                        AudioClip TheChoosenOne = lol.Sounds[IDX];
                        Player.clip = TheChoosenOne;
                        Player.volume = lol.Volume;
                        Player.minDistance = lol.MinRange;
                        Player.maxDistance = lol.MaxRange;
                        //Uggly i know but kinda cool Ngl
                        if (lol.Stereo) { Player.spatialBlend = 1; }
                        else { Player.spatialBlend = 0; }
                        //Ugly thingy end so ig I love yall... No Homo
                        Player.rolloffMode = lol.RolloffMode;
                        Player.Play();
                        int RIDX = Random.Range(0, lol.FootStepPrefab.Count);
                        SpawnFootPrint(lol.FootStepPrefab[RIDX], other.gameObject);
                    }
                }
            }
        }
    }
    public void PlayParticle(ParticleSystem particle, GameObject HitObject)
    {
        Vector3 NPos = HitObject.transform.position.normalized;
        Transform TR = HitObject.transform;
        Instantiate(particle, NPos, TR.rotation);
    }

    public void SpawnFootPrint(GameObject FootStep, GameObject HitObject)
    {
        Instantiate(FootStep, gameObject.transform.position, HitObject.transform.rotation); 
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(BetterHitsounds.HitSoundItem))]
public class HitSoundItemDrawer : PropertyDrawer

//Idk if this works stole this from an random script. hehe
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var nameProp = property.FindPropertyRelative("Name");
        if (!string.IsNullOrEmpty(nameProp.stringValue))
        {
            label.text = nameProp.stringValue;
        }
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
#endif

