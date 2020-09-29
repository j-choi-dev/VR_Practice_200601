using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Choi.MyProj.Domain.InGame
{
    /// <summary>
    /// Value
    /// </summary>
    public struct Value
    {
        public const int MockCount = 10; // TODO これモック

        public const float ColliderActiveDistance = 1.5f;

        public const float OverJudgementDistance = 1f;

        public const float OverJudgementDelta = -0.5f;

        public const int NoteObjectLayer = 1 << 12;
    }

    /// <summary>
    /// Note情報
    /// </summary>
    public enum NoteSide
    {
        Left,
        Right,
    }

    /// <summary>
    /// Score 判定
    /// </summary>
    public enum Score
    {
        Perfect,
        Greate,
        Good,
        Bad,
        Miss,
    }
}