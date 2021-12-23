using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;       
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += Select;
    }

    private void OnDisable()
    {
        mouseClick.performed -= Select;
        mouseClick.Disable();
    }

    private void Select(InputAction.CallbackContext context)
    {
       
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            if (hit.collider != null && hit.collider.gameObject.name == gameObject.name)
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        
        while (mouseClick.ReadValue<float>() != 0)
        {
            transform.position = MouseWorldPosition();
            yield return null;
        }
        
    }

}
