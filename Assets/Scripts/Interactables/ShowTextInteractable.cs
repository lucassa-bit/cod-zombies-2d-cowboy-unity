using TMPro;
using UnityEngine;

public class ShowTextInteractable : MonoBehaviour
{
    [SerializeField]
    private float CircleRadius;
    private string InteractableName;

    private IInteraction Interaction;
    private TextMeshPro TextMeshGO;

    private bool IsOnCooldown;

    private void Awake() {
        Interaction = GetComponent<IInteraction>();
        TextMeshGO = GetComponentInChildren<TextMeshPro>();
    }

    private void Start() {
        InteractableName = Interaction.GetInteractableName();
        TextMeshGO.text = formatTextToDisplay(InteractableName);
    }

    private void Update() {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, CircleRadius, Vector2.zero, 0, LayerMask.GetMask("Player"));
        TextMeshGO.gameObject.SetActive(hit);

        if (hit) {
            IsOnCooldown = !Interaction.CanInteract();
            TextMeshGO.text = formatTextToDisplay(InteractableName);
        }
    }

    private string formatTextToDisplay(string interactableType) {
        string formattedText = "Para acessar " + interactableType;

        if(!IsOnCooldown) { 
            formattedText += "\nPague " + Interaction.GetCost() + " pontos";
        }
        else {
            formattedText += " novamente\nEspere " + ((int)Interaction.TimeToInteract()) + " segundos";
        }

        return formattedText;
    }
}
