using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
 
[RequireComponent(typeof(ScrollRect))]
public class ScrollViewExtra : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private enum SnapState
    {
        None,
        Inertia,
        Reverse,
    }
 
    public Action OnScrollStartDrag;
    public Action<int> OnScrollEndDrag;
    public Action<int> OnSelectGridChanged;
 
    [SerializeField] private float gridSpace = 20;
 
    [SerializeField] private bool isSnap;
 
    [SerializeField] private bool isScale;
 
    [SerializeField] private AnimationCurve scaleCurve = new AnimationCurve(new Keyframe(0,1),new Keyframe(1,0.5f));
 
    //初始化不变字段
 
    private ScrollRect scrollRect;
    private RectTransform contentRectTrans;
    private Vector2 scrollHalfSize; //列表尺寸
 
    private Vector2 gridSize;
    private List<RectTransform> gridList = new List<RectTransform>();
    private List<Vector2> gridCenterList = new List<Vector2>();
 
    private float snapDecelerate;
    private const float snapReverseSpeed = 500;
 
    private bool isInited = false;
 
    //动态变化字段
 
    private SnapState snapState = SnapState.None;
 
    private int curSelectIndex;
 
    //----------
 
    private void Start()
    {
        Init();
    }
 
    private void Update()
    {
        if(isInited)
        {
            switch(snapState)
            {
                case SnapState.Inertia:
                    UpdateSnapInertia();
                    break;
                case SnapState.Reverse:
                    UpdateSnapReverse();
                    break;
                default:
                    break;
            }
 
            if(contentRectTrans.hasChanged)
            {
                if(isScale)
                    UpdateScale();
                else
                    UpdateSelectGrid();
            }
        }
    }
 
    #region --- Drag Event
 
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnScrollStartDrag?.Invoke();
        BreakSnap();
    }
    public void OnDrag(PointerEventData eventData)
    {
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        StartSnap();
    }
 
    #endregion
 
    public void Init()
    {
        scrollRect = GetComponent<ScrollRect>();
        if(!scrollRect.horizontal)
            Debug.LogError("目前只支持横向从左往右列表");
 
        contentRectTrans = scrollRect.content;
        if(contentRectTrans.pivot.x > 0)
            Debug.LogError("目前只支持横向从左往右列表");
 
        //scrollCenter = scrollRect.viewport.rect.center;
        scrollHalfSize = scrollRect.viewport.rect.size * 0.5f;
 
        for(int i = 0; i < contentRectTrans.childCount; i++)
        {
            gridList.Add(contentRectTrans.GetChild(i) as RectTransform);
        }
        if(gridList.Count > 0)
            gridSize = gridList[0].rect.size;
 
        snapDecelerate = scrollRect.decelerationRate;
        //if(snapDecelerate < 0.1f)
        //    snapDecelerate = 0.1f;
 
        if(gridList.Count == 0)
            return;
 
        //第一个格子坐标
        Vector2 gridInitPos = gridList[0].anchoredPosition;
        gridInitPos.x = scrollHalfSize.x - gridSize.x * 0.5f;
 
        //格子间隔
        Vector2 gridOffset = Vector2.zero;
        gridOffset.x = gridSize.x + gridSpace;
 
        //计算画布尺寸
        Vector2 contentSize = contentRectTrans.rect.size;
        contentSize.x = gridSize.x * gridList.Count + gridSpace * (gridList.Count - 1) + scrollHalfSize.x * 2 - gridSize.x;
 
        //设置画布尺寸
        contentRectTrans.sizeDelta = contentSize;
        contentRectTrans.anchoredPosition = new Vector2(0,contentRectTrans.anchoredPosition.y);
 
        //设置每个格子坐标
        for(int i = 0; i < gridList.Count; i++)
        {
            gridList[i].anchoredPosition = gridInitPos + gridOffset * i;
            gridList[i].anchorMin = new Vector2(0,1);
            gridList[i].anchorMax = new Vector2(0,1);
            gridList[i].pivot = new Vector2(0,1);
            gridCenterList.Add(gridList[i].anchoredPosition + gridSize * 0.5f);
        }
 
        if(isScale)
            UpdateScale();
 
        isInited = true;
        curSelectIndex = 0;
    }
 
    #region --- Snap ---
 
    private Vector2 snapTargetPos;
 
    private void StartSnap()
    {
        if(isSnap)
        {
            if(gridList.Count > 0)
            {
                snapState = SnapState.Inertia;
            }
        }
    }
    private void UpdateSnapInertia()
    {
        if(scrollRect.velocity.x > -snapReverseSpeed && scrollRect.velocity.x < snapReverseSpeed)
        {
            //反向
            StartSnapReverse();
            return;
        }
    }
    private void StartSnapReverse()
    {
        snapState = SnapState.Reverse;
        scrollRect.StopMovement();
 
        //当前屏幕中心的画布坐标
        float centerPos = Mathf.Abs(contentRectTrans.anchoredPosition.x) + scrollHalfSize.x;
 
        float temOffset;
        float minOffet = float.MaxValue;
        for(int i = 0; i < gridCenterList.Count; i++)
        {
            if(!gridList[i].gameObject.activeSelf)
                continue;
            //格子中心坐标
            temOffset = centerPos - gridCenterList[i].x;
            //比较最小距离
            if(Mathf.Abs(temOffset) < Mathf.Abs(minOffet))
            {
                minOffet = temOffset;
                //格子在中间，反推画布的坐标
                snapTargetPos.x = -(gridCenterList[i].x - scrollHalfSize.x);
            }
        }
        snapTargetPos.y = contentRectTrans.anchoredPosition.y;
    }
    private void UpdateSnapReverse()
    {
        if(Mathf.Abs(contentRectTrans.anchoredPosition.x - snapTargetPos.x) < 1)
        {
            contentRectTrans.anchoredPosition = snapTargetPos;
            EndSnap();
            return;
        }
        contentRectTrans.anchoredPosition = Vector2.Lerp(contentRectTrans.anchoredPosition,snapTargetPos,snapDecelerate);
    }
 
    private void EndSnap()
    {
        if(snapState == SnapState.None)
            return;
 
        scrollRect.StopMovement();
        snapState = SnapState.None;
 
        if(isScale)
            UpdateScale();
 
        OnScrollEndDrag?.Invoke(curSelectIndex);
    }
 
    private void BreakSnap()
    {
        if(snapState != SnapState.None)
            snapState = SnapState.None;
    }
 
    #endregion
 
    #region --- Scale ---
 
    int tempIndex;
    float tempCenter;
    float tempOffset;
    float minDistance;
    Vector3 tempScale;
    Vector2 tempAnPos;
    private void UpdateScale()
    {
        minDistance = float.MaxValue;
        tempCenter = Mathf.Abs(contentRectTrans.anchoredPosition.x) + scrollHalfSize.x;
        for(int i = 0; i < gridCenterList.Count; i++)
        {
            if(!gridList[i].gameObject.activeSelf)
                continue;
            //格子中心到屏幕中心距离
            tempOffset = Mathf.Abs(tempCenter - gridCenterList[i].x);
            if(tempOffset > scrollHalfSize.x + gridSize.x)
                continue;
            //计算缩放值
            tempScale.x = scaleCurve.Evaluate(tempOffset / scrollHalfSize.x);
            tempScale.y = tempScale.x;
            tempScale.z = 1;
            //修改缩放
            gridList[i].localScale = tempScale;
            //修改位置（锚点在左上角，保证缩放后格子仍然在中间）
            tempAnPos.x = gridCenterList[i].x - gridSize.x * 0.5f * tempScale.x;
            tempAnPos.y = gridCenterList[i].y + gridSize.y * (0.5f * tempScale.y - 1);
            gridList[i].anchoredPosition = tempAnPos;
            //比较最小距离
            if(tempOffset < minDistance)
            {
                minDistance = tempOffset;
                tempIndex = i;
            }
        }
        if(curSelectIndex != tempIndex)
        {
            curSelectIndex = tempIndex;
            OnSelectGridChanged?.Invoke(curSelectIndex);
        }
    }
 
    private void UpdateSelectGrid()
    {
        minDistance = float.MaxValue;
        tempCenter = Mathf.Abs(contentRectTrans.anchoredPosition.x) + scrollHalfSize.x;
        for(int i = 0; i < gridCenterList.Count; i++)
        {
            if(!gridList[i].gameObject.activeSelf)
                continue;
            //格子中心到屏幕中心距离
            tempOffset = Mathf.Abs(tempCenter - gridCenterList[i].x);
            if(tempOffset > scrollHalfSize.x + gridSize.x)
                continue;
            //比较最小距离
            if(tempOffset < minDistance)
            {
                minDistance = tempOffset;
                tempIndex = i;
            }
        }
        if(curSelectIndex != tempIndex)
        {
            curSelectIndex = tempIndex;
            OnSelectGridChanged?.Invoke(curSelectIndex);
        }
    }
 
    #endregion
 
}