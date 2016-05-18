namespace CqrsRestService.CorePortable
{
    public interface IRestServiceQuery<T>
    {
        T Result { get; set; }
    }
}