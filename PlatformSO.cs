using System;
using System.Collections.Generic;
using UnityEngine;

public struct Direction
{
    private const int PixelsPerTile = 16;
    public PosibleDirectionPlatforms CurrentDirection { get; set; }

    /// <summary>
    /// Se mide en píxeles.
    /// </summary>
    public int Distance
    {
        get => _distance;
        set => _distance = (int) value;
    }

    private int _distance;

    public Direction(PosibleDirectionPlatforms dir, int dist)
    {
        CurrentDirection = dir;
        _distance = dist;
    }

    public Vector2 ToVector2()
    {
        var vector = CurrentDirection switch
        {
            PosibleDirectionPlatforms.Right => Vector2.right,
            PosibleDirectionPlatforms.Left => Vector2.left,
            PosibleDirectionPlatforms.Up => Vector2.up,
            PosibleDirectionPlatforms.Down => Vector2.down,
            PosibleDirectionPlatforms.DiagonalUR => Vector2.one,
            PosibleDirectionPlatforms.DiafonalUL => new Vector2(-1, 1),
            PosibleDirectionPlatforms.DiagonalDL => -Vector2.one,
            PosibleDirectionPlatforms.DiagonalDR => new Vector2(1, -1),
            _ => Vector2.right
        };
        return vector;
    }
}

public enum PosibleDirectionPlatforms
{
    Right,
    Left,
    Up,
    Down,
    DiagonalUR,
    DiafonalUL,
    DiagonalDR,
    DiagonalDL
}

namespace HeroesGames.Arcade.SO.Platform
{
    [CreateAssetMenu(fileName = "Platform", menuName = "Scriptables/Platform")]
    public class PlatformSO : ScriptableObject
    {
        public List<PosibleDirectionPlatforms> directions = new List<PosibleDirectionPlatforms>();
        public List<int> distances = new List<int>();


        private List<Direction> directionList = new List<Direction>();

        public List<Direction> DirectionList => directionList;

        public void CreateFinalList()
        {
            for (int i = 0; i < directions.Count; i++)
            {
                directionList.Add(new Direction(directions[i], distances[i]));
            }
        }

        public void AddToList(PosibleDirectionPlatforms direction, int distance)
        {
            directions.Add(direction);
            distances.Add(distance);
        }

        public void AddToList(PosibleDirectionPlatforms direction, int distance, int index)
        {
            if (directions.Count < 2 || index == 0) return;
            index = Mathf.Clamp(index, 1, directions.Count);
            directions.Insert(index, direction);
            distances.Insert(index, distance);
        }

        public void AddRight(int distance) => AddToList(PosibleDirectionPlatforms.Right, distance);
        public void AddLeft(int distance) => AddToList(PosibleDirectionPlatforms.Left, distance);
        public void AddUp(int distance) => AddToList(PosibleDirectionPlatforms.Up, distance);
        public void AddDown(int distance) => AddToList(PosibleDirectionPlatforms.Down, distance);
        public void AddDiagonalUR(int distance) => AddToList(PosibleDirectionPlatforms.DiagonalUR, distance);
        public void AddDiagonalUL(int distance) => AddToList(PosibleDirectionPlatforms.DiafonalUL, distance);
        public void AddDiagonalDR(int distance) => AddToList(PosibleDirectionPlatforms.DiagonalDR, distance);
        public void AddDiagonalDL(int distance) => AddToList(PosibleDirectionPlatforms.DiagonalDL, distance);

        public void ResetDirections()
        {
            directions.Clear();
            directionList.Clear();
            distances.Clear();
        }

        public void RemoveDirection(int index)
        {
            index = Mathf.Clamp(index, 0, directions.Count - 1);
            if (directions.Count == 0) return;

            directions.RemoveAt(index);
            distances.RemoveAt(index);
        }

        public void RemoveDirection()
        {
            if (directions.Count == 0) return;

            RemoveDirection(directions.Count - 1);
        }
    }
}