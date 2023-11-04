Shader "Ibrahim/Frame"
{
    Properties
    {

        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo", 2D) = "white" {}
        _Normal("Normal Map", 2D) = "bump" {}
        _BumbMount("Bumb Mount", Range(0,10)) = 1
        _Brightness("Brightness", Range(0,10)) = 1
    }
    SubShader
    {

        CGPROGRAM
        #pragma surface surf Lambert


        sampler2D _MainTex;
        sampler2D _Normal;
        half _BumbMount;
        half _Brightness;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_Normal;
        };



        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = (tex2D(_MainTex, IN.uv_MainTex) * _Color * _Brightness).rgb;
            o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));
            o.Normal *= float3(_BumbMount, _BumbMount, 1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
