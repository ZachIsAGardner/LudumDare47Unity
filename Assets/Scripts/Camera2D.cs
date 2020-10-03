using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera2D : MonoBehaviour
{
    public Vector3 Offset;
    public Player Player;
    public float Speed = 0.5f;

    private Bounds? bounds;
    private UnityEngine.Camera cam;

    void Start()
    {
        cam = GetComponent<UnityEngine.Camera>();
        Player = FindObjectOfType<Player>();

        GetBoundaries();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetBoundaries();
    }

    void LateUpdate()
    {
        if (Player)
        {
            float interpolation = Speed * Time.deltaTime;

            transform.position = new Vector3(
                Player.transform.position.x,
                Player.transform.position.y,
                transform.position.z
            );

            CheckBoundaries();
        }
    }

    void GetBoundaries()
    {
        GameObject boundsObject = GameObject.FindGameObjectWithTag("CameraBounds");

        if (boundsObject)
            bounds = boundsObject.GetComponent<BoxCollider2D>().bounds;
        else
            bounds = null;
    }

    void CheckBoundaries()
    {
        if (bounds == null)
            return;

        CheckHorizontal();
        CheckVertical();
    }

    void CheckHorizontal()
    {
        float camExtents = cam.orthographicSize * cam.aspect;

        if (camExtents >= bounds.Value.extents.x)
        {
            transform.position = new Vector3(
                bounds.Value.center.x + Offset.x,
                transform.position.y,
                transform.position.z
            );
        }
        else
        {
            float camLeft = transform.position.x - camExtents;
            float camRight = transform.position.x + camExtents;

            float boundsLeft = bounds.Value.center.x - bounds.Value.extents.x;
            float boundsRight = bounds.Value.center.x + bounds.Value.extents.x;

            if (camLeft < boundsLeft)
            {
                transform.position = new Vector3(
                    boundsLeft + camExtents,
                    transform.position.y,
                    transform.position.z
                );
            }
            else if (camRight > boundsRight)
            {
                transform.position = new Vector3(
                    boundsRight - camExtents,
                    transform.position.y,
                    transform.position.z
                );
            }
        }
    }

    void CheckVertical()
    {
        float camExtents = cam.orthographicSize;

        if (camExtents > bounds.Value.extents.y)
        {
            transform.position = new Vector3(
                transform.position.x,
                bounds.Value.center.y + Offset.y,
                transform.position.z
            );
        }
        else
        {
            float camBottom = transform.position.y - camExtents;
            float camTop = transform.position.y + camExtents;

            float boundsBottom = bounds.Value.center.y - bounds.Value.extents.y;
            float boundsTop = bounds.Value.center.y + bounds.Value.extents.y;

            if (camBottom < boundsBottom)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    bounds.Value.center.y - bounds.Value.extents.y + camExtents,
                    transform.position.z
                );
            }
            else if (camTop > boundsTop)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    bounds.Value.center.y + bounds.Value.extents.y - camExtents,
                    transform.position.z
                );
            }
        }
    }
}
