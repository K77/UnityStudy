Shader "Instance/Indirect_VF"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
 
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100
 
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag		
			#pragma target 4.5
             #pragma multi_compile_instancing

			#include "UnityCG.cginc"
			
 
			StructuredBuffer<float4> positionBuffer;//positionBuffer
			// StructuredBuffer<float4> colorBuffer;
			// StructuredBuffer<float4x4> matrix4x4Buffer;
 
			struct appdata
			{
				fixed4 color : COLOR;
			    float4 vertex : POSITION;
			    float4 texcoord : TEXCOORD0;
			};
 
 
			struct v2f
			{
				float4 color: COLOR;
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				
			};
 
			sampler2D _MainTex;
            float4 _MainTex_ST;
 
			v2f vert(appdata v, uint instanceID : SV_InstanceID)
			{
				float4 data = positionBuffer[instanceID];
				// float4x4 materix = matrix4x4Buffer[instanceID];				
				float3 worldPosition = data.xyz + v.vertex.xyz;// + mul(materix,v.vertex.xyz * data.w);
				 
				v2f o;
				o.vertex = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0f));	
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				// o.color = colorBuffer[instanceID];
				return o;
			}
 
			fixed4 frag(v2f i) : SV_Target
			{
				return tex2D (_MainTex, i.texcoord);// * i.color;
			}
 
			ENDCG
		}
	}
}