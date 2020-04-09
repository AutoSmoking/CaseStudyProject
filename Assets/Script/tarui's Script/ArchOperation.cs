using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchOperation : MonoBehaviour
{
    [SerializeField, Header("わっかの大きさ")]
    private float majorRadius = 3f;
    [SerializeField, Header("わっかの太さ")]
    private float minorRadius = 1f;
    [SerializeField, Header("わっかの分割数")]
    private int thetaSegments = 8;
    [SerializeField, Header("太さ部分の分割数")]
    private int phiSegments = 8;

    public Material material;
    public PhysicMaterial physicMaterial;

    private void Awake()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[(1 + phiSegments) * 2 + (thetaSegments + 1) * phiSegments];
        int[] triangles = new int[phiSegments * 3 * 2 + thetaSegments * phiSegments * 2 * 3];

        float thetaStep = Mathf.PI * 2.0f / thetaSegments;
        float phiStep = Mathf.PI * 2.0f / phiSegments;
        int vi = 0;
        int vj = 0;
        int ti = 0;

        // creates bottom cap
        vertices[vj++] = new Vector3(0, -1, 0);
        for (int ai = 0; ai < phiSegments; ai++)
        {
            float angle = ai * phiStep;
            vertices[vj++] = new Vector3(majorRadius + minorRadius * Mathf.Cos(angle), minorRadius * Mathf.Sin(angle),0);
        }
        for (int ai = 0; ai < phiSegments; ai++)
        {
            triangles[ti++] = 0;
            triangles[ti++] = ai + 1;
            triangles[ti++] = ai != phiSegments - 1 ? ai + 2 : 1;
        }
        vi += vj;

        for (int hi = 0; hi < thetaSegments; hi++)
        {
            Quaternion rot = Quaternion.AngleAxis(hi * thetaStep * Mathf.Rad2Deg, Vector3.up);
            for (int pi = 0; pi < phiSegments; pi++)
            {
                float phi = pi * phiStep;
                vertices[vi+vj++] = rot * new Vector3(majorRadius + minorRadius * Mathf.Cos(phi), minorRadius * Mathf.Sin(phi), 0);
            }
        }
        
        for (int hi = 0; hi < thetaSegments; hi++)
        {
            int hj = hi != thetaSegments - 1 ? hi + 1 : 0;
            for (int pi = 0; pi < phiSegments; pi++)
            {
                int pj = pi != phiSegments - 1 ? pi + 1 : 0;
                int v00 = pi + hi * phiSegments;
                int v10 = pj + hi * phiSegments;
                int v01 = pi + hj * phiSegments;
                int v11 = pj + hj * phiSegments;
                ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
            }
        }
        vi += vj;

        // creates top cap
        vj = 0;
        for (int ai = 0; ai < phiSegments; ai++)
        {
            float angle = ai * phiStep;
            vertices[vi + vj++] = new Vector3(majorRadius + minorRadius * Mathf.Cos(angle), minorRadius * Mathf.Sin(angle), 0);
        }
        vertices[vi + vj++] = new Vector3(0, 1, 0);
        for (int ai = 0; ai < phiSegments; ai++)
        {
            triangles[ti++] = vertices.Length - 1;
            triangles[ti++] = (ai != phiSegments - 1 ? ai + 1 : 0) + vi;
            triangles[ti++] = ai + vi;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (!meshFilter) meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();

        //MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        //if (!meshCollider) meshCollider = gameObject.AddComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial = material;
        //meshCollider.sharedMesh = mesh;
        //meshCollider.sharedMaterial = physicMaterial;
    }

    private int MakeQuad(int[] triangles, int ti, int v00, int v10, int v01, int v11)
    {
        triangles[ti] = v00;
        triangles[ti + 1] = triangles[ti + 5] = v01;
        triangles[ti + 2] = triangles[ti + 4] = v10;
        triangles[ti + 3] = v11;
        return ti + 6;
    }
}
