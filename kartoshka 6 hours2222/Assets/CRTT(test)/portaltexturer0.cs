using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaltexturer0 : MonoBehaviour
{
    public TextureRenderer texturer;
    public Material mat;
    public Camera cam;
    private Texture2D img;
    public RenderTexture rend;
    void LateUpdate()
    {
        img = texturer.RTImg(cam);
        mat.SetTexture("Texture2D_3FA50A66", img);
    }
}
