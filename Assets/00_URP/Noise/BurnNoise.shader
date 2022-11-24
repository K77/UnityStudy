Shader "URP/BurnNoise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("NoiseTex", 2D) = "white" {}
        _BurnRange("BurnRange",Range(0,1))=0

        _BaseColor("Color",Color)=(1,1,1,1)
        _SpecularRange("SpecularRange",Range(10,300))=10
        _SpecularColor("SpecularColor",Color)=(1,1,1,1)
    }
    SubShader

    {
        Tags {
            "QUEUE"="Transparent"  
            "RenderType"="Transparent" 
            "RenderPipeline"="UniversalRenderPipeline"
            }
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_ST;
        real4 _BaseColor;
        float _SpecularRange;
        float _BurnRange;
        real4 _SpecularColor;
        CBUFFER_END
        
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_NoiseTex);
        SAMPLER(sampler_NoiseTex);

        struct  a2v
        {
            float4 positionOS:POSITION ;
            float3 normalOS:NORMAL;
            float2 texcoord:TEXCOORD0;
        } ;

        struct v2f
        {
            float4 positionCS:SV_POSITION;
            float3 normalWS:NORMAL;
            float3 viewDirWS:TEXCOORD0 ; 
            float2 texcoord:TEXCOORD1  ;
        };
        ENDHLSL
        Pass
        {NAME"MainPass"
            Tags{
                "LightMode"="UniversalForward"
            }
            ZWrite off
            Blend SrcAlpha OneMinusSrcAlpha  
            HLSLPROGRAM
            #pragma vertex vert1
            #pragma fragment  frag1
            v2f vert1(a2v i)
            {
                v2f o;
                o.positionCS=TransformObjectToHClip(i.positionOS.xyz);
                o.normalWS=TransformObjectToWorldNormal(i.normalOS,true);
                o.viewDirWS=normalize(_WorldSpaceCameraPos.xyz-TransformObjectToWorld(i.positionOS.xyz));//得到世界空间的视图方向
                o.texcoord=TRANSFORM_TEX(i.texcoord,_MainTex);
                return  o;
            } 

            real4 frag1(v2f i):SV_TARGET
            {
                // Light mylight=GetMainLight();
                // float3 LightDirWS=normalize( mylight.direction);
                // float spe=dot(normalize(LightDirWS+i.viewDirWS),i.normalWS);//需要取正数
                // real4 specolor=(pow(saturate(spe),_SpecularRange)*_SpecularColor);
                // real4 texcolor=(dot(i.normalWS,LightDirWS)*0.5+0.5)*SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.texcoord)*_BaseColor/PI;

                real4 texcolor = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.texcoord);
                real4 noiseColor = SAMPLE_TEXTURE2D(_NoiseTex,sampler_NoiseTex,i.texcoord);
                if (noiseColor.r < _BurnRange)
                {
                    return float4(1,1,1,0);
                }
                return texcolor;
            }
            ENDHLSL
        }
    }
}