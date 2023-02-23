using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputScript : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    LineRenderer lineRenderer;

    public void Start()
    {
        toggleReference.action.started += Toggle;
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.Log(hit.transform.position);
            Debug.Log(hit.transform.name);
        }
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if(hit.transform.gameObject.layer == 0)
                lineRenderer.SetPosition(1, new Vector3(0, 0, Vector3.Distance(transform.position, new Vector3(0, 0, hit.transform.position.z))));

            if(hit.transform.gameObject.layer == 5)
                lineRenderer.SetPosition(1, new Vector3(0, 0, Vector3.Distance(transform.position, new Vector3(0, 0, hit.transform.position.z + 0.35f))));
        }
        else
        {
            lineRenderer.SetPosition(1, new Vector3(0, 0, 10));
        }
    }
}