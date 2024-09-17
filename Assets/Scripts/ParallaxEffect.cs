using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //Starting position for the parallax game object
    Vector2 startingPosition;

    //Start Z value of the parallax game object
    float startingZ;

    //Distance that the camera has moved from the starting postion of parallax object
    Vector2 CamMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float ZDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float ClippingPlane => (cam.transform.position.z + (ZDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));


    // The Further the object from the player, the faster the ParallaxEffect object will move. Drag it's Z value closer to the target to make it slower
    float ParallaxFactor => Mathf.Abs(ZDistanceFromTarget) / ClippingPlane;
        
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + CamMoveSinceStart * ParallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
