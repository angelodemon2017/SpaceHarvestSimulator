using UnityEngine;

public interface IStatesCharacter
{
    bool IsFinishedCurrentState();

    Transform GetTransform();

    MonoBehaviour GetEntityMonobeh();

    void SetState(State state);
}