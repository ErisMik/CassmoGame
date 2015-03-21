Shader "Custom/VertexLit 2" {
        Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Albedo ("Albedo", Range(0,1)) = 0.5
		_Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        }
 
        SubShader {
                Tags {
                        "Queue" = "Transparent"
                        "RenderType" = "Opaque"
                }
                CGPROGRAM
                #pragma surface surf Lambert
                struct Input {
                        float4 color : color;
                        float2 uv_mainTex;
                };
                sampler2D _MainTex;
                fixed4 _Color;
               
                void surf(Input IN, inout SurfaceOutput o) {
                        o.Albedo = tex2D(_MainTex, IN.uv_mainTex).rgb * IN.color.rgb * _Color.rgb;
                        o.Alpha = tex2D(_MainTex, IN.uv_mainTex).a * IN.color.a * _Color.a;
                        o.Specular = 0;
                        o.Gloss = 0;
                }
                ENDCG
        }
        //Fallback "Alpha/VertexLit", 1
}