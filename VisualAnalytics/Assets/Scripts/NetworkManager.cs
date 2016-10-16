using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;

public class NetworkManager : MonoBehaviour {



   public void Upload(GazeData data)
    {
       
        WWWForm form = new WWWForm();
        form.AddField("PlayerID", data.playerID);
        form.AddField("ObjectID", data.objectID);
        form.AddField("Time", data.time);
        form.AddField("TimeEnter", data.timeEnter);
        form.AddField("TimeExit", data.timeExit);
        form.AddField("UserPosition", data.userPosition);
        form.AddField("BoundaryPoint", data.userDirection);
        form.AddField("ObjectPoint", data.objectPoint);
        form.AddField("ObjectDistance", data.objectDistance);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8084/VisualAnalytics/VisualAnalyticsServlet", form);
        www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    public void retrieve() {
        UnityWebRequest www = new UnityWebRequest("http://localhost:8084/VisualAnalytics/VisualAnalyticsServlet");
        www.downloadHandler = new DownloadHandlerBuffer();
       
        if (www.isError) {
            Debug.Log(www.error);
        } else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
           // byte[] results = www.downloadHandler.data;
        }
    }
}
