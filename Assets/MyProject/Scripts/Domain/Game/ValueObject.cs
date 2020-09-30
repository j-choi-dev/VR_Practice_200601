using System.Collections;
using System.Collections.Generic;
using System;


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

        public struct ScoreJedge
        {
            public const int Bad = 150;
            public const int Good = 100;
            public const int Greate = 60;
            public const int Perfect = 30;
        }
    }

    public struct NoteResult
    {
        public int ID { get; private set; }

        public Judgement Judgement { get; private set; }

        public NoteResult(int id, Judgement judge)
        {
            ID = id;
            Judgement = judge;
        }
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
    public enum Judgement
    {
        None,
        Perfect,
        Greate,
        Good,
        Bad,
        Miss,
    }
}