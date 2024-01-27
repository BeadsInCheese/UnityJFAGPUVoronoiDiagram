using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeployShader : MonoBehaviour
{
    public ComputeShader computeShader;
    public int iterations=7;
    public float step=1;
    public RenderTexture rt;
   public  RenderTexture input;

    // Start is called before the first frame update
    private void OnEnable()
    {

    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        rt = new RenderTexture(128, 128, 1);
        input = new RenderTexture(128, 128, 1);
        rt.useMipMap = false;
        input.useMipMap = false;
        // Create a RenderTexture object with a resolution of 128x128 pixels.
        rt.enableRandomWrite = true;
        input.enableRandomWrite = true;
        // Set the RenderTexture as the active render target.


        computeShader.SetTexture(0, "Result", rt);
        computeShader.SetTexture(0, "Input", input);
        computeShader.SetFloat("_Step", step);
        computeShader.SetInt("_MaxIter", iterations+1);
        for (int i = 0; i < iterations+1; i++)
        {
            RenderTexture.active = rt;
            computeShader.Dispatch(0, 32, 32, 1);
            computeShader.SetInt("_Iter", i);

            // Swap the input and output textures.
            RenderTexture temp = rt;
            rt = input;
            input = temp;
            // Update the shader with the new textures.
            computeShader.SetTexture(0, "Input", rt);
            computeShader.SetTexture(0, "Result", input);
        }
        Graphics.Blit(rt,destination);
    }
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
