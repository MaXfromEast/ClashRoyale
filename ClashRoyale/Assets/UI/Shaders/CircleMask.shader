Shader "Sprites/CircleMask"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
            // Плавность перехода края (для антиалиасинга)
            _EdgeFeather("Edge Feather", Range(0.001, 0.1)) = 0.01
    }

        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True"}

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // Используем UnityCG.cginc для базовых функций
                #include "UnityCG.cginc" 

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float4 color : COLOR;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float4 color : COLOR;
                };

                sampler2D _MainTex;
                float4 _Color;
                float _EdgeFeather;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    o.color = v.color * _Color;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv) * i.color;

                // --- Логика маски круга ---

                // 1. Сдвигаем UV-координаты так, чтобы центр спрайта был в (0.5, 0.5)
                float2 centeredUV = i.uv - float2(0.5, 0.5);

                // 2. Вычисляем квадрат расстояния от центра до текущего пикселя (по теореме Пифагора)
                // Расстояние будет меняться от 0 (центр) до примерно 0.5 (края квадрата)
                float distanceSq = dot(centeredUV, centeredUV);

                // 3. Вычисляем маску: 
                // Чем ближе к центру, тем значение больше (около 0.25, что соответствует радиусу 0.5)
                // Чем дальше от центра (вне круга), тем значение меньше

                // Используем smoothstep для плавного перехода (антиалиасинга)
                // Мы хотим отбросить пиксели, где distanceSq > (радиус * радиус)
                float radius = 0.5; // Радиус 0.5 занимает весь квадрат
                float mask = smoothstep(
                    (radius * radius) - _EdgeFeather, // Начало затухания
                    (radius * radius) + _EdgeFeather, // Конец затухания
                    distanceSq // Текущее значение
                );

                // Инвертируем маску, чтобы внутри круга было 1, а снаружи 0
                mask = 1.0 - mask;

                // Применяем маску к альфа-каналу
                col.a *= mask;

                // Отбрасываем полностью прозрачные пиксели, чтобы не было проблем с откликом на UI-события
                clip(col.a - 0.01);

                return col;
            }
            ENDCG
        }
        }
}
