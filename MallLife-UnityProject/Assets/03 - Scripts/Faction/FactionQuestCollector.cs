using UnityEngine;
using UnityEngine.Events;

public class FactionQuestCollector :  MonoBehaviour
{
    
    [SerializeField] private Inventory _inventory;
    [SerializeField] private QuestContent _questContent;
    
    [SerializeField] private Quest _quest;
    
    [SerializeField] private UnityEvent _onCollected;
    [SerializeField] private UnityEvent _onCollectFailed;
    [SerializeField] private UnityEvent _onQuestComplete;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        if(_questContent)
        {
            _quest = new Quest(_questContent);
        }
    }

    public void Collect()
    {
        bool? collectDone = _quest?.Collect(_inventory);
        if(collectDone.HasValue)
        {
            if (collectDone.Value) _onCollected?.Invoke();
            else _onCollectFailed?.Invoke();
        }
        
        bool? questComplete = _quest?.Complete;
        if(questComplete.HasValue && questComplete.Value) _onCollected?.Invoke();
    }
    
}
