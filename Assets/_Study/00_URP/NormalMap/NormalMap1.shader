Shader "URP/NormalMap1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalTex ("normal", 2D) = "white" {}
        
        _BaseColor("Color",Color)=(1,1,1,1)
        _SpecularRange("SpecularRange",Range(10,300))=10
        _SpecularColor("SpecularColor",Color)=(1,1,1,1)
    }
    SubShader

    {
        Tags { "RenderType"="Opaque" 
        "RenderPipeline"="UniversalRenderPipeline"}
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_ST;
        real4 _BaseColor;
        float _SpecularRange;
        real4 _SpecularColor;
        CBUFFER_END
        
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);

                TEXTURE2D(_NormalTex);
        SAMPLER(sampler_NormalTex);

        struct  a2v
        {
            float4 positionOS:POSITION ;
            float3 normalOS:NORMAL;
            float3 tangent:TANGENT;
            float2 texcoord:TEXCOORD0;
        } ;

        struct v2f
        {
            float4 positionCS:SV_POSITION;
            float2 texcoord:TEXCOORD0;

            float3 trans0:TEXCOORD1;
            float3 trans1:TEXCOORD2;
            float3 trans2:TEXCOORD3;

            
            // float4 normalWS:NORMAL;
            // float4 tangentWS:TANGENT;
            // float3 viewDirWS:TEXCOORD0 ; 
            // float4 BtangentWS:TEXCOORD2;
        };
        ENDHLSL
        Pass
        {NAME"MainPass"
            Tags{
                "LightMode"="UniversalForward"
            }
            HLSLPROGRAM
            #pragma vertex vert1
            #pragma fragment  frag1
            v2f vert1(a2v i)
            {
                v2f o;
                o.positionCS=TransformObjectToHClip(i.positionOS.xyz);
                float3 tangent2 = cross(i.normalOS,i.tangent);
                
            o.trans0 = i.tangent;
            o.trans1 = tangent2;
            o.trans2 = i.normalOS;
                // Light mylight=GetMainLight();
                // o.lightDirTan=mul(trans,mylight.direction);
                // o.viewDirTan = mul(trans,(_WorldSpaceCameraPos.xyz-TransformObjectToWorld(i.positionOS.xyz)));//得到世界空间的视图方向
                o.texcoord=TRANSFORM_TEX(i.texcoord,_MainTex);
                return  o;
            } 

            real4 frag1(v2f i):SV_TARGET
            {
                Light mylight=GetMainLight();
                float3 LightDirWS=normalize( mylight.direction);
                real3 normal = UnpackNormal(SAMPLE_TEXTURE2D(_NormalTex,sampler_NormalTex,i.texcoord));
                float3x3 trans = float3x3(i.trans0,i.trans1,i.trans2);
                real3 normlWorld = mul(trans,normal);
                real4 texcolor=(dot(normlWorld,LightDirWS)*0.5+0.5)*SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.texcoord)*_BaseColor/PI;
                return texcolor;
            }
            ENDHLSL
        }
    }
}