using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    GamePlayManager gamePlayManager;

    private void Start()
    {
        gamePlayManager = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //destroy this object
        //Debug.Log(string.Format("{0} {1}","Road Collide",collision.collider.tag));        
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log(string.Format("{0} {1}", "Road exit Collide", collision.collider.tag));
        if (collision.collider.tag == "Back Limit Pos")
        {
            gamePlayManager.MoveRoad(this.gameObject);
        }
    }
}
