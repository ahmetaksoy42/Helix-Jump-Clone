using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPiecer : MonoBehaviour
{
    private int randomNumberRed;

    private int randomNumberEmpty;
    
    private int index;

    [SerializeField] List<GameObject> childs = new List<GameObject>();

    private void Awake()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        randomNumberRed = Random.Range(1, 3);

        randomNumberEmpty = Random.Range(1, 4);

        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(transform.GetChild(i).gameObject);
        }

        MakeRandomRing();
    }

    private void MakeRandomRing()
    {
        for (int j = 0; j < randomNumberRed; j++)
        {
            MakeRed();
        }

        for (int j = 0; j < randomNumberEmpty; j++)
        {
            MakeEmpty();
        }
    }

    private void MakeRed()
    {
        index = Random.Range(0, childs.Count - 1);

        GameObject c = childs[index];

        c.GetComponent<MeshRenderer>().material.color = Color.red;

        //c.GetComponent<Ring>().SetType(RingType.Red);

        c.tag = "Unsafe";
        childs.RemoveAt(index);
    }

    public void MakeEmpty()
    {
        index = Random.Range(0, childs.Count - 1);

        GameObject c = childs[index];

        c.GetComponent<MeshRenderer>().enabled = false;

        c.GetComponent<MeshCollider>().isTrigger = true;

        c.transform.position += Vector3.down * 0.20f;

        c.tag = "AddScore";

        childs.RemoveAt(index);
    }

    public void FirstRing()
    {
        MakeEmpty();
    }


    public void Breaker(int a)
    {
        for (int i = 0; transform.childCount > 0; i++)
        {
            transform.GetChild(0).GetComponent<Ring>().Break(a);
        }
        Destroy(gameObject, 0.5f);


    }

}
