using UnityEngine;
using System.Collections;

public class BoundaryScript : MonoBehaviour {

    static BoundaryScript instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	

    public static BoundaryScript getInstance() {
        return instance;
    }
}
