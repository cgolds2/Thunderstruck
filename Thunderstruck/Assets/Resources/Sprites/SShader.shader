Shader "Custom/SShader"
{
Properties
    {
        _Color ("Color Tint", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = ""
    }
   
    Category
    {
        ZWrite Off
        Tags {"Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        SubShader
        {
            Pass
            {
                SetTexture[_MainTex] {Combine texture * constant ConstantColor[_Color]}
            }
        }
    }}
