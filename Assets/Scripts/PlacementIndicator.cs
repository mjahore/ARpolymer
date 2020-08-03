/*
 * PlacementIndicator.cs - This updates the position of the reticle when the
 *                         application is first opened. The user can then
 *                         tap on their screen to place the polymer chain using
 *                         the location of the reticle... after which it will 
 *                         be hidden.
 *                         
 * Mike Hore (hore@case.edu), Case Western Reserve University
 * August 2020
 *
 *
 * Copyright 2020 Michael J. A. Hore
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{

    public GameObject baseParticle;

    private ARRaycastManager rayManager;
    private GameObject visual;
    private bool isPlaced = false;

    void Start ()
    {
        // get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = GameObject.Find("Reticle");

        // hide the placement indicator visual
        visual.GetComponent<Renderer>().enabled = false;
    }
    
    void Update ()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);

        // if we hit an AR plane surface, update the position and rotation
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            // enable the visual if it's disabled
            if (visual.GetComponent<Renderer>().enabled == false && isPlaced == false)
            {
                visual.GetComponent<Renderer>().enabled = true;
            }
        }

        // Check to see if the user has tapped the screen
        // to place the simulation. If they have, start the
        // simulation and hide the reticle.
        if (Input.touchCount > 0 && isPlaced == false)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (isPlaced == false)
                {
                    Instantiate(baseParticle, transform.position, transform.rotation);
                    isPlaced = true;
                    visual.GetComponent<Renderer>().enabled = false;
                }
            }
        }
       
    }
}
