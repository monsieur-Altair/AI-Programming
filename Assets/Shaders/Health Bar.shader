Shader "Health Bar"
{
    Properties
    {
        _ColorA ("ColorA", Color) = (1,1,1,1)
        _ColorB ("ColorB", Color) = (1,1,1,1)
        _BorderColor ("Border Color", Color) = (1,1,1,1)

        _MinThreshold ("Min", Range (0,50)) = 20
        _MaxThreshold ("Max", Range (51,100)) = 80

        _Health ("Health", Range(0,100)) = 100

        _HealthTex ("Texture", 2D) = "white"{}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue" = "Transparent"
        }

        Pass
        {
            zwrite off
            blend SrcAlpha OneMinusSrcAlpha
            cull off

            //srcColor * X + dstColor * Y 

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

            float4 _ColorA;
            float4 _ColorB;
            float4 _BorderColor;

            float _MinThreshold;
            float _MaxThreshold;

            float _Health;

            sampler2D _HealthTex;


            v2f vert(appdata input)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.normal = UnityObjectToWorldNormal(input.normal);
                output.uv = input.uv;
                return output;
            }

            float InverseLerp(float a, float b, float v)
            {
                return (v - a) / (b - a);
            }

            float4 frag(v2f input) : SV_Target
            {
                float offset = 0.5;
                float count = 8;
                float2 coord = input.uv;

                coord.x *= count;

                float2 clampedPos = float2(clamp(coord.x, 0 + offset, count - offset), 0.5);
                float dist = distance(clampedPos, coord) * 2 - 1;
                clip(-dist);
                float barBorder = dist + 0.2;
                barBorder = step(0, barBorder);
                //return float4(dist.xxx, 1);



                
                float t = InverseLerp(_MinThreshold, _MaxThreshold, _Health);
                t = saturate(t);

                //float border = InverseLerp(0, 100, _Health);
                float healthBorder = _Health / 100;

                float mask = input.uv.x < healthBorder;
                //float4 healthColor = lerp(_ColorA, _ColorB, t);
                float4 healthColor = tex2D(_HealthTex, float2(t, input.uv.y));

                return lerp(healthColor * mask, _BorderColor, barBorder.x);
                //return lerp(back, healthColor, mask);
            }
            ENDCG
        }
    }
}