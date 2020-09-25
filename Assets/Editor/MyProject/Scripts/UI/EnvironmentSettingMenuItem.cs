using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Choi.MyProj.Domain.Virtual;
using Choi.MyProj.Repository.Editor;
using Choi.MyProj.UseCase.Virtual;
using System.Threading.Tasks;

namespace Choi.MyProj.UI.Editor
{
#if UNITY_EDITOR
    public static class EnvironmentSettingMenuItem
    {
        /// <summary>
        /// EditorでVirtualモードとして実行するかをチェック入れるメニュー
        /// </summary>
        public const string UseVirtualInEditorMenuPath = "ChoiTool/Use VR in Editor";

        [MenuItem(UseVirtualInEditorMenuPath)]
        /// <summary>
        /// EditorでVirtualモードとして実行するかをチェック入れるメソッド
        /// </summary>
        public static async Task SetVirtualMode()
        {
            var usecase = new VirtualModeChangeInEditorUseCase(new VirtualStateInEditorRepository());
            var isChecked = Menu.GetChecked(UseVirtualInEditorMenuPath);
            Menu.SetChecked(UseVirtualInEditorMenuPath, !isChecked);
            isChecked = Menu.GetChecked(UseVirtualInEditorMenuPath);
            var result = await usecase.Execute(isChecked ? VirtualState.Virtual : VirtualState.Normal);
        }
    }
#endif
}