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

    /// <summary>
    /// Note 判定結果
    /// </summary>
    public struct NoteResult
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// 判定
        /// </summary>
        public Judgement Judgement { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="judge">判定</param>
        public NoteResult(int id, Judgement judge)
        {
            ID = id;
            Judgement = judge;
        }
    }

    /// <summary>
    /// CSVパーサーから読み込む情報
    /// </summary>
    public struct NoteInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// 千分率 Time
        /// </summary>
        public int Time { get; private set; }

        /// <summary>
        /// タイプ
        /// </summary>
        public NoteType Type { get; private set; }

        /// <summary>
        /// 時間差(千分率)
        /// </summary>
        public int DeltaTime { get; private set; }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="time">千分率 Time</param>
        /// <param name="type">タイプ</param>
        /// <param name="deltaTime">時間差(千分率)</param>
        public NoteInfo(int id, int time, NoteType type, int deltaTime)
        {
            ID = id;
            Time = time;
            Type = type;
            DeltaTime = deltaTime;
        }
    }

    /// <summary>
    /// Note情報
    /// </summary>
    public enum NoteType
    {
        Start,
        Left,
        Right,
        Finish,
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

    /// <summary>
    /// Note Info Parse Index
    /// </summary>
    public enum ParseIndex
    {
        ID = 0,
        Time = 1,
        Type = 2,
        DeltaTime = 3,
    }
}