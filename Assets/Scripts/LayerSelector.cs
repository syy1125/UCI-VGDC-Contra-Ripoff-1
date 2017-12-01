using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SingleLayer))]
public class LayerSelector : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		// Label
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Selection
		SerializedProperty layerProperty = property.FindPropertyRelative("value");
		layerProperty.intValue = EditorGUI.LayerField(position, layerProperty.intValue);

		EditorGUI.EndProperty();
	}
}