﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "NormalMapShader"
{

	//"c  C"
	Properties
	{
		_Color ("Color Tint", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Texture", 2D) = "white" {}
		_Texture2("Texture2", 2D) = "black" {}
		_BlendTexture("Blending Texture", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_BumpMap2("Detereated Normal Map", 2D) = "bump" {}
		_SpecColor("Specular Color", Color) = (1.0,1.0,1.0,1.0)
		_Shininess("Shininess", Float) = 10.0
		_RimColor("Rim Color", Color) = (1.0,1.0,1.0,1.0)
		_RimPower("Rim Power", Range(0.1,10.0)) = 3.0
		_Deteriotated("In past", Int) = 0
		_Blend("BlendWeight", Range(0,1)) = 0.5
	}

	SubShader
	{
		
		Pass{
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#pragma exclude_renderers flash 

			//user defined variables
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform sampler2D _BumpMap2;
			uniform sampler2D _Texture2;
			uniform sampler2D _BlendTexture;
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float4 _RimColor;
			uniform float _Shininess;
			uniform float _RimPower;
			uniform float _Blend;

			//unity defined variables
			uniform float4 _LightColor0;

			//base input structs
			struct vertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;


			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normalWorld : TEXCOORD2;
				float3 tangentWorld : TEXCOORD3;
				float3 binormalWorld : TEXCOORD4;
			};

			//vertex function

			vertexOutput vert(vertexInput v){
				vertexOutput o;

				o.normalWorld = normalize(mul(float4(v.normal,0.0),unity_WorldToObject).xyz );
				o.tangentWorld = normalize(mul(unity_ObjectToWorld,v.tangent).xyz);
				o.binormalWorld = normalize(cross(o.normalWorld,o.tangentWorld) * v.tangent.w );

				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex = v.texcoord;

				return o;

			}

			//fragment function
			float4 frag(vertexOutput i) : COLOR 
			{
				
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld);
				float3 lightDirection;
				float atten;

				if(_WorldSpaceLightPos0.w == 0.0) { //directional light 
					atten = 1.0;
					lightDirection = normalize(_WorldSpaceLightPos0.xyz );

				}
				else {
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz ;
					float distance = length(fragmentToLightSource);
					atten = 1.0/distance;
					lightDirection = normalize(fragmentToLightSource);
				}

				//Texture Maps 
				float blendTextureAlpha = tex2D(_BlendTexture, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw).r;
				float4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw )*(1-(blendTextureAlpha*_Blend)) + tex2D(_Texture2, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw)*(blendTextureAlpha*_Blend);
				float4 texN = tex2D(_BumpMap, i.tex.xy * _BumpMap_ST.xy + _MainTex_ST.zw )*(1 - _Blend) + tex2D(_BumpMap2, i.tex.xy * _BumpMap_ST.xy + _MainTex_ST.zw)*(_Blend);

				//unpackNormal function
				float3 localCoords = float3(2.0 * texN.ag - float2(1.0,1.0),0.0);
				localCoords.z = 1.0 - 0.5 * dot(localCoords,localCoords);

				//normal transpose matrix 
				float3x3 local2WorldTranspose = float3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
					);

				//calculate normal direction

				float3 normalDirection = normalize( mul( localCoords, local2WorldTranspose));



				//lighting
				float3 diffuseReflection = atten * _LightColor0.xyz * saturate(dot(normalDirection,lightDirection));
				float3 specularReflection = diffuseReflection * _SpecColor.xyz * pow(saturate(dot(reflect(-lightDirection, normalDirection), viewDirection)) , _Shininess);

				//rim lighting
				float rim = 1 - saturate(dot(viewDirection,normalDirection));
				float3 rimLighting = saturate(dot(normalDirection,lightDirection) * _RimColor.xyz * _LightColor0.xyz * pow(rim,_RimPower));

				float3 lightFinal = UNITY_LIGHTMODEL_AMBIENT.xyz + diffuseReflection + specularReflection + rimLighting;

					

				return float4(tex.xyz * lightFinal * _Color.xyz, 1.0);


			}



			ENDCG
		}

		Pass{
			Tags{"LightMode" = "ForwardAdd"}
			Blend One One 
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#pragma exclude_renderers flash 

			//user defined variables
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float4 _RimColor;
			uniform float _Shininess;
			uniform float _RimPower;

			//unity defined variables
			uniform float4 _LightColor0;

			//base input structs
			struct vertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;


			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normalWorld : TEXCOORD2;
				float3 tangentWorld : TEXCOORD3;
				float3 binormalWorld : TEXCOORD4;
			};

			//vertex function

			vertexOutput vert(vertexInput v){
				vertexOutput o;

				o.normalWorld = normalize(mul(float4(v.normal,0.0),unity_WorldToObject).xyz );
				o.tangentWorld = normalize(mul(unity_ObjectToWorld,v.tangent).xyz);
				o.binormalWorld = normalize(cross(o.normalWorld,o.tangentWorld) * v.tangent.w );

				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex = v.texcoord;

				return o;

			}

			//fragment function
			float4 frag(vertexOutput i) : COLOR 
			{
				
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld);
				float3 lightDirection;
				float atten;

				if(_WorldSpaceLightPos0.w == 0.0) { //directional light 
					atten = 1.0;
					lightDirection = normalize(_WorldSpaceLightPos0.xyz );

				}
				else {
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz ;
					float distance = length(fragmentToLightSource);
					atten = 1.0/distance;
					lightDirection = normalize(fragmentToLightSource);
				}

				//Texture Maps 
				float4 texN = tex2D(_BumpMap, i.tex.xy * _BumpMap_ST.xy + _MainTex_ST.zw );

				//unpackNormal function
				float3 localCoords = float3(2.0 * texN.ag - float2(1.0,1.0),0.0);
				localCoords.z = 1.0 - 0.5 * dot(localCoords,localCoords);

				//normal transpose matrix 
				float3x3 local2WorldTranspose = float3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
					);

				//calculate normal direction
				float3 normalDirection = normalize( mul( localCoords, local2WorldTranspose));



				//lighting
				float3 diffuseReflection = atten * _LightColor0.xyz * saturate(dot(normalDirection,lightDirection));
				float3 specularReflection = diffuseReflection * _SpecColor.xyz * pow(saturate(dot(reflect(-lightDirection, normalDirection), viewDirection)) , _Shininess);

				//rim lighting
				float rim = 1 - saturate(dot(viewDirection,normalDirection));
				float3 rimLighting = saturate(dot(normalDirection,lightDirection) * _RimColor.xyz * _LightColor0.xyz * pow(rim,_RimPower));

				float3 lightFinal =  diffuseReflection + specularReflection + rimLighting;

					

				return float4( lightFinal * _Color.xyz, 1.0);


			}



			ENDCG
		}

		
	}
	//Fallback "Specular"
}
