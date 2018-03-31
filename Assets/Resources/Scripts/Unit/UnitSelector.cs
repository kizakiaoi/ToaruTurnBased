using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unit
{
    public class UnitSelector : Singleton<UnitSelector>
    {
        private GameObject _selectedUnit;
        public delegate void UnitSelectAction();
        public static event UnitSelectAction OnSelect;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.gameObject.GetComponent<UnitObject>())
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        _selectedUnit = hit.transform.gameObject;
                        if(OnSelect != null)
                        OnSelect();
                    }

                    else
                    {
                    }
                }
            }
        }

        public GameObject GetSelectedUnit()
        {
            return _selectedUnit;
        }
    }
}
