using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
/// <summary>
/// 自动吸附的滑动列表
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public class ScrollRect_AutoAdsorption : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [Header("滑动列表类型")]
    public ScrollRectType scrollRectType;
    [Header("能否滑动多页")]
    public bool canMultiPageSlide;
    [Header("是否使用动态缩放")]
    public bool useDynamicScale;
    [HideInInspector]
    public float scaleCoe = 0.3f;
 
    int totalPage;//总页数
    int curPage;//当前页
    float[] eachPageNormalizedPos;//每页坐标
 
    ScrollRect scrollRect;//滑动区域
    RectTransform content;//Content
    RectTransform viewport;//viewport
    GridLayoutGroup layout;//布局
 
    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        viewport = scrollRect.viewport;
        layout = content.GetComponent<GridLayoutGroup>();
        if (layout == null)
        {
            Debug.LogError("Content下找不到GridLayoutGroup组件");
            layout = content.AddComponent<GridLayoutGroup>();
        }
 
        layout.constraint = scrollRectType == ScrollRectType.Horizontal
            ? GridLayoutGroup.Constraint.FixedRowCount
            : GridLayoutGroup.Constraint.FixedColumnCount;
        layout.constraintCount = 1;
        scrollRect.horizontal = scrollRectType == ScrollRectType.Horizontal;
        scrollRect.vertical = scrollRectType == ScrollRectType.Vertical;
    }
 
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        curPage = 1;
        totalPage = content.childCount;
 
        //计算每页的归一化坐标
        eachPageNormalizedPos = new float[totalPage];
        if (scrollRectType == ScrollRectType.Horizontal)
        {
            float tempNormalizedPos = 0;
            for (int i = 0; i < eachPageNormalizedPos.Length; i++)
            {
                eachPageNormalizedPos[i] = tempNormalizedPos;
                tempNormalizedPos += 1f / (totalPage - 1);
            }
        }
        else if (scrollRectType == ScrollRectType.Vertical)
        {
            float tempNormalizedPos = 1;
            for (int i = 0; i < eachPageNormalizedPos.Length; i++)
            {
                eachPageNormalizedPos[i] = tempNormalizedPos;
                tempNormalizedPos -= 1f / (totalPage - 1);
            }
        }
 
        //设置Content
        content.sizeDelta = scrollRectType == ScrollRectType.Horizontal
            ? new Vector2((totalPage - 1) * (layout.cellSize.x + layout.spacing.x), viewport.rect.height)
            : new Vector2(0, viewport.rect.height + (totalPage - 1) * (layout.cellSize.y + layout.spacing.y));
 
        scrollRect.horizontalNormalizedPosition = 0;
        scrollRect.verticalNormalizedPosition = 1;
    }
 
    bool inDrag;//是否在拖拽中
    float beginDragPos;//开始拖拽的位置
    float endDragPos;//结束拖拽的位置
 
    public void OnBeginDrag(PointerEventData eventData)
    {
        inDrag = true;
 
        beginDragPos = scrollRectType == ScrollRectType.Horizontal
            ? Input.mousePosition.x
            : Input.mousePosition.y;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        inDrag = false;
 
        endDragPos = scrollRectType == ScrollRectType.Horizontal
            ? Input.mousePosition.x
            : Input.mousePosition.y;
        float curNormalizedPos = scrollRectType == ScrollRectType.Horizontal
            ? scrollRect.horizontalNormalizedPosition
            : scrollRect.verticalNormalizedPosition;
 
        float offset = endDragPos - beginDragPos;
        //多页滑动
        if (canMultiPageSlide)
        {
            curPage = FindNearlyPage(curNormalizedPos);
        }
        //单页滑动
        else
        {
            int nearlyPage = FindNearlyPage(curNormalizedPos);
            if (nearlyPage != curPage)
            {
                if (offset > 0)
                {
                    curPage = scrollRectType == ScrollRectType.Horizontal
                        ? curPage - 1 < 1 ? 1 : curPage - 1
                        : curPage + 1 > totalPage ? curPage : curPage + 1;
                }
                else if (offset < 0)
                {
                    curPage = scrollRectType == ScrollRectType.Horizontal
                        ? curPage + 1 > totalPage ? curPage : curPage + 1
                        : curPage - 1 < 1 ? 1 : curPage - 1;
                }
            }
        }
    }
 
    private void Update()
    {
        if (layout == null) return;
 
        if (!inDrag)
        {
            scrollRect.normalizedPosition = scrollRectType == ScrollRectType.Horizontal
                ? new Vector2(Mathf.Lerp(scrollRect.horizontalNormalizedPosition, eachPageNormalizedPos[curPage - 1], 0.2f), 0)
                : new Vector2(0, Mathf.Lerp(scrollRect.verticalNormalizedPosition, eachPageNormalizedPos[curPage - 1], 0.2f));
        }
 
        if (useDynamicScale)
        {
            UpdateScale();
        }
    }
 
    /// <summary>
    /// 更新缩放
    /// </summary>
    void UpdateScale()
    {
        Vector2 centerPos = transform.position;
 
        //得到所有项
        GameObject[] items = new GameObject[content.childCount];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = content.GetChild(i).gameObject;
        }
 
        foreach (var item in items)
        {
            Vector2 itemPos = item.transform.position;
            Vector2 offset = (itemPos - centerPos) * scaleCoe;
            float offsetCoe = 1 - Mathf.Clamp(offset.magnitude, 0, 0.5f);
            item.transform.localScale = Vector3.one * offsetCoe;
        }
    }
 
    /// <summary>
    /// 查找最近的页
    /// </summary>
    int FindNearlyPage(float curNormalizedPos)
    {
        int tempPage = 1;
        float tempMinOffset = Mathf.Abs(curNormalizedPos - eachPageNormalizedPos[0]);
        for (int i = 1; i <= totalPage; i++)
        {
            float tempOffset = Mathf.Abs(curNormalizedPos - eachPageNormalizedPos[i - 1]);
            if (tempOffset < tempMinOffset)
            {
                tempMinOffset = tempOffset;
                tempPage = i;
            }
        }
        return tempPage;
    }
 
    #region interface
 
    /// <summary>
    /// 设置页数
    /// </summary>
    public void SetPage(int page)
    {
        curPage = page;
    }
 
    #endregion
}
 
/// <summary>
/// 滑动列表类型
/// </summary>
public enum ScrollRectType
{
    Horizontal,
    Vertical,
}
 
#if UNITY_EDITOR
 
/// <summary>
/// 自动吸附的滑动列表Inspector扩展
/// </summary>
[UnityEditor.CustomEditor(typeof(ScrollRect_AutoAdsorption))]
public class ScrollRect_AutoAdsorption_Editor : UnityEditor.Editor
{
    ScrollRect_AutoAdsorption t;
 
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
 
        t = target as ScrollRect_AutoAdsorption;
 
        if (t.useDynamicScale)
        {
            t.scaleCoe = UnityEditor.EditorGUILayout.FloatField("缩放系数", t.scaleCoe);
        }
    }
}
 
#endif