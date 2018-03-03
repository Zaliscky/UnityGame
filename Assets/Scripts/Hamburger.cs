using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamburger : MonoBehaviour {

    public float score;

   
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Burg")
        {
            score++;
            Destroy(col.gameObject);
        }

}
    void Update()
    {
        GUI.Box(new Rect(0, 0, 100, 100), " Hamburgers : " + score);
    }
}
