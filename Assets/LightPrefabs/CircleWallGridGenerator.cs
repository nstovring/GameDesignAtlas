using UnityEngine;
using System.Collections;



[ExecuteInEditMode] 
public class CircleWallGridGenerator : MonoBehaviour
{
    public float numObjects;
    public float radius;
    public int rows;
    public int columns;
    
    public float heightOffset;
    public float DepthOffset;

    public GameObject prefab;

    public bool halfOrNot = false;
    public float division;


    private float currentCircleOffset;
    private float CircleOffsetForEachObject;

    private float halfCircleLimit;
    private float radiusOriginal;
    private Vector3 center;

    private bool test;
    private int rowOffset;

    void Start()
    {

        rowOffset = 0;
        initialization();

        center = this.transform.position;
        radiusOriginal = radius;

        //print(halfCircleLimit);
        //print(CircleOffsetForEachObject);

        for (int b = 0; b < columns; b++)
        {
            test = true;


            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < numObjects; i++)
                {
              
                    Vector3 pos = RandomCircle(center, radius, currentCircleOffset);
                    GameObject shelf = Instantiate(prefab, pos, Quaternion.LookRotation(center - pos, Vector3.up));
                    shelf.transform.parent = this.transform;

                    if (j == 0 && i == 0 && halfOrNot == true)
                    {
                        DestroyImmediate(shelf);
                    }

                    if (halfOrNot == true && currentCircleOffset >= halfCircleLimit) {

                        currentCircleOffset = 0;
                        i = (int) numObjects;
                    }
                   
                    currentCircleOffset += CircleOffsetForEachObject;



                }

                rowOffset += 1;

                if (rowOffset == rows && halfOrNot == true) {
                    numObjects += 1;
                    initialization();
                    print(test);
                }
                radius += DepthOffset;
            }

            center.y += heightOffset;
            radius = radiusOriginal;

            if (halfOrNot == true) {
                rowOffset = 0;
                numObjects -= 1;
                initialization();
            }

        }
    }


    private void initialization() {
        currentCircleOffset = 0;
        CircleOffsetForEachObject = 360 / numObjects;
        halfCircleLimit = (numObjects / division) * CircleOffsetForEachObject;
    }



    Vector3 RandomCircle (Vector3 center, float radius, float ang) {
        Vector3 pos; 
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }


}
