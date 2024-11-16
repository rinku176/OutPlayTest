using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Board : MonoBehaviour
{
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

    //int GetWidth();
    //int GetHeight();

    //JewelKind GetJewel(int x, int y);
    //void SetJewel(int x, int y, JewelKind kind);
    Move CalculateBestMoveForBoard()
    {
        //int w = GetWidth();
        //int h = GetHeight();
        Move bestMove = new Move();
        int bestScore = 0;


        // swap all the tiles in the grid.
        //check in all direction if the JewelKind matches.
        //swap back if not.
        //else, start a count and count the points that will be acquired.
        //compare it with the bestScore, if > bestScore = count; count =0;
        //and then swap back, and repeat this for all tiles in the grid.
        //in the end, return the best move (x,y, direction)

        return bestMove;
    }
}