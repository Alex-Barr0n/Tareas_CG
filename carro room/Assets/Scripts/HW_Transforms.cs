/*
Functions to work with transformation matrices in 3D

Gilberto Echeverria
2022-11-03
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeration to define the axis
public enum AXIS {X, Y, Z};
// Values:        0  1  2

public class HW_Transforms : MonoBehaviour
{
    public static Matrix4x4 TranslationMat(float tx, float ty, float tz)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = tx;
        matrix[1, 3] = ty;
        matrix[2, 3] = tz;
        return matrix;
    }

    public static Matrix4x4 ScaleMat(float sx, float sy, float sz)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = sx;
        matrix[1, 1] = sy;
        matrix[2, 2] = sz;
        return matrix;
    }

    public static Matrix4x4 RotateMat(float angle, AXIS axis)
    {
        // Convert the angle to radians
        float rads = angle * Mathf.Deg2Rad;
        Mathf.Sin(rads);

        // Create the matrix
        Matrix4x4 matrix = Matrix4x4.identity;

        // According to WHAT we are going to rotate
        if (axis == AXIS.X) {
            matrix[1,1] = Mathf.Cos(rads);
            matrix[1,2] = -Mathf.Sin(rads);
            matrix[2,1] = Mathf.Sin(rads);
            matrix[2,2] = Mathf.Cos(rads);
        } 
        
        else if (axis == AXIS.Y) {
            matrix[0,0] = Mathf.Cos(rads);
            matrix[0,2] = Mathf.Sin(rads);
            matrix[2,0] = -Mathf.Sin(rads);
            matrix[2,2] = Mathf.Cos(rads);
        } 
        
        else if (axis == AXIS.Z) {
            matrix[0,0] = Mathf.Cos(rads);
            matrix[0,1] = -Mathf.Sin(rads);
            matrix[1,0] = Mathf.Sin(rads);
            matrix[1,1] = Mathf.Cos(rads);
        }

        return matrix;
    }
}
