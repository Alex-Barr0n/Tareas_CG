/*
    Andrea Alexandra Barrón Córdova - A01783126
    Apply transformations to a car and move its wheels accordingly.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apply_Transform : MonoBehaviour
{
    [SerializeField] Vector3 displacement;
    [SerializeField] float angle;
    [SerializeField] AXIS rotationAxis;
    
    [SerializeField] GameObject llanta;
    [SerializeField] Vector3[] llantasPos;

    Mesh mesh;
    Mesh[] llantaMeshes;
    Vector3[] baseVertices;
    Vector3[] newVertices;

    void Start()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        newVertices = new Vector3[baseVertices.Length];

        // Initialize llantaMeshes array
        llantaMeshes = new Mesh[llantasPos.Length];
        
        for (int i = 0; i < llantasPos.Length; i++)
        {
            GameObject llantaTemp = Instantiate(llanta, Vector3.zero, Quaternion.identity);
            llantaMeshes[i] = llantaTemp.GetComponentInChildren<MeshFilter>().mesh;
        }
    }

    void Update()
    {
        DoTransform();
    }

    void DoTransform()
    {
        // Carro transformations
        transform.Translate(displacement * Time.deltaTime);
        transform.Rotate(rotationAxis, angle * Time.deltaTime);

        // Apply transformations to mesh vertices
        for (int i = 0; i < baseVertices.Length; i++)
        {
            newVertices[i] = transform.TransformPoint(baseVertices[i]);
        }

        mesh.vertices = newVertices;
        mesh.RecalculateNormals();

        // Llantas transformations
        for (int i = 0; i < llantasPos.Length; i++)
        {
            Transform llantaTransform = llantaMeshes[i].transform;
            llantaTransform.position = llantasPos[i];
            llantaTransform.Rotate(AXIS.X, angle * Time.deltaTime);

            // Apply transformations to llanta vertices
            for (int j = 0; j < baseVertices.Length; j++)
            {
                Vector4 temp = new Vector4(baseVertices[j].x, baseVertices[j].y, baseVertices[j].z, 1);
                llantaMeshes[i].vertices[j] = (transform.localToWorldMatrix * llantaTransform.localToWorldMatrix) * temp;
            }

            llantaMeshes[i].RecalculateNormals();
        }
    }
}
