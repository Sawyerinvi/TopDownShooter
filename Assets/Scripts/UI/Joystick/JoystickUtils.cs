using UnityEngine;

public static class JoystickUtils
{

    public static Vector3 TouchPosition(this Canvas _Canvas,int touchID)
    {
        Vector3 point = Vector3.zero;

        if (_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
#if UNITY_IOS || UNITY_ANDROID && !UNITY_EDITOR
            return Input.GetTouch(touchID).position;
#else
            point = Input.mousePosition;
#endif
        }
        else if (_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            Vector2 tempVector = Vector2.zero;
#if UNITY_IOS || UNITY_ANDROID && !UNITY_EDITOR
           Vector3 pos = Input.GetTouch(touchID).position;
#else
            Vector3 pos = Input.mousePosition;
#endif
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, pos, _Canvas.worldCamera, out tempVector);
            point = _Canvas.transform.TransformPoint(tempVector);
        }

        return point;
    }
}