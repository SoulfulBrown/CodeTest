
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARCakePlacer : MonoBehaviour
{

    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    public GameObject CakeObjectPrefab;

    [SerializeField]
    [Tooltip("The instance of the cake in the scene")]
    GameObject m_PlacedPrefab;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();


    ARRaycastManager m_RaycastManager;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {


#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {

            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    void Update()
    {

        //check to see if the mouse has been clicked that frame
        //if false terminates, if true updates touch position to last mouse place 
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;


        //creates a raycast
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out hit, 500))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100, true);
            Debug.Log(hit.point);

        }

        //if the object has already been placed, dont call
        if (m_PlacedPrefab == null && hit.point != null)
        {
            m_PlacedPrefab = Instantiate(CakeObjectPrefab, hit.point, Quaternion.identity);
        }
        else
        {   // updates the position if a new point has been made
            m_PlacedPrefab.transform.position = hit.point;
        }

    }
}
