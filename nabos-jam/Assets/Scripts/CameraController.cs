using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles Camera movement and unit following
/// </summary>
public class CameraController : Singleton<CameraController>
{
    [SerializeField]
    Vector3 offset = new Vector3(-3f, 15f, -8f);
    [SerializeField]
    float maxZoom;
    [SerializeField]
    float minZoom;
    float _zoomAmount = 0;
    public float zoomSpeed;
    public GameObject cam;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.PlayerSquad.ActiveUnits != null)
        {
            List<Unit> active_units = GameManager.Instance.PlayerSquad.ActiveUnits;
            if (active_units.Count > 0) 
            {
                GameObject leader = active_units[0].gameObject;
                Vector3 deltaScroll = new Vector3(0f, _zoomAmount * zoomSpeed , - _zoomAmount/2 * zoomSpeed);
                
                _zoomAmount = 0;
                offset += deltaScroll;
                if (offset.y < minZoom)
                {
                    offset.y = minZoom;
                    offset.z = -minZoom / 2;
                }
                if (offset.y > maxZoom)
                {
                    offset.y = maxZoom;
                    offset.z = -maxZoom / 2;
                }
                cam.transform.position = leader.transform.position + offset;
            }
        }
    }

    /// <summary>
    /// Adds a new zoom value 
    /// </summary>
    /// <param name="zoomAmount">Zoom value</param>
    public void UpdateZoom(float zoomAmount)
    {
        _zoomAmount = zoomAmount;
    }
}
