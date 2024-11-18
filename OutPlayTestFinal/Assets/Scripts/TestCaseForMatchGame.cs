using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestCaseForMatchGame : MonoBehaviour
{
    int bestScore = 0;

    enum JewelKind
    {
        Empty,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }

    enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    struct Move
    {
        public int x;
        public int y;
        public MoveDirection direction;
    }

    private JewelKind[,] board; // to display test Board
    void Start()
    {
        TestBoard(6, 6); // 6x6 board
        PopulateBoard();       
        DemoCalculateBestMove(); 
    }

    private void TestBoard(int width, int height)
    {
        board = new JewelKind[width, height];
    }

    // Populates the board with random jewel kinds
    private void PopulateBoard()
    {
        System.Array values = System.Enum.GetValues(typeof(JewelKind));
        System.Random random = new System.Random();

        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                board[x, y] = (JewelKind)values.GetValue(random.Next(1, values.Length));
            }
        }
    }

    int GetWidth() { return board.GetLength(0);  } 
    int GetHeight() => board.GetLength(1);
    JewelKind GetJewel(int x, int y) => board[x, y];
    void SetJewel(int x, int y, JewelKind kind) => board[x, y] = kind;

    private void DemoCalculateBestMove()
    {
        // Print the initial board
        for (int y = 0; y < board.GetLength(1); y++)
        {
            string row = "";
            for (int x = 0; x < board.GetLength(0); x++)
            {
                row += board[x, y] + " ";
            }
            Debug.Log(row);
        }

        Move bestMove = CalculateBestMoveForBoard();
        Debug.Log($"Best Move: (x = {bestMove.x}, y = {bestMove.y}) -> {bestMove.direction} Direction");
    }
    

    private Move CalculateBestMoveForBoard()
    {
        int w = GetWidth();
        int h = GetHeight();
        Move bestMove = new Move();

        //to iterate for all the jewels on the grid
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                foreach (var direction in Enum.GetValues(typeof(MoveDirection)))
                {
                    //try to swap the two, and check if the count > bestScore so far
                    if (TrySwap(x, y, (MoveDirection)direction, out int counter) && (counter > bestScore))
                    {
                        bestScore = counter;
                        bestMove = new Move { x = x, y = y, direction = (MoveDirection)direction };
                    }
                }
            }
        }
        return bestMove;
    }

    private bool TrySwap(int x, int y, MoveDirection direction, out int counter)
    {
        counter = 0;

        int tempX = x, tempY = y;
        switch (direction)
        {
            case MoveDirection.Up:
                tempY--;
                break;
            case MoveDirection.Down:
                tempY++;
                break;
            case MoveDirection.Left:
                tempX--;
                break;
            case MoveDirection.Right:
                tempX++;
                break;
        }

        //to check if its valid positions
        if (!IsValidPosition(tempX, tempY))
            return false;

        Swap(x, y, tempX, tempY);

        // Calculate max score
        counter = CalculateMaxScore();

        // Undo swap
        Swap(x, y, tempX, tempY);

        return (counter > bestScore);
    }

    private void Swap(int x1, int y1, int x2, int y2)
    {
        JewelKind temp = GetJewel(x1, y1);
        SetJewel(x1, y1, GetJewel(x2, y2));
        SetJewel(x2, y2, temp);
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && y >= 0 && x < GetWidth() && y < GetHeight();
    }

    private int CalculateMaxScore()
    {
        int count = 0;
        int width = GetWidth();
        int height = GetHeight();
        int Finalcounter = 0;

        bool[,] matched = new bool[width, height]; //to track the matched jewels

        // Check horizontal matches
        for (int y = 0; y < height; y++)
        {
            int rowcount = 0;
            while (rowcount < width)
            {
                count = 1;
                JewelKind currentKind = GetJewel(rowcount, y);

                // Count matching jewels
                for (int x = rowcount + 1; x < width && GetJewel(x, y) == currentKind; x++)
                {
                    count++;
                }

                // to store the matched jewels before the count resets to 1
                if (count >= 3)
                {
                    for (int x = rowcount; x < rowcount + count; x++)
                    {
                        matched[x, y] = true;
                    }
                }

                rowcount += count;
            }
        }

        // Check vertical matches
        for (int x = 0; x < width; x++)
        {
            int columncount = 0;
            while (columncount < height)
            {
                count = 1;
                JewelKind currentKind = GetJewel(x, columncount);

                // Count matching jewels
                for (int y = columncount + 1; y < height && GetJewel(x, y) == currentKind; y++)
                {
                    count++;
                }

                // to store the matched jewels before the count resets to 1
                if (count >= 3)
                {
                    for (int y = columncount; y < columncount + count; y++)
                    {
                        matched[x, y] = true;
                    }
                }

                columncount += count;
            }
        }


        //finally to count the matched jewels
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (matched[x, y])
                {
                    Finalcounter++;
                }
            }
        }
        return Finalcounter;

    }
}

