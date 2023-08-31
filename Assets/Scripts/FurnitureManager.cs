using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FurnitureManager : MonoBehaviour
{
    public GameObject SpawnableFurniture;

    public ARSessionOrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private List<ARRaycastHit> raycastsHit = new List<ARRaycastHit>();

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastsHit, TrackableType.PlaneWithinPolygon);

                if (collision && isButtonPressed() == false)
                {
                    var _gameobject = Instantiate(SpawnableFurniture);
                    _gameobject.transform.position = raycastsHit[0].pose.position;
                    _gameobject.transform.rotation = raycastsHit[0].pose.rotation;
                }

                foreach(var planes in planeManager.trackables)
                {
                    planes.gameObject.SetActive(false);
                }

                planeManager.enabled = false;

            }
        }
    }

    public bool isButtonPressed()
    {
        if (EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null)
        {
            return false;
        }
        else return true;
    }

    public void SwitchFurniture(GameObject furniture)
    {
        SpawnableFurniture = furniture; 
    }
}
