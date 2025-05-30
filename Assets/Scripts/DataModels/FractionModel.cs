public class FractionModel : IDataModel
{
    public string ModelName => nameof(FractionModel);

    public EFraction Fraction;
    public int Resources;

    public FractionModel(EFraction fraction)
    {
        Fraction = fraction;
        Resources = 0;
    }
}