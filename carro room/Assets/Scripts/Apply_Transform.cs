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
    Mesh llantaMesh1, llantaMesh2, llantaMesh3, llantaMesh4;

    Vector3[] baseVertices;
    Vector3[] newVectices;

    Vector3[] llantasVertices1;
    Vector3[] llantas_newVertices1;

    Vector3[] llantasVertices2;
    Vector3[] llantas_newVertices2;

    Vector3[] llantasVertices3;
    Vector3[] llantas_newVertices3;

    Vector3[] llantasVertices4;
    Vector3[] llantas_newVertices4;


    void Start()
    {
        
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        
        newVectices = new Vector3[baseVertices.Length];
        for (int i = 0; i < baseVertices.Length; i++)
        {
            newVectices[i] = baseVertices[i];
        }

        generateLlantas();

    }


    void Update()
    {
       DoTransform();
    }


    void DoTransform()
    {
        //Carro
        Matrix4x4 move = HW_Transforms.TranslationMat(displacement.x * Time.time, displacement.y * Time.time, displacement.z * Time.time);
        Matrix4x4 rotate = HW_Transforms.RotateMat(angle * Time.time, rotationAxis);
        Matrix4x4 composite =  rotate * move ;

        //Llantas
        Matrix4x4 roll = HW_Transforms.RotateMat(angle * Time.time, AXIS.X) ;

        Matrix4x4 pos_1 = HW_Transforms.TranslationMat(llantasPos[0].x,llantasPos[0].y ,llantasPos[0].z );
        Matrix4x4 pos_2 = HW_Transforms.TranslationMat(llantasPos[1].x,llantasPos[1].y ,llantasPos[1].z );
        Matrix4x4 pos_3 = HW_Transforms.TranslationMat(llantasPos[2].x,llantasPos[2].y ,llantasPos[2].z );
        Matrix4x4 pos_4 = HW_Transforms.TranslationMat(llantasPos[3].x,llantasPos[3].y ,llantasPos[3].z );

        //(Recuerda que importa el orden)
        Matrix4x4 composite_1 =  composite * pos_1 * roll;
        Matrix4x4 composite_2 =  composite * pos_2 * roll;
        Matrix4x4 composite_3 =  composite * pos_3 * roll;
        Matrix4x4 composite_4 =  composite * pos_4 * roll;
    

        for (int i = 0; i < newVectices.Length; i++)
        {
            Vector4 temp= new Vector4(baseVertices[i].x, baseVertices[i].y, baseVertices[i].z, 1);
            
            newVectices[i] = composite * temp;
        }

        for (int i = 0; i < llantas_newVertices1.Length; i++)
        {
            Vector4 temp1= new Vector4(llantasVertices1[i].x, llantasVertices1[i].y, llantasVertices1[i].z, 1);

            llantas_newVertices1[i] = composite_1 * temp1;
            llantas_newVertices2[i] = composite_2 * temp1;
            llantas_newVertices3[i] = composite_3 * temp1;
            llantas_newVertices4[i] = composite_4 * temp1;

            
        }

        mesh.vertices = newVectices;
        mesh.RecalculateNormals();

        llantaMesh1.vertices = llantas_newVertices1;
        llantaMesh1.RecalculateNormals();

        llantaMesh2.vertices = llantas_newVertices2;
        llantaMesh2.RecalculateNormals();

        llantaMesh3.vertices = llantas_newVertices3;
        llantaMesh3.RecalculateNormals();

        llantaMesh4.vertices = llantas_newVertices4;
        llantaMesh4.RecalculateNormals();
                                                    
    }  

    void generateLlantas()
    {
        Vector3 posicion_original = new Vector3(0, 0, 0);
        for (int i = 0; i < llantasPos.Length; i++)
        {
            GameObject llantaTemp = Instantiate(llanta, posicion_original, Quaternion.identity);  

            if (i == 0)
            {
                llantaMesh1 = llantaTemp.GetComponentInChildren<MeshFilter>().mesh;
                llantasVertices1 = llantaMesh1.vertices;
                llantas_newVertices1 = new Vector3[llantasVertices1.Length];
                for (int j = 0; j < llantasVertices1.Length; j++)
                {
                    llantas_newVertices1[j] = llantasVertices1[j];
                }
            }

            else if (i == 1)
            {
                llantaMesh2 = llantaTemp.GetComponentInChildren<MeshFilter>().mesh;
                llantasVertices2 = llantaMesh2.vertices;
                llantas_newVertices2 = new Vector3[llantasVertices2.Length];
                for (int j = 0; j < llantasVertices2.Length; j++)
                {
                    llantas_newVertices2[j] = llantasVertices2[j];
                }
            }

            else if (i == 2)
            {
                llantaMesh3 = llantaTemp.GetComponentInChildren<MeshFilter>().mesh;
                llantasVertices3 = llantaMesh3.vertices;
                llantas_newVertices3 = new Vector3[llantasVertices3.Length];
                for (int j = 0; j < llantasVertices3.Length; j++)
                {
                    llantas_newVertices3[j] = llantasVertices3[j];
                }
            }

            else if (i == 3)
            {
                llantaMesh4 = llantaTemp.GetComponentInChildren<MeshFilter>().mesh;
                llantasVertices4 = llantaMesh4.vertices;
                llantas_newVertices4 = new Vector3[llantasVertices4.Length];
                for (int j = 0; j < llantasVertices4.Length; j++)
                {
                    llantas_newVertices4[j] = llantasVertices4[j];
                }
            }
        }
    }


}
