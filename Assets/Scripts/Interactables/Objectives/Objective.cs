using UnityEngine;

internal abstract class Objective : Interactable, IObjective
{
    public virtual bool IsComplete => false;

    public abstract void Complete();

    public override void Interact()
    {
        if(!IsComplete)
        {
            return;
        }

        Complete();
    }

}
