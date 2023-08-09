using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	//Velocidade de movimento da camera
	public float velocity = 1;
	
	void Update () {
		//Movimento do mouse em x e y
		float movex = Input.GetAxis("Mouse X");
		float movey = Input.GetAxis("Mouse Y");

		//Rotaciona a camera em relação ao movimento do mouse
		transform.Rotate(new Vector3(-movey * velocity, movex * velocity, 0));
		//Ajusta a rotação do objeto no eixo z
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
	}
}
