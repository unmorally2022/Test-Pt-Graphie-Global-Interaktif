using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -Vector3.forward * speed * Time.deltaTime;
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log(string.Format("{0} {1}", "Road exit Collide", collision.collider.tag));

        if (collision.collider.tag == "Back Limit Pos")
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
