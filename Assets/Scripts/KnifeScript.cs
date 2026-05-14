using NUnit.Framework;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    private Camera mainCamera;
    private Collider knifeCollider;
    private bool isSlicing;
    private TrailRenderer knifeTrail;

    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private void Awake()
    {
        mainCamera = Camera.main;
        knifeCollider = GetComponent<Collider>();
        knifeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (isSlicing)
        {
            continueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        isSlicing = true;
        knifeCollider.enabled = true;

        knifeTrail.enabled = true;
        knifeTrail.Clear();
    }

    private void StopSlicing()
    {
        isSlicing = false;
        knifeCollider.enabled = false;

        knifeTrail.enabled = false;
    }

    private void continueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;
        float velocity = direction.magnitude / Time.deltaTime;

        knifeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
}