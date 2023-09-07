using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
[CustomEditor(typeof(EnemyManager))]
public class GesturesEditor : Editor
{
    private const float HandleSize = 0.1f;
    private static readonly Color LabelColor = Color.blue;
    private const float SphereRadius = 0.1f;
    private GUIStyle LabelStyle;

    private void OnEnable()
    {
        LabelStyle = new GUIStyle();
        LabelStyle.fontSize = 36;
        LabelStyle.fontStyle = FontStyle.Bold;
    }

    protected virtual void OnSceneGUI()
    {
        EnemyManager enemy = (EnemyManager)target;

        for (int i = 0; i < enemy.enemyPathPoints.Length; i++)
        {
            Vector3 newPosition = Handles.PositionHandle(enemy.enemyPathPoints[i], Quaternion.identity);

            // Draw a label with the point's order
            Handles.color = LabelColor;
            Handles.Label(enemy.enemyPathPoints[i], i.ToString(), LabelStyle);
            Handles.SphereHandleCap(0, enemy.enemyPathPoints[i], Quaternion.identity, SphereRadius, EventType.Repaint);

            // Update the point's position if it has changed
            if (newPosition != enemy.enemyPathPoints[i])
            {
                Undo.RecordObject(enemy, "Move Enemy Path Point");
                enemy.enemyPathPoints[i] = newPosition;
            }
        }
    }
}*/