using System.Collections.Generic;
using UnityEngine;
using Choi.MyProj.Interface.API;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.InGame;

namespace Choi.MyProj.UI.InGame
{
    /// <summary>
    /// Note Info 読み込む
    /// </summary>
    public sealed class NoteInfoControl
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <returns>Init Result</returns>
        public async UniTask<IList<NoteInfo>> Init()
        {
            var infos = new List<NoteInfo>();
            var mock = "Song_001";
            var textAsset = await AssetLoaderAPI.Instance.LoadTextData(mock);

            var rows = textAsset.text.Split('\n');
            for (var i = 1; i < rows.Length; i++)
            {
                infos.Add(ConvertToNoteInfo(rows[i]));
            }
            return infos;
        }

        /// <summary>
        /// Text to NoteInfo
        /// </summary>
        /// <param name="val"><Text Data/param>
        /// <returns>NoteInfo</returns>
        private NoteInfo ConvertToNoteInfo(string val)
        {
            var cols = val.Split(',');
            var id = int.Parse(cols[(int)ParseIndex.ID]);
            var time = int.Parse(cols[(int)ParseIndex.Time]);
            NoteType type;
            System.Enum.TryParse(cols[(int)ParseIndex.Type], out type);
            var deltaTime = int.Parse(cols[(int)ParseIndex.DeltaTime]);
            return new NoteInfo(id, time, type, deltaTime);
        }
    }
}