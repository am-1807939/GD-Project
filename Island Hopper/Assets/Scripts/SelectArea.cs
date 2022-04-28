using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArea : MonoBehaviour
{

	public GameObject curseball;


    public LayerMask terrainLayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Summon a curseball 
            Vector3 direction = ray.direction;
		    GameObject createdCurseball = Instantiate(curseball, ray.origin, transform.rotation);
		    createdCurseball.GetComponent<Rigidbody>().velocity = direction * 20f;

             if (Physics.Raycast(transform.position, ray.direction, out hit, Mathf.Infinity, terrainLayer )) {
                // position to attack
                transform.position = hit.point;
             }
         }
    }
}
