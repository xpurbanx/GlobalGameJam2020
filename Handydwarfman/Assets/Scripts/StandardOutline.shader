Shader "Custom/StandardOutline"
{
    Properties {
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _MetallicGlossMap ("Metallic", 2D) = "black" {}
        _Glossiness ("Smoothness", Range(0, 1)) = 0
        _BumpMap ("Normal", 2D) = "bump" {}
        _OcclusionMap ("Occlusion", 2D) = "white" {}
        [Toggle(INVERT_OCCLUSION)]_InvertOcclusion ("Invert Occlusion", Int) = 0
        [HDR] _Emission ("Emission", color) = (0,0,0)
        [Space(10)]
        [Toggle(BETTER_ALGORITHM)] _BetterAlgorithm ("Better Algorithm", Int) = 1
		[HDR] _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Float) = 0.01
        _Angle ("Change algorithm on angle", Float) = 89
		_PulseSpeed ("Pulse animaion speed", Float) = 0
		_PulseAmplitude ("Pulse amplitude", Float) = 0
    }
    SubShader {
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

        CGPROGRAM
        //the shader is a surface shader, meaning that it will be extended by unity in the background 
        //to have fancy lighting and other features
        //our surface shader function is called surf and we use our custom lighting model
        //fullforwardshadows makes sure unity adds the shadow passes the shader might need
        //vertex:vert makes the shader use vert as a vertex shader function
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        #pragma shader_feature_local INVERT_OCCLUSION

        sampler2D _MainTex;
        sampler2D _MetallicGlossMap;
        sampler2D _BumpMap;
        sampler2D _OcclusionMap;
        fixed4 _Color;

        half _Glossiness;
        half3 _Emission;

        //The dithering pattern
        sampler2D _DitherPattern;
        float4 _DitherPattern_TexelSize;

        //remapping of distance
        float _MinDistance;
        float _MaxDistance;

        //input struct which is automatically filled by unity
        struct Input 
        {
            float2 uv_MainTex;
        };

        //the surface shader function which sets parameters the lighting function then uses
        void surf (Input i, inout SurfaceOutputStandard o) {
            //read albedo color from texture and apply tint
            fixed4 col = tex2D(_MainTex, i.uv_MainTex);
            col *= _Color;
            o.Albedo = col.rgb;
            //just apply the values for metalness, smoothness and emission
            o.Metallic = tex2D(_MetallicGlossMap, i.uv_MainTex);
            o.Normal = UnpackNormal(tex2D(_BumpMap, i.uv_MainTex));
            #ifndef INVERT_OCCLUSION
                o.Occlusion = tex2D(_OcclusionMap, i.uv_MainTex);
            #endif
            #ifdef INVERT_OCCLUSION
                o.Occlusion = -tex2D(_OcclusionMap, i.uv_MainTex) + 1.0f;
            #endif
            o.Smoothness = _Glossiness * tex2D(_MetallicGlossMap, i.uv_MainTex).a;
            o.Emission = _Emission;
        }
        ENDCG

        //The second pass where we render the outlines
        Pass
		{
            Cull Front

            CGPROGRAM

            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature_local BETTER_ALGORITHM

            //tint of the texture
            fixed4 _OutlineColor;
            float _OutlineThickness;
            float _Angle;
			float _PulseSpeed;
			float _PulseAmplitude;

            //the object data that's put into the vertex shader
            struct appdata{
                float4 vertex : POSITION;
                float4 normal : NORMAL;
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f{
                float4 position : SV_POSITION;
            };

            //the vertex shader
            v2f vert(appdata v){
                v2f o;

                #ifdef BETTER_ALGORITHM
                    float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
                    if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _Angle) 
                    {
                        v.vertex.xyz += normalize(v.normal.xyz) * ( (_OutlineThickness + abs(_PulseAmplitude * sin(_PulseSpeed * _Time.y))) / 300 );
                    }
                    else 
                    {
                        v.vertex.xyz += scaleDir * ( (_OutlineThickness + abs(_PulseAmplitude * sin(_PulseSpeed * _Time.y))) / 300);
                    }
                    o.position = UnityObjectToClipPos(v.vertex);
                #endif

                #ifndef BETTER_ALGORITHM
                    o.position = UnityObjectToClipPos(v.vertex + normalize(v.normal) * (_OutlineThickness + abs(_PulseAmplitude * sin(_PulseSpeed * _Time.y))));
                #endif

                
                return o;
            }

            //the fragment shader
            fixed4 frag(v2f i) : SV_TARGET{
                return _OutlineColor;
            }

            ENDCG
        }
    }
    FallBack "Standard"
}
