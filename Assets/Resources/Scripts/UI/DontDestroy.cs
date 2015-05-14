using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

    static DontDestroy instance;

    //When the object awakens, we assign the static variable if its a new instance and
    void Awake()
    {
        //destroy the already existing instance, if any
        if (instance)
        {
            Destroy(instance);                                      
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);                                
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
