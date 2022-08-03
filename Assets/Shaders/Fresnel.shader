Shader "Fresnel"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _FresnelPower ("_FresnelPower", Range(0,0.7)) = 0
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
            blend SrcAlpha oneMinusSrcAlpha
            ztest lequal
            cull back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

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
                float3 worldPos : TEXCOORD1;
            };

            float4 _Color;
            float _FresnelPower;


            v2f vert(appdata input)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.normal = UnityObjectToWorldNormal(input.normal);
                output.worldPos = mul(unity_ObjectToWorld, input.vertex);
                return output;
            }

            float4 frag(v2f input) : SV_Target
            {
                const float3 NORMAL_DIR = normalize(input.normal);
                const float3 VIEW_DIR = normalize(_WorldSpaceCameraPos - input.worldPos);

                float fresnel = 1 - dot(VIEW_DIR, NORMAL_DIR);
                fresnel = (fresnel + _FresnelPower) * _FresnelPower;
                float4 fresnelColor = fresnel * _Color.xyzw;

                //
                return fresnelColor;
            }
            ENDCG
        }
    }
}