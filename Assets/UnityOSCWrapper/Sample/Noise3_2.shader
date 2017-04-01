Shader "Hidden/Noise3_2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    // 0 ~ 1
    float _HorizonValue = 1;
    // 乱数シード
    int _Seed;

    // 乱数生成
    // http://neareal.net/index.php?ComputerGraphics%2FHLSL%2FCommon%2FGenerateRandomNoiseInPixelShader
    float rnd(float2 value, int Seed)
    {
        return frac(sin(dot(value.xy, float2(12.9898, 78.233)) + Seed) * 43758.5453);
    }

    fixed4 frag (v2f_img i) : SV_Target
    {
        // _Time.xを加算する理由は乱数が面白くなかったから
        float rndValue = rnd(float2(0, i.uv.y + _Time.x),0) - 0.5;

        // パーティクルノイズ入り
        // float rndValue = rnd(float2(step(i.uv.y, rnd(float2(i.uv.x, i.uv.y),0)) * frac(i.uv.x * _Time.x), i.uv.y), i.uv.y) - 0.5;

        // -1(左へ) or 1(右へ)
        int dir = step(abs(rndValue), _Time.x * 0.5) * 2 - 1;
        _HorizonValue = _HorizonValue * dir * rndValue;
        float vRnd = rnd(float2(_Time.x, i.uv.y),0)*2-1;
        float2 uv = float2(i.uv.x + _HorizonValue, i.uv.y + vRnd * _HorizonValue);
        return tex2D(_MainTex, uv);
    }

    ENDCG

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}