using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class meshGenerator
{

    public static MeshData GenerisiTeren(float[,] heightmap, float visinaDodavanje,AnimationCurve meshVisina,int jedostavnostDetalja)
    {
        int sirina = heightmap.GetLength(0);
        int visina = heightmap.GetLength(1);
        float levix = (sirina - 1) / -2f;
        float leviz = (visina - 1) / 2f;

        int inkrementJednostavnostiDetalja = jedostavnostDetalja * 2;
        int verticesPoLiniji = (sirina - 1) / inkrementJednostavnostiDetalja + 1;
        MeshData meshdata = new MeshData(verticesPoLiniji, verticesPoLiniji);
  

        int vertexindex = 0;
        for(int j=0;j<visina;j+=inkrementJednostavnostiDetalja)
        {
            for(int i=0;i<sirina;i+=inkrementJednostavnostiDetalja)
            {
                meshdata.vertices[vertexindex] = new Vector3(levix+i, meshVisina.Evaluate(heightmap[i, j])*visinaDodavanje, leviz-j);
                meshdata.uvs[vertexindex] = new Vector2(i / (float)sirina, j/ (float)visina);
                if(i<sirina-1&&j<visina-1)
                {
                    meshdata.AddTriangle(vertexindex, vertexindex + verticesPoLiniji + 1, vertexindex + verticesPoLiniji);
                    meshdata.AddTriangle(vertexindex+verticesPoLiniji+1, vertexindex, vertexindex + 1);

                }
                vertexindex++;
            }
        }
        return meshdata;
    }

}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangle;
    public Vector2[] uvs;

    int index;
    public MeshData(int meshSirina,int meshVisina)
    {
        vertices = new Vector3[meshSirina * meshVisina];
        uvs = new Vector2[meshSirina * meshVisina];
        triangle = new int[(meshSirina - 1) * (meshVisina - 1) * 6];
    }

    public void AddTriangle(int a,int b,int c)
    {
        triangle[index] = a;
        triangle[index + 1] = b;
        triangle[index + 2] = c;
        index += 3;
    }
    public Mesh kreirajMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangle;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
