using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

  public GameObject followedObject;
    
	void FixedUpdate ()
  {
	  if (followedObject != null)
    {
      transform.position = new Vector3(followedObject.transform.position.x, followedObject.transform.position.y, transform.position.z);
    }
	}
}
