using UnityEngine;
//using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]Transform[] points;
    private float moveSpeed = 2f;
    private int pointsIndex;
    public bool actCleared;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = points[pointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointsIndex <= points.Length -1)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[pointsIndex].transform.position, moveSpeed * Time.deltaTime);
            if(transform.position == points[pointsIndex].transform.position)
            {
                pointsIndex++;
            }
        }
        else
        {
            Debug.Log ("End of path");
            actCleared = true;
            //SceneManager.LoadScene("Title Sequence");
        }
    }
}
