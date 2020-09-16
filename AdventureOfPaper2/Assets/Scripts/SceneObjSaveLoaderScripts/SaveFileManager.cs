using System;
using UnityEngine;
using Extensions;

[Serializable]
[CreateAssetMenu(menuName ="Custom Assets/Save File manager")]
public class SaveFileManager : ScriptableObjectSingleton<SaveFileManager>
{
    [SerializeField]
    public SaveFileManager[] SaveFiles;

    [SerializeField]
    public int CurrentSaveFile;
}
