using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private Transform sweepTransform;
    public float rotationSpeed;
    public float radarDistance;
    public LayerMask TerrainLayer;
    public Ray theRay;

    //On game start
    private void Awake()
    {
        //finds the RadarSweep objects in the editor
        sweepTransform = transform.Find("RadarSweep");
        rotationSpeed = 180f;
        radarDistance = 180f;

    }

    //updates every frame
    private void Update()
    {
        //finds RadarTexture child of the Radar gameobject
        var RT = GameObject.Find("RadarTexture");
        
        //Uses RadarTexture to get the CanvasScript attached to it
        CanvasScript CSc = RT.GetComponent<CanvasScript>();

        //makes arrow spin clockwise, deltaTime makes it not dependant on FPS
        sweepTransform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);

        for (int i = 0; i < 45; i++)
        {
            //creates the raycasts
            theRay = new Ray(sweepTransform.position, sweepTransform.TransformDirection(Quaternion.Euler(0,0, i) * transform.right * radarDistance));
            Debug.DrawRay(sweepTransform.position, sweepTransform.TransformDirection((Quaternion.Euler(0, 0, i)* transform.right) * radarDistance));

            /*Ray theRayUp = new Ray(sweepTransform.position, sweepTransform.TransformDirection(Quaternion.Euler(0, 10, i) * transform.right * radarDistance));
            Debug.DrawRay(sweepTransform.position, sweepTransform.TransformDirection((Quaternion.Euler(0, 10, i) * transform.right) * radarDistance));

            Ray theRayDown = new Ray(sweepTransform.position, sweepTransform.TransformDirection(Quaternion.Euler(0, -10, i) * transform.right * radarDistance));
            Debug.DrawRay(sweepTransform.position, sweepTransform.TransformDirection((Quaternion.Euler(0, -10, i) * transform.right) * radarDistance));*/


            //if the raycast hits something
            if (Physics.Raycast(theRay, out RaycastHit hit, radarDistance))
            {
                //if it has the tag assigned, can be anything as long as you set it here and in the inspector
                if (hit.collider.tag == "Boat")
                {
                    print("You found a boat!");
                    //Debug.Log(hit.point);
                    //adds a value to the hitpoints list in CanvasScript
                    CSc.hitPoints.Add(new Vector2(hit.point.x, hit.point.z));

                }
            }

        }
    }
}