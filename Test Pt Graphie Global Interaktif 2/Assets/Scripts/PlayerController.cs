using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Live;
    public float Score;
    [SerializeField]
    private int PosIndex;
    [SerializeField]
    private GameObject[] Poses;

    [SerializeField]
    GamePlayManager gamePlayManager;

    private void Awake()
    {
        AppManager.playerController = this;
        Score = 0;
        Live = 3;
        PosIndex = 2;

        transform.position = Poses[PosIndex].transform.position;
    }

    private void FixedUpdate()
    {
        Score += 0.1f;
        gamePlayManager.refreshStat();
    }

    public void startMoving(Vector2 vector2)
    {
        Debug.Log(vector2);

        if (vector2.x > 0)
        {
            Debug.Log("move right");
            MoveRight();
        }
        else if (vector2.x < 0 )
        {
            Debug.Log("move left");
            MoveLeft();
        }
    }

    private void MoveLeft() {
        if (PosIndex > 0) {
            PosIndex -= 1;
            transform.position = Poses[PosIndex].transform.position;
        }
    }

    private void MoveRight() {
        if (PosIndex < 4)
        {
            PosIndex += 1;
            transform.position = Poses[PosIndex].transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(string.Format("{0} {1}", "player Collide", collision.collider.tag));
        if (collision.collider.tag == "EnemyCar") {
            Live -= 1;
            gamePlayManager.refreshStat();
        }
    }
}
