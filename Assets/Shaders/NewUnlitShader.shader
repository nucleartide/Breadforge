Shader "Jason/SimpleColor"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white"
//             _EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
// 
//             _MinDepth("Min Depth", Float) = 0.0
//             _MaxDepth("Max Depth", Float) = 1.0
//             _Thickness("Thickness", Float) = 1.0
     }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            HLSLPROGRAM
            // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
            #pragma vertex Vert
            #pragma fragment Frag

            TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
            float4 _MainTex_TexelSize;

            TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
            float4x4 unity_MatrixMVP;

            half _MinDepth;
            half _MaxDepth;
            half _Thickness;
            half4 _EdgeColor;

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 screen_pos : TEXCOORD2;
            };

            inline float4 ComputeScreenPos(float4 pos)
            {
                float4 o = pos * 0.5f;
                o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w;
                o.zw = pos.zw;
                return o;
            }

            v2f Vert(AttributesDefault v)
            {
                v2f o;
                o.vertex = float4(v.vertex.xy, 0.0, 1.0);
                o.uv = TransformTriangleVertexToUV(v.vertex.xy);
                o.screen_pos = ComputeScreenPos(o.vertex);
            #if UNITY_UV_STARTS_AT_TOP
                o.uv = o.uv * float2(1.0, -1.0) + float2(0.0, 1.0);
            #endif
                return o;
            }

            float4 Frag(v2f i) : SV_TARGET
            {
                float4 original = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                // For testing
                float4 depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.uv);

                // Four sample UV points
                float offset_positive = +ceil(_Thickness * 0.5);
                float offset_negative = -floor(_Thickness * 0.5);
                float left = _MainTex_TexelSize.x * offset_negative;
                float right = _MainTex_TexelSize.x * offset_positive;
                float top = _MainTex_TexelSize.y * offset_negative;
                float bottom = _MainTex_TexelSize.y * offset_positive;
                float2 uv0 = i.uv + float2(left, top);
                float2 uv1 = i.uv + float2(right, bottom);
                float2 uv2 = i.uv + float2(right, top);
                float2 uv3 = i.uv + float2(left, bottom);

                // Sample depth.
                float d0 = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv0));
                float d1 = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv1));
                float d2 = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv2));
                float d3 = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv3));

                float d = length(float2(d1 - d0, d3 - d2));
                d = smoothstep(_MinDepth, _MaxDepth, d);
                half4 output = d;

                return output;
            }

            /*
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
            struct Attributes
            {
                // The positionOS variable contains the vertex positions in object
                // space.
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                // The positions in this struct must have the SV_POSITION semantic.
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };

            // Constant buffer definition
            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
            CBUFFER_END

            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                // Declaring the output object (OUT) with the Varyings struct.
                Varyings OUT;
                // The TransformObjectToHClip function transforms vertex positions
                // from object space to homogenous clip space.
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                // Returning the output.
                return OUT;
            }

            // The fragment shader definition.
            half4 frag() : SV_Target
            {
                // Defining the color variable and returning it.
                half4 customColor = half4(0.5, 0, 0, 1);
                return customColor;
            }

            half4 frag() : SV_Target
            {
                return _BaseColor;
            }
            */
            ENDHLSL
        }
    }
}
