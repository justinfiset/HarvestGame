using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderQueue : MonoBehaviour
{
    public Stack<CustomerOrderList> m_pendingOrders = new Stack<CustomerOrderList>();

    [Header("Customer Settings")]
    [SerializeField] private Transform m_targetPosition;
    [SerializeField] private Transform m_spawnPosition;
    [SerializeField] private Transform m_exitPosition;
    [SerializeField] private GameObject m_customerPrefab; // TODO MAYBE SWITCH TO AN ARRAY
    private CustomerOrder m_currentOrder;
    [SerializeField] private float m_animationDuration = 5.0f;

    public void Start()
    {
        // TODO REMOVE, FOR TEST PURPOSES ONLY
        List<CustomerOrderItem> list = new List<CustomerOrderItem>();

        CustomerOrderItem item = new CustomerOrderItem();
        item.crop = new Crop();
        item.quantity = 2;
        list.Add(item);

        CustomerOrderList order = new CustomerOrderList();
        order.totalTime = 5.0f;
        order.items = list;

        AddOrder(order);


        item.crop = new Crop();
        item.quantity = 2;
        list.Add(item);
        order.totalTime = 5.0f;
        order.items = list;

        AddOrder(order);
    }

    public void AddOrder(CustomerOrderList order)
    {
        m_pendingOrders.Push(order);

        if(m_currentOrder == null)
        {
            InstantiateNextOrder();
        }
    }

    public void InstantiateNextOrder()
    {
        if (m_pendingOrders.Count == 0) return;

        GameObject instance = Instantiate(m_customerPrefab, transform);
        m_currentOrder = instance.GetComponentInChildren<CustomerOrder>();

        CustomerOrderList orderList = m_pendingOrders.Pop();
        m_currentOrder.Setup(orderList);

        m_currentOrder.transform.position = m_spawnPosition.position;

        LeanTween.moveX(m_currentOrder.gameObject, m_targetPosition.position.x, m_animationDuration)
            .setEaseInOutQuad()
            .setOnComplete(StartCountdown);
    }

    public void StartCountdown()
    {
        m_currentOrder.StartTimer(this);
    }

    public void NotifyTimerEnded()
    {
        LeanTween.moveX(m_currentOrder.gameObject, m_exitPosition.position.x, m_animationDuration)
            .setOnComplete(() =>
            {
                Destroy(m_currentOrder.gameObject);
                InstantiateNextOrder();
            });
    }
}
