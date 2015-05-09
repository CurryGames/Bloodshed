using System;
using UnityEngine;


public class Pixelated : MonoBehaviour
{
    public Material material;
    public Texture textureRamp;
    public float pixelCountU;
    public float pixelCountV;

    void Start()
    {
        material = new Material(Shader.Find("Seventy Sevian/Pixelated"));
    }

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        material.SetTexture("_RampTex", textureRamp);
        material.SetFloat("_PixelCountU", pixelCountU);
        material.SetFloat("_PixelCountV", pixelCountV);
        Graphics.Blit(source, destination, material);

    }
}

