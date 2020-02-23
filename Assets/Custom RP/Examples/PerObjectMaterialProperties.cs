using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour {
	
	static int baseColorId = Shader.PropertyToID("_BaseColor");
	static MaterialPropertyBlock block;

	[SerializeField]
	Color baseColor = Color.white;

	// OnValidate gets invoked in the Unity editor when the component is loaded or changed. 
	void OnValidate () {
		if (block == null) {
			block = new MaterialPropertyBlock();
		}
		block.SetColor(baseColorId, baseColor);
		GetComponent<Renderer>().SetPropertyBlock(block);
	}

	void Awake () {
		OnValidate();
	}
}