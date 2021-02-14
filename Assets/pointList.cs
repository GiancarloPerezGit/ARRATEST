using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Text.RegularExpressions;
public class pointList : MonoBehaviour
{
    public List<GameObject> GA = new List<GameObject>();
    public GameObject[] aa;
    public GameObject prefab;
    private int set = 0;
    private int loc = 0;
    private int inc = 0;
    public void addPoint()
    {
        GameObject g = Instantiate(prefab);
        g.transform.parent = transform;
        g.transform.localPosition = new Vector3(-278f, 80 - loc*80, 0.51f);
        g.transform.localScale = new Vector3(1.090909f, 0.1860464f, 1f);
        g.transform.localRotation = Quaternion.identity;
        g.name = "PointSwap"; 
        g.GetComponent<TextMeshProUGUI>().text = "P" + (inc * 3 + loc + 1).ToString();
        g.GetComponent<Button>().onClick.AddListener(() => pointSwap(g.GetComponent<TextMeshProUGUI>().text));
        g.GetComponent<TextMeshProUGUI>().color = Color.green;
        if (inc != set)
        {
            g.SetActive(false);
        }
        GA.Add(g);
        aa = (GameObject[])GA.ToArray();
        loc += 1;
        if(loc > 2)
        {
            loc = 0;
            inc += 1;
        }
    }
    public void removePoint()
    {
        Destroy(GA[GA.Count - 1]);
        GA.RemoveAt(GA.Count - 1);
        aa = (GameObject[])GA.ToArray();
        loc -= 1;
        if(loc < 0)
        {
            loc = 2;
            inc -= 1;
        }
    }

    public void removeMidPoint()
    {
        GA[GA.Count - 2].GetComponent<TextMeshProUGUI>().color = Color.red;
    }


    void pointSwap(string tex)
    {
        GameObject x = GameObject.FindGameObjectWithTag("Highlight");
        print("Highlight");
        if (x == null)
        {
            print("Nope");
            return;
        }
        if (x.transform.parent.GetComponent<CodeLineButtonScript>().end)
        {
            x.transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>().text = tex;
        }
        else
        {
            x.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text = tex;
        }
    }

    public void setAllRed()
    {
        for(int i = 0; i < GA.Count; i++)
        {
            GA[i].GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }

    public void setGreen(int index)
    {
        print(index);
        GA[index].GetComponent<TextMeshProUGUI>().color = Color.green;
    }

    public void resetti()
    {
        foreach(GameObject child in GA)
        {
            Destroy(child);
        }
        GA.Clear();
        Array.Clear(aa, 0, aa.Length);
        set = 0;
        loc = 0;
        inc = 0;
}

    public void upCycle()
    {
        if(set == 0)
        {

        }
        else
        {
            flipper(set);
            set -= 1;
            flipper(set);
        }
    }
    
    private void flipper(int se)
    {
        if(se*3 < aa.Length)
        {
            aa[se * 3].SetActive(!aa[se * 3].activeSelf);
        }
        if (se * 3 + 1 < aa.Length)
        {
            aa[se * 3 + 1].SetActive(!aa[se * 3 + 1].activeSelf);
        }
        if (se * 3 + 2 < aa.Length)
        {
            aa[se * 3 + 2].SetActive(!aa[se * 3 + 2].activeSelf);
        }
        
        
    }

    public void downCycle()
    {
        if (set == 99)
        {

        }
        else
        {
            flipper(set);
            set += 1;
            flipper(set);
        }
    }

   
}
