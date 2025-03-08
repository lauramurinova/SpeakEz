using Meta.XR.MRUtilityKit;
using UnityEngine;

// Manages the room and its objects
public class SceneSetup : MonoBehaviour
{
    public static SceneSetup Instance;

    private MRUKRoom _room;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Returns the first object in the room of the given label.
    private GameObject FindAnchorObjectOfType(MRUKAnchor.SceneLabels label)
    {
        if (_room == null)
        {
            Debug.LogError("SceneSetup: No room found");
            return null;
        }
        
        foreach (var obj in _room.Anchors)
        {
            if (obj.Label == label)
            {
                return obj.transform.gameObject;
            }
        }
        Debug.LogError($"Object with label {label} not found.");
        return null;
    }

    // Set the current room
    public void SetRoom(MRUKRoom room)
    {
        _room = room;
    }
    

    // Returns the first door in the room.
    public GameObject GetDoor()
    {
        return FindAnchorObjectOfType(MRUKAnchor.SceneLabels.DOOR_FRAME);
    }
    
    // Returns the first couch in the room.
    public GameObject GetCouch()
    {
        return FindAnchorObjectOfType(MRUKAnchor.SceneLabels.COUCH);
    }
}
