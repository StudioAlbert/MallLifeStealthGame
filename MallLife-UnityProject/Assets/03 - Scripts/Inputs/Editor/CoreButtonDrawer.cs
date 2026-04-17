// Assets/03 - Scripts/Editor/CoreButtonDrawer.cs
using UnityEditor;
using UnityEngine;
using CoreInputs;

[CustomPropertyDrawer(typeof(CoreButton))]
public class CoreButtonDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Une seule ligne
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Affiche le label du champ
        position = EditorGUI.PrefixLabel(position, label);

        // Récupère les propriétés sérialisées
        var upProp = property.FindPropertyRelative("_up");
        var downProp = property.FindPropertyRelative("_down");
        var maintainedProp = property.FindPropertyRelative("_maintained");

        // Calcul des rects pour 3 indicateurs côte à côte
        float spacing = 4f;
        float totalWidth = position.width;
        float itemWidth = (totalWidth - spacing * 2) / 3f;

        Rect upRect = new Rect(position.x, position.y, itemWidth, position.height);
        Rect downRect = new Rect(position.x + itemWidth + spacing, position.y, itemWidth, position.height);
        Rect maintainedRect = new Rect(position.x + (itemWidth + spacing) * 2, position.y, itemWidth, position.height);

        // Couleurs visuelles (vert = actif, gris = inactif)
        // Color defaultColor = GUI.backgroundColor;
        //
        // GUI.backgroundColor = upProp.boolValue ? Color.green : Color.gray;
        // EditorGUI.LabelField(upRect, "Up", EditorStyles.colorField);
        //
        // GUI.backgroundColor = downProp.boolValue ? Color.cyan : Color.gray;
        // EditorGUI.LabelField(downRect, "Down", EditorStyles.colorField);
        //
        // GUI.backgroundColor = maintainedProp.boolValue ? Color.yellow : Color.gray;
        // EditorGUI.LabelField(maintainedRect, "Maintained", EditorStyles.colorField);
        //
        // GUI.backgroundColor = defaultColor;
        // Couleurs visuelles (vert = actif, gris = inactif)
        DrawIndicator(upRect, "Up", upProp.boolValue ? Color.green : Color.gray);
        DrawIndicator(downRect, "Down", downProp.boolValue ? Color.cyan : Color.gray);
        DrawIndicator(maintainedRect, "Held", maintainedProp.boolValue ? Color.yellow : Color.gray);

        
        EditorGUI.EndProperty();
    }
    
    private void DrawIndicator(Rect rect, string label, Color color)
    {
        // Dessine un rectangle plein avec la couleur voulue
        EditorGUI.DrawRect(rect, color);

        // Texte centré par-dessus, en noir pour la lisibilité
        var style = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.black }
        };
        EditorGUI.LabelField(rect, label, style);
    }
}