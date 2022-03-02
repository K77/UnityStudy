Shader "URP/Quaternion"
{
    Properties

    {

        _MainTex("MainTex",2D)="white"{}

        _BaseColor("BaseColor",Color)=(1,1,1,1)

        _angle("Angle",Range(0,360))=0//旋转度数

        _axis("axis",Vector)=(1,1,1,0)//旋转轴

    }

    SubShader

    {
        Cull Off 
        Tags{

        "RenderPipeline"="UniversalRenderPipeline"

        "RenderType"="Opaque"

        }

        HLSLINCLUDE

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(PerMaterials)

        float4 _MainTex_ST;

        half4 _BaseColor;

        float _angle;

        float4 _axis;

        CBUFFER_END

        sampler2D _MainTex;

         struct a2v

         {

             float4 positionOS:POSITION;

             float4 normalOS:NORMAL;

             float2 texcoord:TEXCOORD;

         };

         struct v2f

         {

             float4 positionCS:SV_POSITION;

             float2 texcoord:TEXCOORD;

         };

        ENDHLSL


        pass

        {

            HLSLPROGRAM

            #pragma vertex VERT

            #pragma fragment FRAG

            float4 Multi_Granlam(float4 Q1,float4 Q2);

            v2f VERT(a2v i)

            {

                v2f o;

                float4 _Quaternion_V=float4(0,i.positionOS.xyz);//纯四元数v 实部为0 虚部为模型坐标系下到顶点的向量

                float4 _Quaternion_P=float4(cos(_angle*PI/360),sin(_angle*PI/360)*_axis.xyz);//单位四元数p 实部为旋转角一半的余弦值 虚部为旋转一半的正弦值再乘旋转轴

                _Quaternion_P=normalize(_Quaternion_P);//单位化

                float4 _Quaternion_I_P=float4(_Quaternion_P.x,-_Quaternion_P.yzw);//共轭四元数p* 因为是单位四元数 故p*=p的逆

                float4 _Quaternion_V1=Multi_Granlam(_Quaternion_P,_Quaternion_V);//v'=pvp-1 这里要用Graßmann积

                _Quaternion_V1=Multi_Granlam(_Quaternion_V1,_Quaternion_I_P);


                o.positionCS=TransformObjectToHClip(_Quaternion_V1.yzw);

                o.texcoord=TRANSFORM_TEX(i.texcoord,_MainTex);

                return o;

            }

            real4 FRAG(v2f i):SV_TARGET

            {
                float4 tex=tex2D(_MainTex,i.texcoord)*_BaseColor;
                return tex;
            }

            float4 Multi_Granlam(float4 Q1,float4 Q2)//定义四元数的乘法函数 Graßmann积
            {

            float a=Q1.x*Q2.x-dot(Q1.yzw,Q2.yzw);//

            float3 u=Q1.x*Q2.yzw+Q2.x*Q1.yzw+cross(Q1.yzw,Q2.yzw);

            return float4(a,u);

            }

            ENDHLSL

        }

    }

}