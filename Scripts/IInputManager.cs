using System;
using UnityEngine;

public interface IInputManager
{
    void AddListenerOnPointerDownEvent(Action<Vector3> listener);
    void AddListenerOnPointerSecondDownEvent(Action<Vector3> listener);
    void AddListenerOnPointerSecondUpEvent(Action listener);
    void RemoveListenerOnPointerDownEvent(Action<Vector3> listener);
    void RemoveListenerOnPointerSecondDownEvent(Action<Vector3> listener);
    void RemoveListenerOnPointerSecondUpEvent(Action listener);
}