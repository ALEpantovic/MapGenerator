using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerisiNoiseMapu(int mapaSirina, int mapaVisina,float skala,int seed,int oktave,float persistance, float lacunarity,Vector2 offset)
    {
        System.Random rnd = new System.Random(seed);
        Vector2[] oktaveOffset = new Vector2[oktave];
        for(int i =0;i<oktave;i++)
        {
            float offsetI = rnd.Next(-100000, 100000)+offset.x;
            float offsetJ = rnd.Next(-100000, 100000)+offset.y;
            oktaveOffset[i] = new Vector2(offsetI, offsetJ);
        }
        float[,] noiseMapa = new float[mapaSirina, mapaVisina];
        if(skala<=0)
        {
            skala = 0.0001f;
        }

        float najvecaNoiseVisina = float.MinValue;
        float minNoiseVisina = float.MaxValue;

        float polaSirine = mapaSirina / 2f;
        float polaVisine = mapaVisina / 2f;

        for( int j = 0;j < mapaVisina;j++ )
        {
            for (int i = 0; i < mapaSirina; i++)
            {
                float amplituda=1;
                float frekvencija=1;
                float noiseVisina=0;
                for (int x = 0; x < oktave; x++)
                {
                    float i1 = (i-polaSirine) / skala*frekvencija+oktaveOffset[x].x;
                    float j1 = (j-polaVisine) / skala*frekvencija+oktaveOffset[x].y;

                    float perlinValue = Mathf.PerlinNoise(i1, j1)*2-1;
                    noiseVisina += perlinValue * amplituda;
                    amplituda *= persistance;
                    frekvencija *= lacunarity;

                }
                if(noiseVisina>najvecaNoiseVisina)
                {
                    najvecaNoiseVisina = noiseVisina;
                }
                else if(noiseVisina<minNoiseVisina)
                {
                    minNoiseVisina = noiseVisina;
                }
                noiseMapa[i, j] = noiseVisina;
            }
        }
        for (int j = 0; j < mapaVisina; j++)
        {
            for (int i = 0; i < mapaSirina; i++)
            {
                noiseMapa[i, j] = Mathf.InverseLerp(minNoiseVisina, najvecaNoiseVisina, noiseMapa[i, j]);
            }
        }
       return noiseMapa;
    }

    public static float[,] GenerisiDrva(int mapaSirina, int mapaVisina, float skala, int seed, int oktave, float persistance, float lacunarity, Vector2 offset)
    {
        System.Random rnd = new System.Random(seed);
        Vector2[] oktaveOffset = new Vector2[oktave];
        for (int i = 0; i < oktave; i++)
        {
            float offsetI = rnd.Next(-100000, 100000) + offset.x;
            float offsetJ = rnd.Next(-100000, 100000) + offset.y;
            oktaveOffset[i] = new Vector2(offsetI, offsetJ);
        }
        float[,] noiseMapa = new float[mapaSirina, mapaVisina];
        if (skala <= 0)
        {
            skala = 0.0001f;
        }

        float najvecaNoiseVisina = float.MinValue;
        float minNoiseVisina = float.MaxValue;

        float polaSirine = mapaSirina / 2f;
        float polaVisine = mapaVisina / 2f;

        for (int j = 0; j < mapaVisina; j++)
        {
            for (int i = 0; i < mapaSirina; i++)
            {
                float amplituda = 1;
                float frekvencija = 1;
                float noiseVisina = 0;
                for (int x = 0; x < oktave; x++)
                {
                    float i1 = (i - polaSirine) / skala * frekvencija + oktaveOffset[x].x;
                    float j1 = (j - polaVisine) / skala * frekvencija + oktaveOffset[x].y;

                    float perlinValue = Mathf.PerlinNoise(i1, j1) * 2 - 1;
                    noiseVisina += perlinValue * amplituda;
                    amplituda *= persistance;
                    frekvencija *= lacunarity;

                }
                if (noiseVisina > najvecaNoiseVisina)
                {
                    najvecaNoiseVisina = noiseVisina;
                }
                else if (noiseVisina < minNoiseVisina)
                {
                    minNoiseVisina = noiseVisina;
                }
                noiseMapa[i, j] = noiseVisina;
            }
        }
        for (int j = 0; j < mapaVisina; j++)
        {
            for (int i = 0; i < mapaSirina; i++)
            {
                noiseMapa[i, j] = Mathf.InverseLerp(minNoiseVisina, najvecaNoiseVisina, noiseMapa[i, j]);
            }
        }
        return noiseMapa;
    }
}
