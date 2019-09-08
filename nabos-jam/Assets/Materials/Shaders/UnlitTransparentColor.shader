Shader "Unlit/Transparent Color"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    Category
    {
        Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
        Zwrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        SubShader
        {
            Pass
            {

                Cull Off

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    UNITY_FOG_COORDS(1)
                    float3 normal : NORMAL;
                    float4 vertex : SV_POSITION;
                };

                fixed4 _Color;

                v2f vert (appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    o.normal = UnityObjectToWorldNormal(v.normal);
                    return o;
                }

                fixed4 frag (v2f i) : SV_Target
                {
                    // sample the texture
                    fixed4 col = _Color;
                    col.rgb *= i.normal* 0.5 + 0.5;;
                    // apply fog
                    //UNITY_APPLY_FOG(i.fogCoord, col);
                    return col;
                }

                ENDCG
            }
        }       
    }
    Fallback "Transparent/VertexLit"
}
