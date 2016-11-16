namespace Lob.Net.Resouces
{
    public interface IResource
    {
        dynamic All(dynamic options, bool includeMetaData);
        dynamic Create(dynamic data);
        dynamic Delete(string id);
        dynamic Get(string id);
    }
}