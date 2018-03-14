using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class UnitSelector : MonoBehaviour
    {
        Camera mainCamera;
        GameObject selectedUnit;

        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("fire"))
            {
                var hit = new RaycastHit();
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.gameObject.GetComponent<UnitObject>())
                    {
                        selectedUnit = hit.transform.gameObject;
                    }

                    else
                    {

                    }
                }
            }
        }
    }
}
