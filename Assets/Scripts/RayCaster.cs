using UnityEngine;
using UnityEngine.InputSystem;

public class RayCaster : MonoBehaviour
{

    public Camera camera;

    public GameObject oldTarget;

    void Awake()
    {
        // Ensure the camera is assigned
        if (camera == null)
        {
            camera = GetComponent<Camera>();
            if (camera == null)
            {
                Debug.LogError("No camera found. Please assign a camera to the RayCaster script.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        if ( Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            GameObject hitObject = objectHit.gameObject;
            if(oldTarget != null && oldTarget != hitObject){
                Material mat = oldTarget.GetComponent<Renderer>().material;
                mat.DisableKeyword("_EMISSION");            
            }

            if(hitObject.CompareTag("Interactable")){
                // Perform any action with the hit object
                Material mat = hitObject.GetComponent<Renderer>().material;
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.darkRed);

                if(Mouse.current.leftButton.wasPressedThisFrame) {
                    // Perform any action with the hit object
                    Destroy(hitObject);
                    hitObject = null;
                }  
            }
            oldTarget = hitObject;

            // Destroy(hitObject);
            
            // Do something with the object that was hit by the raycast.
        } else{ 
            if(oldTarget != null){
                Material mat = oldTarget.GetComponent<Renderer>().material;
                mat.DisableKeyword("_EMISSION");
            }
            oldTarget = null;
        }
    }

}
