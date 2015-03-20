Shader "Custom/VertexColor" {
	Category {
     BindChannels { 
         Bind "Color", color 
         Bind "Vertex", vertex
     }
     SubShader { Pass { } }
 }
}

