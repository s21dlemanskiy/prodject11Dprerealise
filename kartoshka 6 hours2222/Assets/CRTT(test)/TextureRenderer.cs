using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRenderer : MonoBehaviour
{
    
    // Take a "screenshot" of a camera's Render Texture.
    public Texture2D RTImg(Camera camera)
    {
        
        //Debug.Log(camera.transform.name + i);
        var t = camera.targetTexture;
        //Debug.Log(t);
        int w = t.width;
        
       //Debug.Log(camera.transform.name + i);
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        // Render the camera's view.
        camera.Render();
        //Debug.Log(camera.transform.name + i);
        //Debug.Log(((camera.targetTexture == null) ? 1 : 0) * 10 + i);
        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(w, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
        
    }

}
