namespace ProductManagementAPI.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }   
        public List<string>? Errors { get; set; }
        public T? ResponseObject { get; set; }
    }
}
