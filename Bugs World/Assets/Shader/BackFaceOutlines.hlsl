#ifndef BACKFACEOUTLINES_INCLUDED
#define BACKFACEOUTLINES_INCLUDED

//Include helper functions frop URP
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

//Data from the meshes
struct Attributes
{
    float4 position0S :POSITION; //Position in object space
    float3 normalOS :NORMAL; //Normal vector in object space
#ifdef USE_PRECALCULATED_OUTLINE_NORMALS
    float3 smoothNormalOS       :TEXCOORD1; //Calculated "smooth" normals yo extrude along in object space
#endif    
};


//Output from the vertex function and input to the fragment function
struct VertexOutput
{
    float4 positionCS :SV_POSITION; //Position in clip space
};

//Properties
float _Thickness;
float4 _Color;
float _DepthOffset;

VertexOutput Vertex(Attributes input)
{
    VertexOutput output = (VertexOutput)0;

    float3 normalOS = input.normalOS;
    #ifdef USE_PRECALCULATED_OUTLINE_NORMALS
        normalOS = input.smoothNormalOS;
    #else
        normalOS = input.normalOS;
    #endif
    
    //Extrude the object space position along a normal vector
    float3 posOS = input.position0S.xyz + normalOS * _Thickness;
    //Convert this position to world and clip space
    output.positionCS = GetVertexPositionInputs(posOS).positionCS;
    float depthOffset = _DepthOffset;
    //If the depth is reversed on this platform, reverse the offset
    #ifdef UNITY_REVERSED_Z
        depthOffset = -depthOffset;
    #endif
        output.positionCS.z += depthOffset;
    
    return output;
}

float4 Fragment(VertexOutput input) : SV_Target
{
    return _Color;
}
#endif
