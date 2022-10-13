using TMPro;
using UnityEngine;

public class InteractableLabel : MonoBehaviour
{
    [SerializeField] private TextMeshPro _label;

    private void Start()
    {
        HideLabel();
    }

    public void ShowLabel()
    {
        _label.gameObject.SetActive(true);
    }

    public void HideLabel()
    {
        _label.gameObject.SetActive(false);
    }
}
