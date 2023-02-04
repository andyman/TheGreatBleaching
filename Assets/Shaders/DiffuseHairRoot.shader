Shader "Custom/DiffuseHairRoot"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_RootColor("RootColor", Color) = (0,0,0,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_RootHeight("RootHeight", Float) = 3.0
		_RootHeightVariation("RootHeightVariation", Float) = 1.0
		_NoiseTex("Noise Tex", 2D) = "black" {}
		_NoiseTexScale("Noise Tex Scale", Float) = 1.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Lambert fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			struct Input
			{
				float2 uv_MainTex;
				float3 worldPos;
				fixed4 color : COLOR;
			};

			fixed4 _Color;
			fixed4 _RootColor;
			float _NoiseTexScale;
			float _RootHeight;
			float _RootHeightVariation;

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutput o)
			{
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color * IN.color;

				float modHeight = IN.worldPos.y
					+ tex2D(_NoiseTex, IN.worldPos.zx * _NoiseTexScale).x
					+ tex2D(_NoiseTex, IN.worldPos.zx * _NoiseTexScale * 0.1).y;
				c = lerp(_RootColor, c, step(_RootHeight, modHeight));
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
