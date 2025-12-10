using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteAlways]
[RequireComponent(typeof(Graphic))]
public class CircleMaskController : UIBehaviour
{
    [Tooltip("Ўирина сглаживани€ кра€ (чем меньше, тем резче)")]
    [Range(0.001f, 0.1f)]
    public float edgeFeather = 0.01f;

    private Graphic graphicComponent;
    private Material materialInstance;
    private static readonly int EdgeFeatherProp = Shader.PropertyToID("_EdgeFeather");

    protected override void Awake()
    {
        base.Awake();
        graphicComponent = GetComponent<Graphic>();
        SetupMaterial();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetupMaterial();
        UpdateFeather();
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        SetupMaterial();
        UpdateFeather();
    }
#endif

    private void SetupMaterial()
    {
        if (graphicComponent != null && materialInstance == null)
        {
            Shader shader = Shader.Find("Sprites/CircleMask");
            if (shader != null)
            {
                materialInstance = new Material(shader);
                graphicComponent.material = materialInstance;
            }
            else
            {
                Debug.LogError("Shader 'Sprites/CircleMask' not found!");
            }
        }
    }

    private void UpdateFeather()
    {
        if (materialInstance != null)
        {
            materialInstance.SetFloat(EdgeFeatherProp, edgeFeather);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (graphicComponent != null && materialInstance != null)
        {
            graphicComponent.material = null;
            if (Application.isEditor)
            {
                DestroyImmediate(materialInstance);
            }
            else
            {
                Destroy(materialInstance);
            }
        }
    }
}
