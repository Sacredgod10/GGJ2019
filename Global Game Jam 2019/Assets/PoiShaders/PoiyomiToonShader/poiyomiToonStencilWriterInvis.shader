// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader ".poiyomi/Toon/Stencil/Writer/Invis"
{
    SubShader
    {
       
        Tags { "RenderType"="Opaque" "Queue"="AlphaTest-1" }
        ColorMask 0
        ZWrite off
        Stencil
        {
            Ref 199
            Comp always
            Pass replace
        }

        CGINCLUDE
            struct appdata
            {
                float4 vertex : POSITION;
            };
            struct v2f
            {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            half4 frag(v2f i) : COLOR
            {
                return half4(1,1,0,1);
            }
        ENDCG
        
        Pass
        {
            Cull Back
            ZTest Less
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    } 
}