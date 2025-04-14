using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    private Adventurer parent;
    private LineRenderer lineRenderer;

    void Start()
    {
        Invoke(nameof(DelayedStart), 0.1f);
    }

    void DelayedStart()
    {
        Debug.Log($"{gameObject.name}: LineConnector Start() called");

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;

        Adventurer adventurer = GetComponent<Adventurer>();
        if (adventurer != null)
        {
            parent = adventurer.parent;
        }
        //Draw line if a parent is present
        if (parent != null)
        {
            Debug.Log($"{gameObject.name}: Parent found -> {parent.gameObject.name}");
            Invoke(nameof(UpdateLine), 0.1f);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: No parent assigned");
        }
    }

    public void UpdateLine()
    {
        if (parent != null)
        {
            Debug.Log($"{gameObject.name}: Updating line to {parent.gameObject.name}");
            Vector3 startPos = parent.transform.position;
            Vector3 endPos = transform.position;

            if (parent.TryGetComponent<RectTransform>(out var parentRect))
                startPos = parentRect.position;
            if (TryGetComponent<RectTransform>(out var childRect))
                endPos = childRect.position;

            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: Cannot draw line, parent is null");
        }
    }

    public void SetLineColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}
