using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


[CreateAssetMenu(menuName="Params")]
public class Parameters : ScriptableObject
{
    public long fov = 10;
    public long video = 1;
    // Still in progress. Doesn't work yet.
    public long offset = 0;
    // 1: non-reg with arrows, 2: non-reg without arrows, 3: reg with arrows, 4: reg without arrows
    public long captioningMethod = 1;
    
    private Transform juror1;
    private Transform juror2;
    private Transform juror3;
    private Transform juror4;

    private string currentJuror = "juror-a";
    public float getWidth(float dist) {
        // This is assuming the camera is 5 away from the captions
        return Mathf.Tan(fov/2 * 0.0174533f) * dist * 2;
    }

    public string getCurrentJuror(){
        return currentJuror;
    }

    public void setCurrentJuror(string currentJuror){
        // juror-a/Blue juror-b/Pink juror-c/David jury-foreman/Adam
        this.currentJuror = currentJuror;
    }

    public void setJurorPositions(Transform juror1, Transform juror2, Transform juror3, Transform juror4) {
        this.juror1 = juror1;
        this.juror2 = juror2;
        this.juror3 = juror3;
        this.juror4 = juror4;
    }

    public Transform ReturnCurrentJurorTransform() {
        if(getCurrentJuror() == "juror-a") {
            return juror3;
        } else if (getCurrentJuror() == "juror-b"){
            return juror2;
        } else if(getCurrentJuror() == "juror-c") {
            return juror1;
        } else if(getCurrentJuror() == "jury-foreman") {
            return juror4;
        } else {
            throw new InvalidDataException("Not a valid juror");
        }
    }
    
    public Vector3 projectOntoSphere(float radius, Transform point){
        Transform currentJurorTransform =  ReturnCurrentJurorTransform();
        Vector3 p = new Vector3(
            currentJurorTransform.transform.position.x,
            currentJurorTransform.transform.position.y - 1.1899f,
            currentJurorTransform.transform.position.z
        );
        float magnitude = p.magnitude;
        float scalar = radius / magnitude;
        return new Vector3(
            scalar * p.x,
            scalar * p.y + 1.1899f,
            scalar * p.z
        );
    }
    public static (double r, double theta, double phi) CartesianToSpherical(double x, double y, double z)
    {
        double r = Math.Sqrt(x * x + y * y + z * z);
        double theta = Math.Acos(z / r);  // polar angle
        double phi = Math.Atan2(y, x);    // azimuthal angle
        return (r, theta, phi);
    }

    // Function to convert Spherical coordinates to Cartesian coordinates
    public static (double x, double y, double z) SphericalToCartesian(double r, double theta, double phi)
    {
        double x = r * Math.Sin(theta) * Math.Cos(phi);
        double y = r * Math.Sin(theta) * Math.Sin(phi);
        double z = r * Math.Cos(theta);
        return (x, y, z);
    }

    public static Vector3 convertOffset(double x1, double y1, double z1) {
        // Radius of the sphere
        double r = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);

        // Convert point P to spherical coordinates
        (double rSphere, double theta, double phi) = Parameters.CartesianToSpherical(x1, y1, z1);

        // Moving 10 degrees to the right: Add 10 degrees to the azimuthal angle
        double phiPrime = phi + (10 * Math.PI / 180);

        // Convert back to Cartesian coordinates
        (double x2, double y2, double z2) = Parameters.SphericalToCartesian(rSphere, theta, phiPrime);

        Debug.Log($"New coordinates: ({x2}, {y2}, {z2})");
        return new Vector3(
            (float)x2, (float)y2, (float)z2
        );
    }
}
