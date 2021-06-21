using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTexturePool : MonoBehaviour
{
    public class PoolItem
    {
        public RenderTexture Texture;
        public bool Used;
    }
    public static RenderTexturePool Instance;
    
    public int maxSize = 100;
    private List<PoolItem> pool = new List<PoolItem>();

    public PoolItem GetTexture(){
        foreach(PoolItem poolItem in pool){
            if(!poolItem.Used){
                poolItem.Used = true;
                return poolItem;
            }
        }
        // more RT more skill - make expand and defense it
        if (pool.Count >= maxSize)
        {
            Debug.LogError("RenderTexture pool is full!");
            //throw new OverflowException();
        }
        var newPoolItem = CreateTexture();
        pool.Add(newPoolItem);
        Debug.Log($"New RenderTexture created, pool is now {pool.Count}/{maxSize} items big.");
        newPoolItem.Used = true;
        return newPoolItem;
    }
    public PoolItem CreateTexture()
    {
            var newTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.DefaultHDR);
        newTexture.Create();
        
        return new PoolItem
        {
            Texture = newTexture,
            Used = false
        };
    }
    public void ReleaseTexture(PoolItem item)
    {
        // When releasing a texture, simply mark it as unused.
        item.Used = false;
    }
    public void ReleaseAllTextures()
    {
        foreach (var poolItem in pool)
        {
            ReleaseTexture(poolItem);
        }
    }
    private void DestroyTexture(PoolItem item)
    {
        // First, release on the GPU...
        
        item.Texture.Release();
        
        // Then Destroy() to remove it from Unity completely.
        
        Destroy(item.Texture);
    }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void OnDestroy()
    {
        foreach (var poolItem in pool)
    {
        DestroyTexture(poolItem);
    }
    }
}
