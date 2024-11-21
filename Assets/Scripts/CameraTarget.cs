using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTarget : MonoBehaviour
{
    public static CameraTarget Instance;

    [SerializeField] float speed;

    Transform cameraTransform;
    Coroutine lookAtCoroutine;

    bool isBlocked;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlocked)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!Input.GetMouseButton(0))
            return;

        //if (lookAtCoroutine != null)
        //    StopCoroutine(lookAtCoroutine);

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 movementVector = new Vector3(-x, 0f, -y);

        Vector3 faceDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
        float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);
        var cameraRelativeEulers = Quaternion.Euler(0, cameraAngle, 0);

        transform.Translate(cameraRelativeEulers * movementVector * speed * Time.deltaTime);
    }

    public void LookAt(Vector3 position)
    {
        isBlocked = true;

        if (lookAtCoroutine != null)
            StopCoroutine(lookAtCoroutine);

        position.y = 0f;

        lookAtCoroutine = StartCoroutine(LerpToPoint(position));
    }

    private IEnumerator LerpToPoint(Vector3 position)
    { 
        while (Vector3.Distance(transform.position, position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, position, 6f * Time.deltaTime);
            yield return null;
        }

        isBlocked = false;
    }
}
