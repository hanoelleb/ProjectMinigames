using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField]
    int parent;

    [SerializeField]
    int val;

    BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         if (transform.position != transform.parent.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 0.2f);
        }  
    }

    public int getVal()
    {
        return val;
    }

    public void setParent(int index)
    {
        parent = index;
    }

    public Sprite getSprite()
    {
        return this.GetComponent<SpriteRenderer>().sprite;
    }

    bool SameGem( Gem other)
    {
        return other.getVal() == this.val;  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BackTile")
        {
            if (collision.gameObject.GetComponent<gameTile>().getIndex() == parent)
            {
                print("found parent");
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }
}
