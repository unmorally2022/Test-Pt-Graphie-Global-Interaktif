using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    GameObject RoadPrefab;
    [SerializeField]
    GameObject RoadParent;
    List<GameObject> RoadObjects;

    [SerializeField]
    GameObject[] CarPrefabs;
    [SerializeField]
    GameObject CarParent;
    List<GameObject> CarObjects;

    [SerializeField]
    GameObject MovingObject;

    [SerializeField]
    GameObject FrontLimit;

    [SerializeField]
    GameObject[] CarEnemyPos;

    [SerializeField]
    TMPro.TMP_Text ScoreText, LiveText;

    [SerializeField]
    PlayerController playerController;

    private float TimeCarCounter;
    private int TimeCarCounterNext;


    // Start is called before the first frame update
    void Start()
    {
        CreateRoad();
        CreateCar();
        refreshStat();
    }

    private void FixedUpdate()
    {        

        MovingObject.transform.position += Vector3.forward * 5 * Time.deltaTime;

        TimeCarCounter += Time.deltaTime;
        Debug.Log(TimeCarCounter);
        if (TimeCarCounter > TimeCarCounterNext)
        {
            for (int i = 0; i < CarObjects.Count; i++) {
                if (CarObjects[i].activeSelf == false) {
                    CarObjects[i].transform.position = CarEnemyPos[Random.Range(0,4)].transform.position;
                    CarObjects[i].SetActive(true);
                    break;
                }
            }
            TimeCarCounterNext = Random.Range(2, 5);
            TimeCarCounter = 0;
        }
    }

    public void refreshStat() {
        ScoreText.text = "Score : " + playerController.Score.ToString();
        LiveText.text = "Live : " + playerController.Live.ToString();

    }

    void CreateRoad() {
        RoadObjects = new List<GameObject>();
        for (int i = 0; i < 21; i++) {
            GameObject _road = Instantiate(RoadPrefab, RoadParent.transform);
            _road.transform.position = new Vector3(_road.transform.position.x, _road.transform.position.y, _road.transform.position.z + (-10+i));
            RoadObjects.Add(_road);
        }
    }

    void CreateCar() {
        TimeCarCounterNext = Random.Range(2, 5);
        CarObjects = new List<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            GameObject _car = Instantiate(CarPrefabs[Random.Range(0,2)], CarParent.transform);
            //_car.transform.position = new Vector3(_car.transform.position.x, _car.transform.position.y, _car.transform.position.z + (-10 + i));
            _car.SetActive(false);
            CarObjects.Add(_car);
        }
    }

    public void MoveRoad(GameObject _road) {
        //GameObject _road = Instantiate(RoadPrefab, RoadParent.transform);
        _road.transform.position = FrontLimit.transform.position;
    }
}
