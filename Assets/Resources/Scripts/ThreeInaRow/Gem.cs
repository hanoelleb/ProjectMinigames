using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField]
    int val;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getVal()
    {
        return val;
    }

    public Sprite getSprite()
    {
        return this.GetComponent<SpriteRenderer>().sprite;
    }

    bool SameGem( Gem other)
    {
        return other.getVal() == this.val;  
    }
}
