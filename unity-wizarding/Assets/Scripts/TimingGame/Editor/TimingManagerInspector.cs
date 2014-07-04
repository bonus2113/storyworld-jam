using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TimingManager))]
public class TimingManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var myTarget = (TimingManager) target;

        if (myTarget.HasSequence() && Application.isPlaying)
        {
            if (GUILayout.Button("Start Playback"))
            {
                myTarget.PlaySequence(myTarget.CurrentSequence);
            }

            if (GUILayout.Button("Stop Playback"))
            {
                myTarget.StopPlayback();
            }
        }
    }
}
