Shader "Hidden/Custom/Blur"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float _Radius;
        float _Sigma;


        float CalculateGaussFunc(float x, float y)
        {
            float GChannel;

            // This is equal to 2*σ² where σ = _Blend
            float sigmaCoef = 2 * _Sigma * _Sigma;

            GChannel = ((1 / sqrt( 3.1416 * sigmaCoef)) * 2.7182);

            float exponent = ((x*x) + (y*y)) / sigmaCoef;
            
            GChannel = pow(GChannel, -exponent);

            return GChannel;
        }

        float4 BlurOneStep(float2 coord)
        {
            float4 color = (0,0,0,1);
            float2 currentCoord;


            float gaussCoef = CalculateGaussFunc(1,1);
            
            currentCoord = coord + (_Radius, _Radius);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (-_Radius, _Radius);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (_Radius, -_Radius);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (-_Radius, -_Radius);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);

            gaussCoef = CalculateGaussFunc(0,1);
            
            currentCoord = coord + (_Radius, 0);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (-_Radius, 0);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (0, -_Radius);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (0, _Radius);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);

            gaussCoef = CalculateGaussFunc(0,0);
            color += gaussCoef*SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, coord);
            color /= 9;

            return color;

        }

        float4 simpleBlur(float2 coord){

            float4 color = (0,0,0,0);
            float2 currentCoord;

            currentCoord = coord + (_Radius, _Radius);
            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (-_Radius, _Radius);
            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (_Radius, -_Radius);
            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (-_Radius, -_Radius);
            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);            
            currentCoord = coord + (_Radius, 0);
            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (-_Radius, 0);
            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (0, -_Radius);
            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);
            currentCoord = coord + (0, _Radius);
            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);

            color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, coord);
            color /= 9;

            return color;
        }

        float4 Frag(VaryingsDefault vd) : SV_Target
        {
            //float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, vd.texcoord);
            //float4 color = (0,0,0,1);

            // float2 currentCoord = (0,0);
            // float cStep = (0,0);

            // int steps = 1;

            // cStep.x = lerp(vd.texcoord.x, vd.texcoord.x + _Radius, 1);
            // for (int i = -_Steps; i <= _Steps; i++)
            // {
            //     float xStep = lerp(vd.texcoord.x, vd.texcoord.x + _Radius, abs(i)/_Steps); 
            //     if (i < 0)
            //         xStep *= -1;

            //     currentCoord.x = vd.texcoord.x + xStep;

            //     if (i != 0 && currentCoord.x >= 0 && currentCoord.x <= 1)
            //     {
            //         for (int j = -_Steps; j <= _Steps; j++)
            //         {
            //             float yStep = lerp(vd.texcoord.y, vd.texcoord.y + _Radius, abs(j)/_Steps); 
            //             if (j < 0)
            //                 yStep *= -1;
                            
            //             currentCoord.y = vd.texcoord.y + yStep;

            //             if (j != 0 && currentCoord.y >= 0 && currentCoord.y <= 1)
            //             {

            //                 color += CalculateGaussFunc(i, j) * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, currentCoord);                            
            //             }
            //         }  
            //     }
            // }

            return simpleBlur(vd.texcoord);
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}