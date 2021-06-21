using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalVisualiser : MonoBehaviour
{
    public Transform normalOut;
    public Transform normalIn;
    public PortalRecursionController recursionController;
    public Renderer viewthroughRenderer;
    public Renderer cubeViewthroughRenderer;
    private Material viewthroughMaterial;
    private Material cubeViewthroughMaterial;
    private Camera mainCamera;
    public Texture viewthroughFallbackTexture;
    private Vector4 vectorPlane;
    public static Vector3 TransformPositionBetweenPortals(PortalVisualiser sender, PortalVisualiser target, Vector3 position)
    {
        return
            target.normalIn.TransformPoint(
                sender.normalOut.InverseTransformPoint(position));
    }

    public static Quaternion TransformRotationBetweenPortals(PortalVisualiser sender, PortalVisualiser target, Quaternion rotation)
    {
        return
            target.normalIn.rotation *
            Quaternion.Inverse(sender.normalOut.rotation) *
            rotation;
    }

    private struct VisiblePortalTextures
    {
        public PortalVisualiser VisiblePortal;
        public RenderTexturePool.PoolItem PoolItem;
        public Texture OriginalTexture;
    }

    public void DeepRender(
        Vector3 camPosition,
        Quaternion camRotation,
        out RenderTexturePool.PoolItem tmpPI,
        out Texture originalTexture,
        out int renderNum,
        Camera portalCamera,
        int depth,
        int maxDepth)
    {
        renderNum = 1;
        var targetPortal = recursionController.OtherPortal.gameObject.GetComponent<PortalVisualiser>();
        var relativePos = TransformPositionBetweenPortals(this, targetPortal, camPosition);
        var relativeRot = TransformRotationBetweenPortals(this, targetPortal, camRotation);
        portalCamera.transform.SetPositionAndRotation(relativePos, relativeRot);

        // Convert target portal's plane to camera space (relative to target camera)

        var targetViewThroughPlaneCameraSpace =
            Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix))
            * targetPortal.vectorPlane;

        // Set portal camera projection matrix to clip walls between target portal and target camera
        // Inherits main camera near/far clip plane and FOV settings
        
        var obliqueProjectionMatrix = mainCamera.CalculateObliqueMatrix(targetViewThroughPlaneCameraSpace);
        portalCamera.projectionMatrix = obliqueProjectionMatrix;
        
        var Visibles = new List<VisiblePortalTextures>();
        var visibleArray = targetPortal.gameObject.GetComponent<PortalRecursionController>().visiblePortals;
        if (depth < maxDepth){
            foreach(var visPortalController in visibleArray){
                var visPortal = visPortalController.gameObject.GetComponent<PortalVisualiser>();
                visPortal.DeepRender(relativePos, relativeRot, out var vis_tmpPI, out var vis_originalTexture, out var vis_renders, portalCamera, depth + 1, maxDepth);
                Visibles.Add(new VisiblePortalTextures(){PoolItem = vis_tmpPI, VisiblePortal = visPortal, OriginalTexture = vis_originalTexture});
                renderNum += vis_renders;
            }
        }else
        {
            foreach(var visPortalController in visibleArray){
                var visPortal = visPortalController.gameObject.GetComponent<PortalVisualiser>();
                visPortal.ShowFallback(out var vis_originalTexture);
                Visibles.Add(new VisiblePortalTextures(){VisiblePortal = visPortal, OriginalTexture = vis_originalTexture});
            }
        }

        // A new temporary RenderRexture
        // IMPORTANT: RELEASED BY CALLER
        // ALWAYS RELEASE AFTER CALLING
        tmpPI = RenderTexturePool.Instance.GetTexture();

        portalCamera.targetTexture = tmpPI.Texture;
        portalCamera.transform.SetPositionAndRotation(relativePos, relativeRot);
        portalCamera.projectionMatrix = obliqueProjectionMatrix;

        // Render portal camera to target texture

        portalCamera.Render();

        foreach (var textures in Visibles)
        {
            // Reset to original texture
            // So that it will remain correct if the visible portal is still expecting to be rendered
            // on another camera but has already rendered its texture. Originally the texture may be overriden by other renders.

            textures.VisiblePortal.viewthroughMaterial.mainTexture = textures.OriginalTexture;
            textures.VisiblePortal.cubeViewthroughMaterial.mainTexture = textures.OriginalTexture;
            // Release temp render texture

            if (textures.PoolItem != null)
            {
                RenderTexturePool.Instance.ReleaseTexture(textures.PoolItem);
            }
        }
        originalTexture = viewthroughMaterial.mainTexture;
        viewthroughMaterial.mainTexture = tmpPI.Texture;
        cubeViewthroughMaterial.mainTexture = tmpPI.Texture;

    }

    private void ShowFallback(out Texture originalTexture)
    {
        originalTexture = viewthroughMaterial.mainTexture;
        viewthroughMaterial.mainTexture = viewthroughFallbackTexture;
    }

    private void Start()
    {
        var targetPortal = recursionController.OtherPortal;
        viewthroughMaterial = viewthroughRenderer.material;
        cubeViewthroughMaterial = cubeViewthroughRenderer.material;
     
        mainCamera = Camera.main;
        var plane = new Plane(normalOut.forward, transform.position);
    vectorPlane = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);

    }


    private void OnDestroy()
    {

        // Destroy cloned material 
        Destroy(viewthroughMaterial);

    }
}
