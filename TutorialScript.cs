using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    Text text;
    string[] tutorial = new string[] {"¡Bienvenid@!\n\nPara empezar, prueba a moverte horizontalmente con las teclas A y D.\n\nPara saltar, utiliza la tecla W.",
                                      "Este orbe que gira es un 'Checkpoint', cuando mueras, volverás al último que hayas recogido hasta que alcances otro.\n\nEl bloque rojo que hay más adelante, es un bloque de daño, si pisas uno, o te chocas con él, volverás al 'Checkpoint' y sumarás una muerte a tu seguimiento.",
                                      ""};
    int counter;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = tutorial[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Tutorial")
        {
            counter++;
            text.text = tutorial[counter]; 
        }
    }
}
