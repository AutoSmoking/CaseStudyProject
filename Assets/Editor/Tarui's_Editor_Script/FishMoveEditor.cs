using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(FishMove))]
public class FishMoveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FishMove Fish = target as FishMove;

        // 共通の注記
        EditorGUILayout.HelpBox("動きの種類を変えるとインスペクターが変わるよ", MessageType.Info, true);

        // 入力された魚の動きのタイプを取得
        Fish.moveType = (FishMove.MoveType)EditorGUILayout.EnumPopup("魚の動きの種類", Fish.moveType);

        // 各タイプごとの注記
        if (Fish.moveType == FishMove.MoveType.直進)
        {
            EditorGUILayout.HelpBox("指定した軸の方向にまっすぐ進みます", MessageType.Info, true);
        }
        else if (Fish.moveType == FishMove.MoveType.回転)
        {
            EditorGUILayout.HelpBox("指定した軸に沿って回転します", MessageType.Info, true);
        }

        // 共通の設定
        Fish.MoveAxis = (FishMove.Axis)EditorGUILayout.EnumPopup("魚の動きの軸", Fish.MoveAxis);

        Fish.MoveSpd = EditorGUILayout.Slider("移動スピード", Fish.MoveSpd, 1, 1000);

        Fish.TurnTime = EditorGUILayout.Slider("方向転換時間", Fish.TurnTime, 0.1f, 5.0f);

        if (Fish.moveType == FishMove.MoveType.回転)
        {
            Fish.centerPos = (Transform)EditorGUILayout.ObjectField("中心座標", Fish.centerPos, typeof(Transform), true);
        }

        EditorGUILayout.HelpBox("TRUE：正方向　　　FALSE：逆方向", MessageType.Info, true);
        Fish.LRFlag = EditorGUILayout.Toggle("移動方向", Fish.LRFlag);

        EditorUtility.SetDirty(target);
    }
}
