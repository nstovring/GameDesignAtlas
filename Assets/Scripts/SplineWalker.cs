using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{


    public BezierSpline spline;
    public SplineWalkerMode mode;

    public float duration;
    public bool lookForward;
    

    private bool goingForward = true;
    public float progress;
    public float HSpeed;

    private void Update()
    {

        HSpeed = Input.GetAxis("Horizontal");

        if (goingForward)
        {
            progress += Time.deltaTime * HSpeed ;
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }




    }
}
