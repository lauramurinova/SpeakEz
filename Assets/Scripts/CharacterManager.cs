using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Manages the characters in the app.
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    
    [SerializeField] GameObject[] characters;
    
    [SerializeField] private float _chracterMoveSpeed = 3f;
    [SerializeField] private float _chracterRotationSpeed = 0.5f;


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

    // Generates the given amount of characters to the scene.
    public void GenerateCharacters(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var doorPosition = SceneSetup.Instance.GetDoor().transform.position;
            doorPosition.y = 0;
            characters[i].transform.position = doorPosition;
            characters[i].transform.rotation = SceneSetup.Instance.GetDoor().transform.rotation;

            SitCharacter(characters[i], SceneSetup.Instance.GetCouch());
        }
    }
    
    // Makes the character sit to the given couch.
    private void SitCharacter(GameObject character, GameObject couch)
    {
        UnityEvent reachedDestination = new UnityEvent();
        StartCoroutine(MoveToTarget(character.transform, couch.transform.position, couch.transform.rotation, reachedDestination));
        reachedDestination.AddListener(delegate
        {
            character.GetComponent<Animator>().SetTrigger("Sit");
            character.GetComponent<Character>().Speak("Hello Laura, it's nice to be here.");
        });
    }
    
    // Smooth continuous move of given object to a set destination.
    IEnumerator MoveToTarget(Transform obj, Vector3 targetPosition, Quaternion targetRotation, UnityEvent reachedDestination)
    {
        Quaternion startRotation = obj.transform.rotation;
        Vector3 startPosition = obj.position;
        
        Vector3 targetPositionRecalculate = targetPosition;
        targetPositionRecalculate.y = 0;
        targetPosition = targetPositionRecalculate;
        
        Vector3 targetRotationRecalculate = targetRotation.eulerAngles;
        targetRotationRecalculate.y -= 90;
        targetRotation = Quaternion.Euler(targetRotationRecalculate);
        
        float elapsedTime = 0f;

        while (elapsedTime < _chracterMoveSpeed)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / _chracterMoveSpeed);

            obj.position = Vector3.Lerp(startPosition, targetPosition, progress);
            obj.rotation = Quaternion.Slerp(startRotation, targetRotation, progress * _chracterRotationSpeed * Time.deltaTime);

            yield return null;
        }

        obj.position = targetPosition;
        reachedDestination.Invoke();
    }
}
