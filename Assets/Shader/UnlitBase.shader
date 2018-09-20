// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
//Base unlit shaders, need to work out lighting 
Shader "Unlit/UnlitBase"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			// indicate that our pass is the "base" pass in forward
			// rendering pipeline. It gets ambient and main directional
			// light data set up; light direction in _WorldSpaceLightPos0
			// and color in _LightColor0
			Tags{ "LightMode" = "ForwardBase" }
			//Tags{ "RenderType" = "Opaque" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal: NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				half3 normal : NORMAL;
				UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				float4 worldVertex : TANGENT;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				o.worldVertex = mul(UNITY_MATRIX_M, v.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o, o.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				half nl = max(0.2, dot(i.normal, _WorldSpaceLightPos0.xyz));
				fixed4 col = tex2D(_MainTex, i.uv) * _Color;

				half3 refl = 2 * dot(i.normal, _WorldSpaceLightPos0.xyz) * i.normal - _WorldSpaceLightPos0.xyz;
				half3 dir = normalize(_WorldSpaceCameraPos - i.worldVertex);
				float spec = max(0, dot(refl, dir));

				spec = spec*spec*spec*spec;
				spec = spec*spec*spec*spec;

				// factor in the light color
				//o.diff = nl * _LightColor0;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col*nl + spec*fixed4(1,1,1,1);
			}
			ENDCG
		}
	}
}
