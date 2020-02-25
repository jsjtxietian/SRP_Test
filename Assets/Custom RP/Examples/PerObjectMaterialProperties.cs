using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{

    static int baseColorId = Shader.PropertyToID("_BaseColor"),
		//Cutoff Per Object
		cutoffId = Shader.PropertyToID("_Cutoff"), 
		metallicId = Shader.PropertyToID("_Metallic"),
        smoothnessId = Shader.PropertyToID("_Smoothness");


    static MaterialPropertyBlock block;

    [SerializeField]
    Color baseColor = Color.white;

    [SerializeField, Range(0f, 1f)]
    float cutoff = 0.5f,metallic = 0f, smoothness = 0.5f;

    // OnValidate gets invoked in the Unity editor when the component is loaded or changed. 
    void OnValidate()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
        }
		block.SetFloat(metallicId, metallic);
		block.SetFloat(smoothnessId, smoothness);
        block.SetColor(baseColorId, baseColor);
        block.SetFloat(cutoffId, cutoff);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }

    void Awake()
    {
        OnValidate();
    }
}