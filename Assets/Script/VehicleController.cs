using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheels Colider")]
    public WheelCollider frontRightColider;
    public WheelCollider frontLeftColider;
    public WheelCollider backRightColider;
    public WheelCollider backLeftColider;

    [Header("Wheels Transform")]
    public Transform frontRightTransform;
    public Transform frontLeftTransform;
    public Transform backRightTransform;
    public Transform backLeftTramsform;
    public Transform VehicleDoor;

    [Header("Vehicle Engine")]
    public float accelerationForce = 100f;
    public float breakingForce = 200f;
    private float presentBreakForce = 0f;
    public float presentAcceleration = 0f;

    [Header("Vehicle Steering")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("Vehicle Security")]
    public Player player;
    private float radius = 5f;
    private bool isOpen = false;

    [Header("Disable things")]
    public GameObject Aimcam;
    public GameObject AimCanvas;
    public GameObject TPSCam;
    public GameObject TPSCanvas;
    public GameObject playerCharacter;

    [Header("Vehicle Hit Var")]
    public Camera cam;
    public float hitRange = 2f;
    public float giveDmgOf = 1000f;
    public GameObject goreEffect;
    public GameObject DestroyEffect;
    //public ParticleSystem hitSpark;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                isOpen = true;
                radius = 5000f;
                ObjectiveComplete.occurence.GetObjectivesDone(true, true, true, false);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = VehicleDoor.transform.position;
                isOpen = false;
                radius = 5f;
            }

            if(isOpen == true)
            {
                TPSCam.SetActive(false);
                TPSCanvas.SetActive(false);
                Aimcam.SetActive(false);
                AimCanvas.SetActive(false);
                playerCharacter.SetActive(false);
                MoveVehicle();
                VehicleSteering();
                ApplyBreak();
                HitZombie();
            }
            else if(isOpen == false)
            {
                TPSCam.SetActive(true);
                TPSCanvas.SetActive(true);
                Aimcam.SetActive(true);
                AimCanvas.SetActive(true);
                playerCharacter.SetActive(true);
            }
        }

        
    }

    private void MoveVehicle()
    {
        presentAcceleration = accelerationForce * -Input.GetAxis("Vertical");
        presentAcceleration = Mathf.Clamp(presentAcceleration, -Mathf.Abs(accelerationForce), Mathf.Abs(accelerationForce));

        frontLeftColider.motorTorque = presentAcceleration;
        frontRightColider.motorTorque = presentAcceleration;
        backLeftColider.motorTorque = presentAcceleration;
        backRightColider.motorTorque = presentAcceleration;      
    }
    void VehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightColider.steerAngle = presentTurnAngle;
        frontLeftColider.steerAngle = presentTurnAngle;

        SteeringWheels(frontRightColider, frontRightTransform);
        SteeringWheels(frontLeftColider, frontLeftTransform);
        SteeringWheels(backRightColider, backRightTransform);
        SteeringWheels(backLeftColider, backLeftTramsform);
    }
    void SteeringWheels(WheelCollider wc, Transform wt)
    {
        Vector3 position;
        Quaternion rotation;

        wc.GetWorldPose(out position, out rotation);

        wt.position = position;
        wt.rotation = rotation;
    }
    void ApplyBreak()
    {
        //if(Input.GetKey(KeyCode.Space))
        //{
        //    presentBreakForce = breakingForce;
        //}
        //else
        //{
        //    presentBreakForce = 0f;
        //}
        float targetBreak = Input.GetKey(KeyCode.Space) ? breakingForce : 0f;
        presentBreakForce = Mathf.Clamp(targetBreak, 0f, Mathf.Abs(breakingForce));

        frontLeftColider.brakeTorque = presentBreakForce;
        frontRightColider.brakeTorque = presentBreakForce;
        backLeftColider.brakeTorque = presentBreakForce;
        backRightColider.brakeTorque = presentBreakForce;
    }

    void HitZombie()
    {

        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, hitRange))
        {
            Debug.Log(hitInfo.transform.name);  
             
            Zombie zombie = hitInfo.transform.GetComponent<Zombie>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();
            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();

            if (zombie != null)
            {
                zombie.zombieHitDamage(giveDmgOf);
                zombie.GetComponent<BoxCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDmgOf);
                zombie2 .GetComponent<BoxCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (objectToHit != null)
            {
                objectToHit.ObjectHitDmg(giveDmgOf);
                GameObject boom = Instantiate(DestroyEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(boom, 1f);
            }
        }
    }

}
