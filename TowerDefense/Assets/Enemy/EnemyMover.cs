using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(EnemyMove());
    }

    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            path.Add(child.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator EnemyMove()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPoint = transform.position;
            Vector3 endPoint = waypoint.transform.position;
            float travelPercent = 0f;
            transform.LookAt(endPoint);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPoint, endPoint, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        gameObject.SetActive(false);
    }
}
