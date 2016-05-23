namespace CqrsRestService.CorePortable
{
    public abstract class RestServiceQuery<T> : IRestServiceQuery
    {
        public object Body { get; set; }
        public T Result { get; set; }
    }

    public interface IRestServiceQuery
    {
        //object Body { get; set; }
    }
}