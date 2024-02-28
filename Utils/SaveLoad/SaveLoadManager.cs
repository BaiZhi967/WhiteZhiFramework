using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WhiteZhi
{
    public class SaveLoadManager : GlobalSingleton<SaveLoadManager>
    {
        public Action onSaveAction;
    }
}