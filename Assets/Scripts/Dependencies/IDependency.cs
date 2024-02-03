// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings


#endregion

namespace Racing3D
{
    public interface IDependency<T>
    {
        void Construct(T t);
    }
}