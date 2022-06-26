// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Image Effect Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform float4x4 _FrustumCornersES;
            uniform sampler2D _MainTex;
            uniform float4 _MainTex_TexelSize;
            uniform float4x4 _CameraToWorldMatrix;
            uniform float3 _CameraWorldPos;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 ray : TEXCOORD1;
            };

            float SignedDistanceForTorus(float3 p, float2 t)
            {
                float2 q = float2(length(p.xz) - t.x, p.y);
                return length(q) - t.y;
            }

            float map(float3 p)
            {
                return SignedDistanceForTorus(p, float2(1, 0.2));
            }

            fixed4 rayMarch(float3 pos, float3 dir)
            {
                const int stepCount = 64;
                float passedLength = 0.0f;

                for (int i = 0; i < stepCount; i++)
                {
                    float currentPos = pos + dir * passedLength;
                    float signedDistance = map(currentPos);

                    if (signedDistance < 0.001)
                        return fixed4(0.5, 0.5, 0.5, 1);

                    passedLength += signedDistance;
                }

                return fixed4(0, 0, 0, 0);
            }

            v2f vert(appdata v)
            {
                v2f o;

                half matrixIndex = v.vertex.z;
                v.vertex.z = 0.1;

                #if UNITY_UV_STARTS_AT_TOP
                if (_MainTex_TexelSize.y < 0)
                    o.uv.y = 1 - o.uv.y;
                #endif

                o.ray = _FrustumCornersES[(int)matrixIndex].xyz;
                o.ray = mul(_CameraToWorldMatrix, o.ray);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv.xy;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 rayDir = normalize(i.ray.xyz);
                fixed3 color = tex2D(_MainTex, i.uv);
                float3 pos = _CameraWorldPos.xyz;
                fixed4 objColor = rayMarch(pos, rayDir);
                
                fixed3 finalColor = objColor.xyz * objColor.w + color * (1.0 - objColor.w);
                return fixed4(finalColor, 1);
            }
            ENDCG
        }
    }
}