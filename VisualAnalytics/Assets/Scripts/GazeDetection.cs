using UnityEngine;
using System.Collections;

public class GazeData {
    public GazeData() { }

    public string playerID = " ";
    public string objectID = " ";
    public string time = " ";
    public string timeEnter = " ";
    public string timeExit = " ";
    public string userPosition = " ";
    public string userDirection = " ";
    public string objectPoint = " ";
    public string objectDistance = " ";
}

public class GazeDetection : MonoBehaviour {

    public NetworkManager network;

    GazeData data;
    GameObject ROI;
    RaycastHit[] hits;
    float closest = 500f;
    RaycastHit closestRaycastHit;
    GameObject previousROI;
    IEnumerator gazeRecording;

    void Start() {
        data = new GazeData();
        gazeRecording = recordGaze();
        StartCoroutine(gazeRecording);
    }

    IEnumerator recordGaze() {

        while (true) {

            // reset values
            ROI = null;
            closest = 500f;
            data.objectID = " ";
            data.time = System.DateTime.Now.ToString();
            data.objectPoint = " ";
            data.userPosition = transform.position.ToString();
            data.userDirection = transform.forward.ToString();
            data.objectDistance = " ";

            // find what the user can see in one direction
            hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

            foreach (RaycastHit hit in hits) {
                // find the closest object in view which is ROI tagged
                if (hit.transform.gameObject.GetComponent<ObjectOfInterest>() != null && hit.distance < closest) {
                    closest = hit.distance;
                    ROI = hit.transform.gameObject;
                    closestRaycastHit = hit;
                }
            }

            // was an ROI found?
            if (ROI != null) {
                // just started looking at ROI
                if (ROI.GetComponent<ObjectOfInterest>().getObserved() == false) {
                    data.timeEnter = System.DateTime.Now.ToString();
                    ROI.GetComponent<ObjectOfInterest>().setObserved(true);
                } else {
                    // fixated on ROI
                    data.objectPoint = closestRaycastHit.point.ToString();
                    data.objectDistance = closestRaycastHit.distance.ToString();

                }
                data.timeExit = System.DateTime.Now.ToString();
                data.objectID = ROI.GetComponent<ObjectOfInterest>().getObjectID();
            } else if (previousROI != null) {
                // stopped looking at the ROI
                previousROI.GetComponent<ObjectOfInterest>().setObserved(false);
                data.timeEnter = " ";
                data.timeExit = " ";
            }

            //  Debug.Log("userID " + userID + " objectID " + objectID + " time " + time + " objectEnter " + objectEnter + " objectExit " + objectExit + " position " + position + " boundaryHit " + direction + " objectHit " + objectHit + " distance " + distance);
            network.GetComponent<NetworkManager>().Upload(data);
            previousROI = ROI;
            yield return new WaitForSeconds(1f);
        }
    }
}


