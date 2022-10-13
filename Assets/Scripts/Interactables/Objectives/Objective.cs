using UnityEngine;

internal abstract class Objective : MonoBehaviour, IObjective
{
    public virtual bool IsComplete => false;

    public abstract void Complete();

    public void Interact()
    {
        if(!IsComplete)
        {
            return;
        }

        Complete();
    }

}
