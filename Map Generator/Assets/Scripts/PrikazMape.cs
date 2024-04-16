using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrikazMape : MonoBehaviour
{
    public MeshFilter filter;
    public MeshRenderer MeshRenderer;
    public Renderer textrueRenderer;
    public void nacrtajTeksture(Texture2D tekstura)
    {
        textrueRenderer.sharedMaterial.mainTexture = tekstura;
        textrueRenderer.transform.localScale = new Vector3(tekstura.width, 1, tekstura.height);
    }
    public void DrawMesh(MeshData meshdata,Texture2D tekstura)
    {
        filter.sharedMesh = meshdata.kreirajMesh();
        MeshRenderer.sharedMaterial.mainTexture = tekstura;
    }
}
