using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlinding : MonoBehaviour {
    Material BlindingMaterial = null;
    public Shader BlindingShader;
    [Range(0.0f, 1f)]
    public float BlendWeight = 0;
    float delay;
    public bool time;
    // Use this for initialization
    void OnEnable()
    {
        BlindingMaterial = new Material(BlindingShader);
        BlindingMaterial.hideFlags = HideFlags.DontSave;
    }
    void OnDisable()
    {
        BlindingMaterial = null;
    }
    void Update()
    {
        if(BlindingMaterial != null)
        {
            BlindingMaterial.SetFloat("_Blend", BlendWeight);
        }
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(BlindingMaterial != null)
        {
            Graphics.Blit(source, destination,BlindingMaterial);
        }
        else Graphics.Blit(source, destination);
    }
    IEnumerator FlashScreen()
    {
        while(BlendWeight < 0.95f)
        {
            BlendWeight = Mathf.Lerp(BlendWeight, 1, 0.2f);
            yield return new WaitForSeconds(delay);
        }
        BlendWeight = 1;
        LevelManager.lm.changeTime(time);
        while (BlendWeight > 0.05f)
        {
            BlendWeight = Mathf.Lerp(BlendWeight, 0, 0.2f);
            yield return new WaitForSeconds(delay);
        }
        BlendWeight = 0;
        
    }
}
