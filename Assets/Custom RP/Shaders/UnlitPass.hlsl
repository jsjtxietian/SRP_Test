#ifndef CUSTOM_UNLIT_PASS_INCLUDED
#define CUSTOM_UNLIT_PASS_INCLUDED

#include "../ShaderLibrary/Common.hlsl"

// float4 UnlitPassVertex (float3 positionOS: POSITION) : SV_POSITION {
// 	float3 positionWS = TransformObjectToWorld(positionOS.xyz);
// 	return TransformWorldToHClip(positionWS);
// }

// float4 UnlitPassFragment () : SV_TARGET{
//     return 0.0;
// }

#define UNITY_MATRIX_M unity_ObjectToWorld
#define UNITY_MATRIX_I_M unity_WorldToObject
#define UNITY_MATRIX_V unity_MatrixV
#define UNITY_MATRIX_VP unity_MatrixVP
#define UNITY_MATRIX_P glstate_matrix_projection


#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

#endif
