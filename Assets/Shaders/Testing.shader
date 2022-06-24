Shader "Testing"
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
            "RenderType"="Transparent"
            //"RenderType"="Opaque"
            "Queue"="Transparent"
        }

        zwrite off
        blend one one
        cull off
        
        Pass
        {
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


            v2f vert(appdata input)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.normal = UnityObjectToWorldNormal(input.normal);
                output.uv = input.uv;
                return output;
            }

            float4 frag(v2f input) : SV_Target
            {
                //fixed4 color = tex2D(_MainTex, i.uv);
                //float4 color = lerp(_ColorA, _ColorB, length(2 * input.uv - float2(1,1)));

                // float t = saturate(InverseLerp(_Start, _End, input.uv.x));
                // float4 color = lerp(_ColorA, _ColorB, t);

                //float x = frac(input.uv.x);
                //float t = cos(x * 2 * UNITY_TWO_PI) * 0.5 + 0.5f;
                //float4 color = lerp(_ColorA, _ColorB, t);

                float offset = cos(input.uv.x * UNITY_TWO_PI * 8) * 0.01;
                float x = (input.uv.y + offset - _Time.y * 0.1);
                float t = cos(x * 5 * UNITY_TWO_PI) * 0.5 + 0.5f;
                t *= 1 - input.uv.y;

                float4 color = lerp(_ColorA, _ColorB, t)*(abs(input.normal.y)<0.99f);


                return color;
            }
            ENDCG
        }
    }
}