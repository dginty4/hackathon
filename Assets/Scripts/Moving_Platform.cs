using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour {
    public float speed; // speed of the platform 
    public int startingPoint; // starting index (position of platform)
    public Transform[] points; // An array of transform poins (positions where the platform will move to)

    private int i; // index of the array 
    // Start is called before the first frame update
    void Start() {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update() {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f) {
            i++;
            if (i == points.Length) {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    // When the player lands on the moving platform, make platform the parent of player
    // so that the player moves with it 
    private void OnCollisionEnter2D(Collision2D collision) {
        collision.transform.SetParent(transform);
    }

    // When the player gets off platform, remove parent relationship 
    private void OnCollisionExit2D(Collision2D collision) {
        collision.transform.SetParent(null);
    }
}
