Shader "URP/WaterCar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            HLSLPROGRAM
            #pragma vertex vert1
            #pragma fragment  frag1

            float4 ComputeNDCPos(float4 posCS)
            {
               float4 posNDC    = 0;
               posNDC.xy = float2(posCS.x , posCS.y * _ProjectionParams.x) + posCS.w;
                posNDC.xy = posNDC.xy / posCS.w /2;
               posNDC.zw 	    = posCS.zw;
               return posNDC;
            }
            
            v2f vert1(a2v i)
            {
                v2f o;
                o.positionCS=TransformObjectToHClip(i.positionOS.xyz);
                o.normalWS=TransformObjectToWorldNormal(i.normalOS,true);
                o.viewDirWS=normalize(_WorldSpaceCameraPos.xyz-TransformObjectToWorld(i.positionOS.xyz));//得到世界空间的视图方向

                float2 ab = ComputeNDCPos(o.positionCS);
                ab.y = 1-ab.y;
                // ab.x = 1-ab.x;
                o.texcoord = ab;
            
                // o.texcoord=TRANSFORM_TEX(i.texcoord,_MainTex);
                return  o;
            } 

            real4 frag1(v2f i):SV_TARGET
            {
                // float2 ab = ComputeNDCPos(i.positionCS);
                // ab.y = 1-ab.y;
                // // ab.x = 1-ab.x;
                // i.texcoord = ab;
                //if (i.texcoord.y <0 || i.texcoord.y>1)
                {
                    clip(i.texcoord.y);
                    clip(1-i.texcoord.y);
                }
                Light mylight=GetMainLight();
                float3 LightDirWS=normalize( mylight.direction);
                float spe=dot(normalize(LightDirWS+i.viewDirWS),i.normalWS);//需要取正数
                real4 specolor=pow(saturate(spe),_SpecularRange)*_SpecularColor;
                real4 texcolor=(dot(i.normalWS,LightDirWS)*0.5+0.5)*SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.texcoord)*_BaseColor/PI;

                texcolor*=real4(mylight.color,1);
                return specolor+texcolor;
            }
            ENDHLSL
        }
    }
}