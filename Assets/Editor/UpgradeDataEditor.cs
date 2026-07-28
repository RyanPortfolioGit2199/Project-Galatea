using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(UpgradesSO))]

public class UpgradeDataEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        // 2. Add the SHARED variables to the very top
        root.Add(new PropertyField(serializedObject.FindProperty("UpgradeID")));
        root.Add(new PropertyField(serializedObject.FindProperty("Cost")));
        
        // Add a visual separator spacer
        VisualElement spacer = new VisualElement();
        spacer.style.height = 10;
        root.Add(spacer);

        // 3. Add the Enum Type Dropdown
        SerializedProperty typeProperty = serializedObject.FindProperty("upgradeType");
        PropertyField typeField = new PropertyField(typeProperty);
        root.Add(typeField);
        typeField.style.marginBottom = 10;

        // 4. Create isolated groups for each upgrade type's unique fields
        VisualElement weaponGroup = CreatePropertyContainer("Damage", "ShieldDamage", "IsAutomatic", "CanCharge", "HitVFX", "FireRate", "GunPrefab", "isBurst");
        VisualElement thrusterGroup = CreatePropertyContainer("ThrusterSpeed", "DodgeRechargeRate", "ThrusterPrefab", "dodgeAmount");
        VisualElement shieldGroup = CreatePropertyContainer("ShieldAmount", "RechargeRate", "ShieldPrefab");

        // Add the conditional groups to the root layout
        root.Add(weaponGroup);
        root.Add(thrusterGroup);
        root.Add(shieldGroup);

        // 5. Define the visibility logic based on the enum state
        void ToggleFields(UpgradeType currentType)
        {
            weaponGroup.style.display = (currentType == UpgradeType.Weapon) ? DisplayStyle.Flex : DisplayStyle.None;
            thrusterGroup.style.display = (currentType == UpgradeType.Thrusters) ? DisplayStyle.Flex : DisplayStyle.None;
            shieldGroup.style.display = (currentType == UpgradeType.Shields) ? DisplayStyle.Flex : DisplayStyle.None;
        }

        // Initialize the inspector state when the asset is clicked
        ToggleFields((UpgradeType)typeProperty.enumValueIndex);

        // 6. Track changes in the Enum dropdown dynamically
        typeField.RegisterValueChangeCallback(evt =>
        {
            ToggleFields((UpgradeType)evt.changedProperty.enumValueIndex);
        });

        return root;
    }

    /// <summary>
    /// Helper method to safely bundle SerializedProperties into a single VisualElement container.
    /// </summary>
    private VisualElement CreatePropertyContainer(params string[] propertyNames)
    {
        VisualElement container = new VisualElement();
        foreach (string propName in propertyNames)
        {
            SerializedProperty prop = serializedObject.FindProperty(propName);
            if (prop != null)
            {
                container.Add(new PropertyField(prop));
            }
        }
        return container;
    }
}
