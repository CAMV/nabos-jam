/// <summary>
/// Modificator types. Additive modificators are added to the base value,
/// which then is multiplied by the multiplicative values.
/// </summary>
public enum ModValueType{
    Additive,
    //Treat multiplicative values as a base 1 percentage. 
    //e.g a 40% increase would be a multiplicative value of 1.4
    Multiplicative      
}
