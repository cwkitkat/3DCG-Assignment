using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit {

	protected virtual Vector3 PlayerLocation(){
		return gameObject.transform.position;
	}
}
