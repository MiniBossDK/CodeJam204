using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private ScrollRect scrollRect;
    private RectTransform scrollContent;
    private RectTransform scrollViewport;
    [SerializeField]
    private RectTransform snapRect;

    private RectTransform[] scrollElements;
    private float snapPosX;
    private float leftContentCornerX;
    private float rightContentCornerX;

    [SerializeField]
    private int startElement;
    [SerializeField]
    private float snapAnimationDuration;

    private IEnumerator lerpAnimation;
    private bool isDragging;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        scrollContent = scrollRect.content ? scrollRect.content : throw new NullReferenceException("ScrollRect component is missing a content component!");
        scrollViewport = scrollRect.viewport ? scrollRect.viewport : throw new NullReferenceException("ScrollRect component is missing a viewport component!");
        scrollElements = GetScrollElements();

        snapPosX = snapRect.position.x;

        Vector3[] scrollContentCorners = new Vector3[4];
        scrollContent.GetWorldCorners(scrollContentCorners);

        const int bottomLeftCornerIndex = 0;
        const int bottomRightCornerIndex = 3;

        leftContentCornerX = scrollContentCorners[bottomLeftCornerIndex].x;
        rightContentCornerX = scrollContentCorners[bottomRightCornerIndex].x;

        SnapElementToCenter(scrollElements[startElement]);
    }

    private void OnEnable()
    {
        scrollRect.onValueChanged.AddListener(OnScrollChanged);
    }

    private void OnDisable()
    {
        scrollRect.onValueChanged.RemoveListener(OnScrollChanged);
    }

    /**
     * <summary>Function that is called every time the scroll panel moves</summary>
     */
    private void OnScrollChanged(Vector2 pos)
    {
        var scrollVelocityX = scrollRect.velocity.x;
        if (scrollVelocityX == 0) return;
        HandleInfiniteScroll();

        const float scrollVelocitySnapTarget = 200f;

        if (!isDragging && lerpAnimation == null)
        {
            if (Math.Abs(scrollVelocityX) < scrollVelocitySnapTarget)
            {
                scrollRect.inertia = false;
                var element = GetClosestElementToCenter();
                LerpSnapElementToCenter(element);
            }
        }
    }
    /**
     * <summary>Used for handling the infinite scroll feature</summary>
     */
    private void HandleInfiniteScroll()
    {
        foreach (var element in scrollElements)
        {
            if (IsElementOutOfBounds(element))
            {
                RepositionElementToOppositeSide(element);
            }
        }
    }
    /**
     * <summary>Repositions the element to the opposite site of the scroll content</summary>
     */
    private void RepositionElementToOppositeSide(RectTransform element)
    {
        var elementPosX = element.position.x;
        var elementAnchoredPos = element.anchoredPosition;
        var posX = elementAnchoredPos.x;
        var posY = elementAnchoredPos.y;
        var scrollContentWidth = scrollContent.rect.width;

        if (elementPosX < snapPosX)
        {
            var newPosX = posX + scrollContentWidth;
            element.anchoredPosition = new Vector2(newPosX, posY);
        }
        else if (elementPosX > snapPosX)
        {
            var newPosX = posX - scrollContentWidth;
            element.anchoredPosition = new Vector2(newPosX, posY);
        }
    }
    /**
     * <summary>Returns the element that has the smallest distance to center</summary>
     */
    private RectTransform GetClosestElementToCenter()
    {
        var closestElement = scrollElements[0];
        var closestDistance = Math.Abs(snapPosX - scrollElements[0].position.x);
        foreach (var scrollElement in scrollElements)
        {
            var scrollElementDistToCenterX = Math.Abs(snapPosX - scrollElement.position.x);
            if (scrollElementDistToCenterX < closestDistance)
            {
                closestDistance = scrollElementDistToCenterX;
                closestElement = scrollElement;
            }
        }
        return closestElement;
    }
    /**
     * <summary>Returns an array of all the scroll elements contained in the scroll content</summary>
     */
    private RectTransform[] GetScrollElements()
    {
        var elements = new RectTransform[scrollContent.childCount];

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = scrollContent.GetChild(i) as RectTransform;
        }

        return elements;
    }

    /**
     * <summary>Linear interpolates the position of a element from its start position to it end position
     * over a specified duration</summary>
     */
    private IEnumerator LerpToElement(float startPosition, float endPosition, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            scrollContent.position = new Vector2(Mathf.Lerp(startPosition, endPosition, timeElapsed / duration), scrollContent.position.y);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        scrollContent.position = new Vector2(endPosition, scrollContent.position.y);
    }
    /**
     * <summary>Snap the element to center position without animation</summary>
     */
    private void SnapElementToCenter(Transform element)
    {
        var contentPos = scrollContent.position;

        scrollContent.position = new Vector2((contentPos.x - element.position.x) + snapPosX, contentPos.y);
    }
    /**
     * <summary>Snap the element to center position with animation</summary>
     */
    private void LerpSnapElementToCenter(Transform element)
    {
        var contentPos = scrollContent.position;

        lerpAnimation = LerpToElement(contentPos.x, (contentPos.x - element.position.x) + snapPosX, snapAnimationDuration);
        StartCoroutine(lerpAnimation);
    }
    /**
     * <summary>Returns true if the element is out of the bounds of the scroll content left or right corner position</summary>
     */
    private bool IsElementOutOfBounds([NotNull] RectTransform element)
    {
        if (element == null) throw new ArgumentNullException(nameof(element));

        var elementPosX = element.position.x;

        return elementPosX < leftContentCornerX || elementPosX > rightContentCornerX;
    }
    /**
     * Called the user start dragging on the scroll bar
     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.inertia = true;
        isDragging = true;

        if (lerpAnimation != null)
        {
            StopCoroutine(lerpAnimation);
            lerpAnimation = null;
        }
    }
    /**
     * Called the user has stopped dragging on the scroll bar
     */
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }
}