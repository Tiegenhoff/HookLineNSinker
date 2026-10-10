using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RodSlider : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Slider slider;
    public Transform knob;
    public Canvas canvas;

    public GameObject[] pointsOBJ;
    private List<Transform> points;

    private void Start()
    {
        // Setup transform points
        points = new List<Transform>();
        foreach (var p in pointsOBJ)
        {
            points.Add(p.transform);
        }

        // Setup Slider
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.wholeNumbers = false;

        slider.onValueChanged.AddListener(MoveKnob);
        MoveKnob(slider.value);
    }

    private void MoveKnob(float value)
    {
        if (points == null || points.Count < 2)
            return;

        // Convert slider value into a position between the points
        float scaledValue = value * (points.Count - 1);

        int pointIndex = Mathf.FloorToInt(scaledValue);
        pointIndex = Mathf.Clamp(pointIndex, 0, points.Count - 2);

        float t = scaledValue - pointIndex;

        Transform pointA = points[pointIndex];
        Transform pointB = points[pointIndex + 1];

        knob.position = Vector3.Lerp(
            pointA.position,
            pointB.position,
            t
        );

        knob.rotation = Quaternion.Lerp(
            pointA.rotation,
            pointB.rotation,
            t
        );
    }

    #region knobControls
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Is Down!");
        UpdateSliderFromMouse(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("I Am Dragging It!");
        UpdateSliderFromMouse(eventData.position);
    }

    private void UpdateSliderFromMouse(Vector2 mousePosition)
    {
        Camera canvasCamera = null;

        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            canvasCamera = canvas.worldCamera;
        }

        float closestDistance = Mathf.Infinity;
        float closestValue = 0f;

        for (int i = 0; i < points.Count - 1; i++)
        {
            //Vector2 pointA = cam.WorldToScreenPoint(points[i].position);
            //Vector2 pointB = cam.WorldToScreenPoint(points[i + 1].position);

            Vector2 pointA = RectTransformUtility.WorldToScreenPoint(
                canvasCamera,
                points[i].position
            );

            Vector2 pointB = RectTransformUtility.WorldToScreenPoint(
                canvasCamera,
                points[i + 1].position
            );

            Vector2 pathLine = pointB - pointA;

            if (pathLine.sqrMagnitude == 0f)
                continue;

            float t = Vector2.Dot(mousePosition - pointA, pathLine)
                      / pathLine.sqrMagnitude;

            t = Mathf.Clamp01(t);

            Vector2 closestPoint = pointA + pathLine * t;
            float distance = Vector2.Distance(mousePosition, closestPoint);

            if (distance < closestDistance)
            {
                closestDistance = distance;

                // Convert this point on the path into a slider value
                closestValue = (i + t) / (points.Count - 1);
            }
        }
        
        Debug.Log("Calculated Value: " + closestValue);
        
        slider.SetValueWithoutNotify(closestValue);
        MoveKnob(closestValue);
    }
    #endregion

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(MoveKnob);
    }
}
