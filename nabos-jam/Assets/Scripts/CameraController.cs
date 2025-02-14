﻿using System.Collections;
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
    float maxZoom = 25f;
    [SerializeField]
    float minZoom = 7f;
    float _zoomAmount = 0;
    float _rotateAmount = 0;
    public float zoomSpeed;
    public float rotateSpeed;
    public GameObject cam;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.PlayerParty.ActiveUnits != null)
        {
            List<Unit> active_units = GameManager.Instance.PlayerParty.ActiveUnits;
            if (active_units.Count > 0) 
            {
                GameObject leader = active_units[0].gameObject;
                Vector3 leaderPos = leader.transform.position;

                //Keeps the position relative to the leader player if the player is moving
                cam.transform.position = offset + leaderPos;

                //Performs rotation movement
                if (_rotateAmount != 0)
                {
                    cam.transform.RotateAround(leaderPos, Vector3.up, rotateSpeed * _rotateAmount * Time.fixedDeltaTime);
                }

                //Performs zoom movement
                if (_zoomAmount != 0)
                {
                    Vector3 newPos = Vector3.MoveTowards(cam.transform.position,leaderPos, - _zoomAmount * Time.fixedDeltaTime * zoomSpeed);
                    float newDistance = Vector3.Distance(newPos, leaderPos);
                    // Debug.Log(newDistance);
                    if (newDistance > minZoom && newDistance < maxZoom)
                    {
                        cam.transform.position = newPos;
                    }
                }

                //Updates the relative position to the leader player if a camera movement was done
                if (_zoomAmount != 0 || _rotateAmount != 0)
                {
                    offset = cam.transform.position - leaderPos;
                    _zoomAmount = 0;
                    _rotateAmount = 0;
                }

                //Looks directly at the leader
                cam.transform.LookAt(leaderPos, Vector3.up);
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

    /// <summary>
    /// Adds a new zoom value 
    /// </summary>
    /// <param name="zoomAmount">Zoom value</param>
    public void UpdateRotation(float rotateAmount)
    {
        _rotateAmount = rotateAmount;
    }
}
