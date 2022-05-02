using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArea : MonoBehaviour
{

    public LayerMask terrainLayer;
    
    public RaycastHit hit;
    public bool mode3 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode3) {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

             if (Physics.Raycast(transform.position, ray.direction, out hit, Mathf.Infinity, terrainLayer )) {
                // position to attack
                transform.position = hit.point;
             }
         }
    }
}
