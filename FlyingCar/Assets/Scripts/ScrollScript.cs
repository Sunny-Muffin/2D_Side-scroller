using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(1, 0);
    public Vector2 direction = new Vector2(-1, 0);

    public bool isLinkedToCamera = false;
    public bool isLooping = false;

    private List<Transform> backgroundPart;
    public Vector2 repeatableSize;

    // Start is called before the first frame update
    void Start()
    {
        if (isLooping)
        {
            backgroundPart = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.GetComponent<Renderer>() != null)
                {
                    backgroundPart.Add(child);
                }
            }

            if (backgroundPart.Count == 0)
            {
                Debug.Log("Объект пустой");
            }

            backgroundPart = backgroundPart.OrderBy(t => t.position.x * (-1 * direction.x)).ThenBy((t => t.position.y * (-1 * direction.y))).ToList();

            var first = backgroundPart.First();
            var last = backgroundPart.Last();

            repeatableSize = new Vector2(Mathf.Abs(last.position.x - first.position.x), Mathf.Abs(last.position.y - first.position.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        if(isLooping)
        {
            var dist = (transform.position - Camera.main.transform.position).z;

            float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            float width = Mathf.Abs(rightBorder - leftBorder);

            float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
            float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
            float height = Mathf.Abs(topBorder - bottomBorder);

            Vector3 exitBorder = Vector3.zero;
            Vector3 entryBorder = Vector3.zero;

            if (direction.x < 0)
            {
                exitBorder.x = leftBorder;
                entryBorder.x = rightBorder;
            }
            if (direction.x > 0)
            {
                exitBorder.x = rightBorder;
                entryBorder.x = leftBorder;
            }

            if (direction.y < 0)
            {
                exitBorder.y = bottomBorder;
                entryBorder.y = topBorder;
            }
            if (direction.y > 0)
            {
                exitBorder.y = topBorder;
                entryBorder.y = bottomBorder;
            }

            Transform firstChild = backgroundPart.FirstOrDefault();
            if (firstChild != null)
            {
                bool checkVisible = false;

                if (direction.x != 0)
                {
                    if ((direction.x < 0 && (firstChild.position.x < exitBorder.x)) || (direction.x > 0 && (firstChild.position.x > exitBorder.x)))
                    {
                        checkVisible = true;
                    }
                }

                if (direction.y != 0)
                {
                    if ((direction.y < 0 && (firstChild.position.y < exitBorder.y)) || (direction.y > 0 && (firstChild.position.y > exitBorder.y)))
                    {
                        checkVisible = true;
                    }
                }

                if(checkVisible)
                {
                    if(firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
                    {
                        firstChild.position = new Vector3(
                            firstChild.position.x + ((repeatableSize.x + firstChild.GetComponent<Renderer>().bounds.size.x) * -1 * direction.x),
                            firstChild.position.y + ((repeatableSize.y + firstChild.GetComponent<Renderer>().bounds.size.y) * -1 * direction.y),
                            firstChild.position.z);

                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }
}
