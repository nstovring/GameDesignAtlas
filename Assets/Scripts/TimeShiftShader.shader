Shader "Custom/TimeShiftShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Texture2("Texture2", 2D) = "black" {}
		_BlendTexture("Blending Texture", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
		_BumpMap2("Detereated Normal Map", 2D) = "bump" {}
		_SpecColor("Specular Color", Color) = (1.0,1.0,1.0,1.0)
		_Shininess("Shininess", Float) = 10.0
		_RimColor("Rim Color", Color) = (1.0,1.0,1.0,1.0)
		_RimPower("Rim Power", Range(0.1,10.0)) = 3.0
		_Deteriotated("In past", Int) = 0
		_Blend("Blend weight", Range(0,1)) = 1
		_TimeShift("Time shift weight", Range(0,1)) = 1
	}
	SubShader {
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True"  "RenderType" = "Transparent" }
		//Cull Off
		LOD 200

		Pass{
		ZWrite On
		ColorMask 0	
		}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		uniform sampler2D _MainTex;
		uniform sampler2D _BumpMap;
		//uniform float4 _BumpMap_ST;
		uniform sampler2D _BumpMap2;
		//uniform float4 _BumpMap2_ST;
		uniform sampler2D _Texture2;
		uniform sampler2D _BlendTexture;
		uniform float4 _RimColor;
		uniform float _Shininess;
		uniform float _RimPower;
		uniform float _Blend;
		uniform float _TimeShift;

		//unity defined variables

		struct Input {
			float2 uv_MainTex;
			float2 uv_Texture2;
			float2 uv_BlendTexture;
			float2 uv_BumpMap;
			float2 uv_BumpMap2;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			if (_TimeShift > 0.0f) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

				fixed4 sec = tex2D(_Texture2, IN.uv_Texture2);
				fixed4 filter = tex2D(_BlendTexture, IN.uv_BlendTexture);

				o.Albedo = lerp(c.rgb, sec.rgb, filter.rgb * _Blend) ;

				float3 bump1 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
				float3 bump2 = UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap2));

				o.Normal = lerp(bump1, bump2, _Blend);

				//o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = _TimeShift;
			}
			else {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = _TimeShift;
			}

			//// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Albedo = c.rgb;
			//// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			//o.Alpha = _TimeShift;
		}
		ENDCG

		//UsePass "Transparent/Diffuse/FORWARD"
	}
	FallBack "Diffuse"
}
