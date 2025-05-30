public class DronState : State
{
    protected DronView _dronView;

    protected override void Init()
    {
        if (Character is DronView dv)
        {
            _dronView = dv;
        }
    }

    public virtual void ResourcePickUpped(ResourceView resourceView)
    {

    }

    public virtual void SpawnedResources(ResourceView resourceView)
    {

    }

    public override bool CheckRules(IStatesCharacter character)
    {
        return character.IsFinishedCurrentState();
    }
}