using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderScript : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;
    public UnityEngine.UI.RawImage imageOnCanvas;
    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();
        imageOnCanvas.texture = renderTexture;

        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.SetFloat("Resolution", renderTexture.width);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
