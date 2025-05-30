using UnityEngine;

[CreateAssetMenu(menuName = "States/Idle State", order = 1)]
public class IdleState : DronState
{
    protected override void Init()
    {
        base.Init();
        IsFinished = true;
    }

    public override bool CheckRules(IStatesCharacter character)
    {
        return character.IsFinishedCurrentState();
    }
}