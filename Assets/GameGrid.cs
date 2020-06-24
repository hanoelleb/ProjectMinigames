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
                Gem temp1 = gameGrid[currIndex].getGem();
                gameGrid[currIndex].setGem(gameGrid[upIndex].getGem());
                gameGrid[upIndex].setGem(temp1);
                checkBoard();
            }
        } 
        else
        {
            current = update;
            current.setSelected(true);
        }
    }

    void checkBoard()
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < gameGrid.Length; i++)
        {
            /*
            if (i - GRIDSIZE * 2 >= 0 && i > 12)
            {
                bool up = checkUp(i);
                if (up)
                {
                    gameGrid[i].setBg(backTileSprites[1]);
                    gameGrid[i - GRIDSIZE].setBg(backTileSprites[1]);
                    gameGrid[i - GRIDSIZE*2].setBg(backTileSprites[1]);
                }
            } */
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
            if ((i + 1) % GRIDSIZE != 1 && (i + 2) % GRIDSIZE != 1 && i < 34)
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
            /*
            if ((i - 1) % GRIDSIZE != 0 && (i + 2) % GRIDSIZE != 0 && i > 1)
            {
                checkLeft(i);
            }
            */
        }

        removeBlocks(indices);
    }

    bool checkUp(int index)
    {
        int val1 = gameGrid[index].getGem().getVal();
        int val2 = gameGrid[index - GRIDSIZE].getGem().getVal();
        int val3 = gameGrid[index - GRIDSIZE * 2].getGem().getVal();

        return val1 == val2 && val1 == val3;
    }

    bool checkDown(int index)
    {
        int val1 = gameGrid[index].getGem().getVal();
        int val2 = gameGrid[index + GRIDSIZE].getGem().getVal();
        int val3 = gameGrid[index + GRIDSIZE * 2].getGem().getVal();

        return val1 == val2 && val1 == val3;
    }

    bool checkLeft(int index)
    {
        int val1 = gameGrid[index].getGem().getVal();
        int val2 = gameGrid[index - 1].getGem().getVal();
        int val3 = gameGrid[index - 2].getGem().getVal();

        return val1 == val2 && val1 == val3;
    }

    bool checkRight(int index)
    {
        int val1 = gameGrid[index].getGem().getVal();
        int val2 = gameGrid[index + 1].getGem().getVal();
        int val3 = gameGrid[index + 2].getGem().getVal();

        return val1 == val2 && val1 == val3;
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
    }

    void removeBlocks(List<int> indices)
    {
        StartCoroutine(Example());

        for (int i = 0; i < indices.Count; i++)
        {
            gameGrid[indices[i]].setGem(null);
        }

        moveDown();
    }

    void moveDown()
    {
        for (int i = 35; i >= 0; i--)
        {
            if (gameGrid[i].getGem() == null)
            {
                int findUp = i - 6;
                while (findUp >= 0)
                {
                    if (gameGrid[findUp].getGem() != null)
                    {
                        gameGrid[i].setGem(gameGrid[findUp].getGem());
                        gameGrid[findUp].setGem(null);
                    }
                    findUp -= 6;
                }

                if (gameGrid[i].getGem() == null) //no gems available above
                {
                    gameGrid[i].randomGem();
                }
            }
        }
    }
}
