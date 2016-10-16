using UnityEngine;
using System.Collections;

public class ObjectOfInterest : MonoBehaviour {

   public string objectID;
   public bool observed;

    public bool getObserved() {
        return observed;
    }

    public void setObserved(bool boolean) {
        observed = boolean;
    }

    public string getObjectID() {
        return objectID;
    }
    public void setObjectID(string ID) {
        objectID = ID;
    }
}
