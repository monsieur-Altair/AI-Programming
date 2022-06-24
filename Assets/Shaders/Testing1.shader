Shader "Testing1"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _ColorA ("ColorA", Color) = (1,1,1,1)
        _ColorB ("ColorB", Color) = (1,1,1,1)

        _Start ("Start", Range(0,1)) = 0
        _End ("End", Range(0,1)) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {
            cull off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            //sampler2D _MainTex;
            //float4 _MainTex_ST;
            float4 _ColorA;
            float4 _ColorB;

            float _End;
            float _Start;

            float InverseLerp(float a, float b, float v)
            {
                return (v - a) / (b - a);
            }

            float Calc(const float value)
            {
                return cos(value * 2 * UNITY_TWO_PI + _Time.y * 4);
            }

            v2f vert(appdata input)
            {
                v2f output;
                input.vertex.z = Calc(input.uv.x)/4;
                output.vertex = UnityObjectToClipPos(input.vertex);

                output.normal = UnityObjectToWorldNormal(input.normal);
                output.uv = input.uv;
                return output;
            }

            float4 frag(v2f input) : SV_Target
            {
                float t = Calc(input.uv.x);
                return lerp(_ColorA, _ColorB, t);

                return t;
            }
            ENDCG
        }
    }
}