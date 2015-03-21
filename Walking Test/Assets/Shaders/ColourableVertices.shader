Shader "Custom/VertexColor" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Albedo ("Albedo", Range(0,1)) = 0.5
		_Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}
	Category {
	     BindChannels { 
	         Bind "Color", color 
	         Bind "Vertex", vertex
	     }
	     SubShader {
			//Tags { "RenderType"="Opaque" }
			//LOD 200
			
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Lambert

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
				float4 color : color;
			};

			half _Glossiness;
			half _Metallic;
			half _Albedo;
			fixed4 _Color;

			void surf (Input IN, inout SurfaceOutput o) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = _Albedo;//c.rgb;
				//Metallic and smoothness come from slider variables
				//o.Metallic = _Metallic;
				//o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		} 
	     //SubShader { Pass { } }
 	}
}

