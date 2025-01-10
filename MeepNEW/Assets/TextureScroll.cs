using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    [Header("Script made by RG_vr dont need creds")]

    [Header("Scroll Settings")]
    public float scrollSpeed = 0.5f;
    public bool scrollInX = true;
    public bool scrollInY = true;

    [Header("Texture Settings")]
    public Texture texture;
    public float transparency = 1.0f;
    public float smoothness = 0f;
    public float metallicness = 0f;

    [Header("The object that will have this texture")]
    public Renderer obj;
    public Material mat;

    // the object that will have this texture
    private void Start()
    {
        obj = GetComponent<Renderer>();
        mat = obj.material;
        SetTexture();
    }

    // the scroll speed and ofset
    private void Update()
    {
        float offsetX = scrollInX ? Time.time * scrollSpeed : 0f;
        float offsetY = scrollInY ? Time.time * scrollSpeed : 0f;
        mat.mainTextureOffset = new Vector2(offsetX, offsetY);
    }

    // for the texture to actually work
    private void SetTexture()
    {
        if (texture != null)
        {
            mat.mainTexture = texture;
            mat.SetFloat("_Mode", 2);
            mat.SetFloat("_Smoothness", smoothness);
            mat.SetFloat("_Metallic", metallicness);
            Color color = mat.color;
            color.a = transparency;
            mat.color = color;
        }
    }

    // transparency
    public void SetTransparency(float alpha)
    {
        transparency = Mathf.Clamp01(alpha);
        Color color = mat.color;
        color.a = transparency;
        mat.color = color;
    }

    // smooooothnesss ( no one likes it)
    public void SetSmoothness(float value)
    {
        smoothness = Mathf.Clamp01(value);
        mat.SetFloat("_Smoothness", smoothness);
    }

    // metalicccness ( no one likes it either )
    public void SetMetallicness(float value)
    {
        metallicness = Mathf.Clamp01(value);
        mat.SetFloat("_Metallic", metallicness);
    }
}
