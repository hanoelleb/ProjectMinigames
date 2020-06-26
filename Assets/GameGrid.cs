using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    const int GRIDSIZE = 6;

    gameTile current;


    [SerializeField]
    gameTile[] gameGrid;

    [SerializeField]
    List<Sprite> backTileSprites;

    // Start is called before the first frame update
    void Start()
    {
        var tiles = GetComponentsInChildren<gameTile>();

        gameGrid = tiles;

        for (int i = 0; i < gameGrid.Length; i++)
        {
            gameGrid[i].setIndex(i);
        }

        setUp();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void setCurrent(gameTile update)
    {
        if (current != null)
        {
            int currIndex = current.getIndex();
            int upIndex = update.getIndex();

            bool isAdjacent = false;

            if ((upIndex == currIndex + 1 && upIndex % GRIDSIZE != 0) ||  //to right
                 (upIndex == currIndex - 1 && upIndex % GRIDSIZE != 5) ||  //to left
                 (upIndex == currIndex - GRIDSIZE || upIndex == currIndex + GRIDSIZE))  //top or bottom
            {
                isAdjacent = true;
                print("isAdjacent");
                current.setSelected(false);
                current = null;
            }

            if (!isAdjacent)
            {
                current.setSelected(false);
                current = update;
                current.setSelected(true);
            } else
            {
                Gem currGem = gameGrid[currIndex].getGem();
                Gem upGem = gameGrid[upIndex].getGem();

                Transform currGemTrans = gameGrid[currIndex].getGemTransform();
                Transform upGemTrans = gameGrid[upIndex].getGemTransform();

                currGemTrans.parent = gameGrid[upIndex].gameObject.transform;
                upGemTrans.parent = gameGrid[currIndex].gameObject.transform;

                gameGrid[upIndex].setGem(currGem);
                gameGrid[currIndex].setGem(upGem);

                checkBoard(false);
                //currGem.gameObject.GetComponent<Gem>().setParent(upIndex);
                /*
                gameGrid[currIndex].setGem(gameGrid[upIndex].getGem());
                gameGrid[upIndex].setGem(temp1);
                List<int> indices = checkBoard();
                if (indices.Count == 0)
                {
                    //no match, switch back
                    Gem temp2 = gameGrid[currIndex].getGem();
                    gameGrid[currIndex].setGem(gameGrid[upIndex].getGem());
                    gameGrid[upIndex].setGem(temp2);
                }
                */
            }
        } 
        else
        {
            current = update;
            current.setSelected(true);
        }
    }

    List<int> checkBoard(bool setup)
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < gameGrid.Length; i++)
        {
            
            if (i - GRIDSIZE * 2 <= GRIDSIZE * GRIDSIZE && i < 24)
            {
                bool down = checkDown(i);
                if (down)
                {
                    gameGrid[i].setBg(backTileSprites[1]);
                    gameGrid[i + GRIDSIZE].setBg(backTileSprites[1]);
                    gameGrid[i + GRIDSIZE * 2].setBg(backTileSprites[1]);

                    indices.Add(i);
                    indices.Add(i + GRIDSIZE);
                    indices.Add(i + GRIDSIZE*2);
                }
            }
            if ((i + 1) % GRIDSIZE != 0 && (i + 2) % GRIDSIZE != 1 && i < 34)
            {
                bool right = checkRight(i);
                if (right)
                {
                    gameGrid[i].setBg(backTileSprites[1]);
                    gameGrid[i + 1].setBg(backTileSprites[1]);
                    gameGrid[i + 2].setBg(backTileSprites[1]);

                    indices.Add(i);
                    indices.Add(i + 1);
                    indices.Add(i + 2);
                }
            }
            
        }

        if (!setup)
        {
            print("will remove");
            removeBlocks(indices);
        }
        return indices;
    }

    bool checkDown(int index)
    {
        int val1 = gameGrid[index].getGemVal();
        int val2 = gameGrid[index + GRIDSIZE].getGemVal();
        int val3 = gameGrid[index + GRIDSIZE * 2].getGemVal();

        return val1 == val2 && val1 == val3;
    }

    bool checkRight(int index)
    {
        int val1 = gameGrid[index].getGemVal();
        int val2 = gameGrid[index + 1].getGemVal();
        int val3 = gameGrid[index + 2].getGemVal();

        return val1 == val2 && val1 == val3;
    }

    void removeBlocks(List<int> indices)
    {
        for (int i = 0; i < indices.Count; i++)
        {
            gameGrid[indices[i]].deleteGem();
        }

        moveDown();
    }

    void moveDown()
    {
        for (int i = 35; i >= 0; i--)
        {
            if (gameGrid[i].getGem() == null)
            {
                int findUp = i - GRIDSIZE;
                while (findUp >= 0)
                {
                    if (gameGrid[findUp].getGem() != null && gameGrid[findUp].getGemTransform() != null)
                    {
                        /*Gem currGem = gameGrid[currIndex].getGem();
                Gem upGem = gameGrid[upIndex].getGem();

                Transform currGemTrans = gameGrid[currIndex].getGemTransform();
                Transform upGemTrans = gameGrid[upIndex].getGemTransform();

                currGemTrans.parent = gameGrid[upIndex].gameObject.transform;
                upGemTrans.parent = gameGrid[currIndex].gameObject.transform;

                gameGrid[upIndex].setGem(currGem);
                gameGrid[currIndex].setGem(upGem);*/

                        Gem foundGem = gameGrid[findUp].getGem();
                        Transform foundTrans = gameGrid[findUp].getGemTransform();
                        //Transform foundGem 

                        foundTrans.parent = gameGrid[i].gameObject.transform;
                        gameGrid[i].setGem(foundGem);

                        //gameGrid[i].getGemTransform().position = ;
                        gameGrid[findUp].setGem(null);
                        break;
                    }
                    findUp -= GRIDSIZE;
                }

                if (gameGrid[i].getGem() == null) //no gems available above
                {
                    gameGrid[i].randomGem(false);
                }
            }
        }
    }

    void setUp() //prevents initiating with matches
    {
        List<int> indices = checkBoard(true);

        bool hasMatches = indices.Count != 0;

        while (hasMatches) //has matches
        {
            for (int i = 0; i < indices.Count; i++)
            {
                gameGrid[indices[i]].randomGem(true);
            }

            hasMatches = checkBoard(true).Count != 0;
        }
    }

}
