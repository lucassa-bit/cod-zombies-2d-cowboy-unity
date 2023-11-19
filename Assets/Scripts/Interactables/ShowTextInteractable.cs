using TMPro;
using UnityEngine;

public class ShowTextInteractable : MonoBehaviour
{
    [SerializeField]
    private float circleRadius;
    [SerializeField]
    private string typeOfInteractable;
    private GameObject textMeshGO;

    private void Awake() {
        foreach (Transform transform in GetComponentsInChildren<Transform>()) {
            if(transform.gameObject.layer == 5) { 
                textMeshGO = transform.gameObject;
            }
        }
    }

    private void Start() {
        textMeshGO.GetComponent<TextMeshPro>().text = formatTextToDisplay(typeOfInteractable);
    }

    private void Update() {
        textMeshGO.SetActive(Physics2D.CircleCast(transform.position, circleRadius, Vector2.zero, 0, LayerMask.GetMask("Player")));
    }

    private string formatTextToDisplay(string weapowType) {
        string formattedText = "Para acessar o(a) " + weapowType;
        formattedText += "\nPague " + GetComponent<IInteraction>().getCost() + " pontos";

        return formattedText;
    }
}
