using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AtlasManager))]
[RequireComponent(typeof(DialoguePanelManager))]

public class DialogueManager : MonoBehaviour
{
    private List<DialogueStateManager> managerList = new List<DialogueStateManager>();
    public static AtlasManager atlasManager { get; private set; }
    public static DialoguePanelManager panelManager { get; private set; }
    void Awake()
    {
        atlasManager = GetComponent<AtlasManager>();
        panelManager = GetComponent<DialoguePanelManager>();

        managerList.Add(atlasManager);
        managerList.Add(panelManager);
        StartCoroutine(BootAllManagers());
    }

    private IEnumerator BootAllManagers()
    {
        foreach (DialogueStateManager manager in managerList)
        {
            manager.BootSequence();
        }
        yield return null;
    }
}
