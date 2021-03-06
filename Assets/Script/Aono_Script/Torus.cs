﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Torus : MonoBehaviour
{
    [SerializeField] private float majorRadius = 3f;
    [SerializeField] private float minorRadius = 1f;
    [SerializeField] private int thetaSegments = 8;
    [SerializeField] private int phiSegments = 8;

    private void Awake()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[thetaSegments * phiSegments];
        int[] triangles = new int[thetaSegments * phiSegments * 2 * 3];

        float thetaStep = Mathf.PI * 2.0f / thetaSegments;
        float phiStep = Mathf.PI * 2.0f / phiSegments;
        int vi = 0;
        for (int hi = 0; hi < thetaSegments; hi++)
        {
            Quaternion rot = Quaternion.AngleAxis(hi * thetaStep * Mathf.Rad2Deg, Vector3.up);
            for (int pi = 0; pi < phiSegments; pi++)
            {
                float phi = pi * phiStep;
                vertices[vi++] = rot * new Vector3(majorRadius + minorRadius * Mathf.Cos(phi), minorRadius * Mathf.Sin(phi), 0);
            }
        }

        int ti = 0;
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

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
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
