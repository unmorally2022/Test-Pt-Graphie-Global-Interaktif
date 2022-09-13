using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{

    [SerializeField]
    GameObject ButtonPrefab, PanelButtonPrefab;
    private List<GameObject> Buttons, PanelButtons;
    [SerializeField]
    GameObject PanelButtonsParent;
    // Start is called before the first frame update
    void Start()
    {
        Buttons = new List<GameObject>();
        PanelButtons = new List<GameObject>();

        GUI_GetHome();
    }

    public void GUI_GetHome() {
        ClearAll();
        StartCoroutine(GetRequest("https://api.gotc.app/dev/pasien/get_ctg/?tp=home"));
    }

    public void GUI_GetAll() {
        ClearAll();
        StartCoroutine(GetRequest("https://api.gotc.app/dev/pasien/get_ctg/"));
    }

    private void ClearAll() {
        for (int i = 0; i < Buttons.Count; i++) {
            Destroy(Buttons[i]);
        }
        Buttons.Clear();

        for (int i = 0; i < PanelButtons.Count; i++)
        {
            Destroy(PanelButtons[i]);
        }
        PanelButtons.Clear();
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);
            string response = www.downloadHandler.text;
            var N = JSON.Parse(response);
            //var N1 = JSON.Parse(N["category"]);

            //Debug.Log(N["category"]);
            //Debug.Log(N["category"].Count);

            int rowCount = 0;
            int ColumnCount = 0;

            GameObject _PanelButtons = Instantiate(PanelButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            _PanelButtons.transform.SetParent(PanelButtonsParent.transform);
            _PanelButtons.transform.localScale = new Vector3(1,1,1);
            PanelButtons.Add(_PanelButtons);

            for (int i = 0; i < N["category"].Count;i++) {
                GameObject _Buttons = Instantiate(ButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                _Buttons.transform.SetParent(_PanelButtons.transform);
                _Buttons.transform.localScale = new Vector3(1, 1, 1);
                Buttons.Add(_Buttons);

                bool _showOnHome = false;
                if (N["category"][i]["show_at_home"] == "Y")
                    _showOnHome = true;
                else
                    _showOnHome = false;

                _Buttons.GetComponent<MyButton>().InitButton(
                    N["category"][i]["id"],
                    N["category"][i]["nama"],
                    N["category"][i]["image"],
                    _showOnHome
                    );


                ColumnCount += 1;

                if (ColumnCount > 3) {
                    _PanelButtons = Instantiate(PanelButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    _PanelButtons.transform.SetParent(PanelButtonsParent.transform);
                    _PanelButtons.transform.localScale = new Vector3(1, 1, 1);
                    PanelButtons.Add(_PanelButtons);
                    ColumnCount = 0;
                }
                
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(PanelButtonsParent.GetComponent<RectTransform>());
            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }
}
