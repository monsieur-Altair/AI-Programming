Shader "Own Lit"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Gloss ("Gloss", Range(0,1)) = 1.0
        _FresnelPower ("_FresnelPower", Range(-0.5,0.5)) =0
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
            float _Gloss;
            float _FresnelPower;


            float ByGloss(float v)
            {
                float gloss = exp2(_Gloss * 11) + 2;
                return pow(v, gloss);
            }

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
                const float3 LIGHT_DIR = normalize(_WorldSpaceLightPos0.xyz);

                const float3 REFLECTED_DIR = normalize(reflect(-LIGHT_DIR, NORMAL_DIR));
                const float3 VIEW_DIR = normalize(_WorldSpaceCameraPos - input.worldPos);

                float3 diffuseColor = saturate(dot(NORMAL_DIR, LIGHT_DIR)).xxx;
                diffuseColor *= _LightColor0.xyz;

                //phong
                float phong = saturate(dot(REFLECTED_DIR, VIEW_DIR));
                float3 phongColor = ByGloss(phong).xxx;
                phongColor *= _LightColor0.xyz;

                //phong-blinn
                const float3 RESULT_DIR = normalize(VIEW_DIR + LIGHT_DIR);
                float blinn = dot(NORMAL_DIR, RESULT_DIR);
                float3 blinnColor = ByGloss(blinn).xxx;
                blinnColor *= _LightColor0.xyz * _Gloss; //mul by gloss to support energy conservation

                //plastic vs metallic
                float3 colorPlastic = diffuseColor * _Color.xyz + blinnColor;
                float3 colorMetallic = (diffuseColor + blinnColor) * _Color.xyz;

                return float4(saturate(colorPlastic),1);
            }
            ENDCG
        }
    }
}