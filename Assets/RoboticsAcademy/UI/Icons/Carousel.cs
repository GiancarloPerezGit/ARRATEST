using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Transform> list;
    public ArrayList carousel;
    private int left = 0;
    private int right = 3;
    void Start()
    {
        carousel = new ArrayList();
        int childCount = gameObject.transform.childCount;
        for (int y = 0; y < childCount; y++)
        {
            carousel.Insert(y, gameObject.transform.GetChild(y).gameObject);
        }
    }
    public void moveLeft()
    {
        ((GameObject)carousel[right]).SetActive(false);
        if (left == 0)
        {
            carousel.Insert(0, carousel[carousel.Count - 1]);
            carousel.RemoveAt(carousel.Count - 1);
            ((GameObject)carousel[0]).transform.SetAsFirstSibling();
            ((GameObject)carousel[0]).SetActive(true);
            print("Moved");
        }
        else
        {
            ((GameObject)carousel[left - 1]).SetActive(true);
            left -= 1;
            right -= 1;
        }


    }
    public void moveRight()
    {
        ((GameObject)carousel[left]).SetActive(false);
        if (right == (carousel.Count - 1))
        {
            carousel.Insert(carousel.Count, carousel[0]);
            carousel.RemoveAt(0);
            ((GameObject)carousel[carousel.Count - 1]).transform.SetAsLastSibling();
            ((GameObject)carousel[carousel.Count - 1]).SetActive(true);
            print("Moved");
        }
        else
        {
            ((GameObject)carousel[right + 1]).SetActive(true);

            left += 1;
            right += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
