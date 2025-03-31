public interface IClonableModel<T> where T : class
{
    T ShallowCopy();
    T DeepCopy();
}