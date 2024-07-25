using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    //Reference to waypoints
    public List<Transform> points;
    public float speed = 2;
    
    private int nextID=0;
    private int idChangeValue = 1;

    private void Update()
    {
        //Get the next Point transform
        Transform goalPoint = points[nextID];
        
        //Flip the enemy transform to look into the point's direction
        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(3, 3, 3);
        else
            transform.localScale = new Vector3(-3, 3, 3);
        
        //Move the enemy towards the goal point
        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position,speed*Time.deltaTime);
        
        //Check the distance between enemy and goal point to trigger next point
        if(Vector2.Distance(transform.position, goalPoint.position)<1f)
        {
            //Check if we are at the end of the line (make the change -1)
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            //Check if we are at the start of the line (make the change +1)
            if (nextID == 0)
                idChangeValue = 1;
            //Apply the change on the nextID
            nextID += idChangeValue;
        }
    }
}
