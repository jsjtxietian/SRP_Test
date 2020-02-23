using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour {
	
	static int baseColorId = Shader.PropertyToID("_BaseColor");
	
	//Cutoff Per Object
	static int cutoffId = Shader.PropertyToID("_Cutoff");
	static MaterialPropertyBlock block;

	[SerializeField]
	Color baseColor = Color.white;

	[SerializeField, Range(0f, 1f)]
	float cutoff = 0.5f;

	// OnValidate gets invoked in the Unity editor when the component is loaded or changed. 
	void OnValidate () {
		if (block == null) {
			block = new MaterialPropertyBlock();
		}
		block.SetColor(baseColorId, baseColor);
		block.SetFloat(cutoffId, cutoff);
		GetComponent<Renderer>().SetPropertyBlock(block);
	}

	void Awake () {
		OnValidate();
	}
}