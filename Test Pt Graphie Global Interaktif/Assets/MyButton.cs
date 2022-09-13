using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Button MyButtonObject;

    public string id, nama, imageURI;
    public bool showOnHome;

    [System.Obsolete]
    public void InitButton(string _id, string _nama, string _imageURI, bool _showOnHome) {
        id = _id;
        nama = _nama;
        imageURI = _imageURI;
        showOnHome = _showOnHome;
        StartCoroutine(DownloadIcon());
    }

    [System.Obsolete]
    IEnumerator DownloadIcon() {
        //MyButtonObject.image

#if UNITY_EDITOR
        var uwr = new UnityWebRequest("https://adm.gotc.app/uploads/product_categories/"+ imageURI, UnityWebRequest.kHttpVerbGET);
#elif UNITY_IOS
                                var uwr = new UnityWebRequest("https://adm.gotc.app/uploads/product_categories/"+ imageURI, UnityWebRequest.kHttpVerbGET);
#elif UNITY_ANDROID
                                var uwr = new UnityWebRequest("https://adm.gotc.app/uploads/product_categories/"+ imageURI, UnityWebRequest.kHttpVerbGET);
#endif

        string path = Path.Combine(Application.persistentDataPath, imageURI);
        uwr.downloadHandler = new DownloadHandlerFile(path, false);
        yield return uwr.SendWebRequest();


        StartCoroutine(LoadScreenshot(path));

        //callback?.Invoke();


        //if (uwr.isNetworkError || uwr.isHttpError)
        //    Debug.LogError(uwr.error);
        //else
        //    Debug.Log("File successfully downloaded and saved to " + path);


    }

    [System.Obsolete]
    static WWW LoadFileFromDisk(string filepath)
    {
        System.Uri uri = new System.Uri(filepath);
        string converted = uri.AbsoluteUri;
        return new WWW(converted);
    }

    [System.Obsolete]
    IEnumerator LoadScreenshot(string file_path)
    {
        
        var www = LoadFileFromDisk(file_path);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("Failed to fetch screenshot image: " + www.error, this);
        }
        else
        {
            MyButtonObject.GetComponent<Image>().sprite = Sprite.Create(www.textureNonReadable,
                    new Rect(0, 0, www.textureNonReadable.width, www.textureNonReadable.height),
                    new Vector2(0, 0));
        }
    }
}
