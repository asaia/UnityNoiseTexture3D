Shader "Noise Texture 3D"
{
	Properties
	{
		_Noise3D ("Texture", 3D) = "" {}
		_Scale ("Scale",  Range(0.05, 3.0)) = 1.0
		_Amplitude ("Amplitude", Range(0.0, 100.0)) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float3 worldPosition : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler3D _Noise3D;
			float _Scale;
			float _Amplitude;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed3 col = tex3D(_Noise3D, i.worldPosition * _Scale).rrr * _Amplitude;
				return fixed4(col, 1);
			}
			ENDCG
		}
	}
}
