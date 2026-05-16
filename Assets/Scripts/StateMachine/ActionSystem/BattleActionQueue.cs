using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionQueue : MonoBehaviour
{
    // Queue chứa toàn bộ action đang chờ xử lý
    private readonly Queue<IBattleAction> actionQueue = new();

    // Kiểm tra queue có đang chạy không
    public bool IsProcessing { get; private set; }

    // Action hiện tại đang execute
    public IBattleAction CurrentAction { get; private set; }

    // Event gọi khi queue bắt đầu chạy
    public event Action OnQueueStarted;

    // Event gọi khi queue chạy xong hoàn toàn
    public event Action OnQueueFinished;

    // Event gọi mỗi lần dequeue action mới
    public event Action<IBattleAction> OnActionStarted;

    // Event gọi khi action execute xong
    public event Action<IBattleAction> OnActionFinished;

    /// <summary>
    /// Thêm action vào queue
    /// </summary>
    public void Enqueue(IBattleAction action)
    {
        if (action == null)
        {
            Debug.LogWarning("Tried to enqueue null action.");
            return;
        }

        actionQueue.Enqueue(action);

        // Nếu queue chưa chạy
        // thì bắt đầu process
        if (!IsProcessing)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    /// <summary>
    /// Execute toàn bộ action tuần tự
    /// </summary>
    private IEnumerator ProcessQueue()
    {
        IsProcessing = true;

        OnQueueStarted?.Invoke();

        // Chạy tới khi queue rỗng
        while (actionQueue.Count > 0)
        {
            // Lấy action đầu queue
            CurrentAction = actionQueue.Dequeue();

            OnActionStarted?.Invoke(CurrentAction);

            // Chờ action execute hoàn toàn
            yield return CurrentAction.Execute();

            OnActionFinished?.Invoke(CurrentAction);

            // Clear current action
            CurrentAction = null;
        }

        IsProcessing = false;

        OnQueueFinished?.Invoke();
    }

    /// <summary>
    /// Xóa toàn bộ action chưa execute
    /// </summary>
    public void ClearQueue()
    {
        actionQueue.Clear();
    }

    /// <summary>
    /// Bao nhiêu action đang chờ
    /// </summary>
    public int PendingCount()
    {
        return actionQueue.Count;
    }
}
