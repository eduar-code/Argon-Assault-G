using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [Header("General Setup setting")]
    [SerializeField] InputAction movement;
    [Tooltip("How fast ship moves up and down")]
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 8f;
    [SerializeField] float yRange = 7f;

    [Header("Screen posotion based tuning")]
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float positionPitchFactor = -2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRowFactor = -20f;

    [Header("Laser gun array")]
    [Tooltip("Add all player  lasers here")]
    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    void Start()
    {

    }

    /*void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }*/

    void Update()
    {
        //float xThrow = movement.ReadValue<Vector2>().x; //new version
        //float yThrow = movement.ReadValue<Vector2>().y; // new version
        ProccesTraslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRowFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProccesTraslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(
            clampedXPos,
            clampedYPos,
            transform.localPosition.z
        );
    }

    void ProcessFiring()
    {

        if (Input.GetButton("Fire1"))
        {
            setLaserActive(true);
        }
        else
        {
            setLaserActive(false);
        }

    }

    void setLaserActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emisionModule = laser.GetComponent<ParticleSystem>().emission;
            emisionModule.enabled = isActive;
        }
    }




}
