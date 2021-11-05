Shader "Outlines/BackFaceOutlines"
{
    Properties
    {
        _Thickness("Thickness", Float) = 1 //L'amount de l'outline a extrude du mesh 
        _Color("Color", Color) = (1,1,1,1) // La couleur de l'outline
        _DepthOffset("Depth offset", Range(0,1)) = 0 //An offset to the clip space 7, pushing the outline back
        //If enabled, this shader will use "smoothed" normals stored in TEXCOORD1 to extrude along
        [Toggle(USE_PRECALCULATED_OUTLINE_NORMALS)]_PrecalculateNormals("Use UV1 normals", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"
        }
        LOD 100

        Pass
        {
            Name "Outlines"
            //Cull front faces
            Cull Front

            HLSLPROGRAM
            //Standard URP requierements
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            //Register our material keywords
            #pragma shader_feature USE_PRECALCULATED_OUTLINE_NORMALS
            
            //Register functions
            #pragma vertex Vertex
            #pragma fragment Fragment

            //Include or logic file
            #include "BackFaceOutlines.hlsl"
            ENDHLSL
        }
    }
}